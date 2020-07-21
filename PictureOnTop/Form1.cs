using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;
using System.Runtime.InteropServices;





namespace PictureOnTop
{
    public partial class Form1 
    {
        #region make form movable

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern bool ReleaseCapture();

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SendMessage(
    IntPtr hwnd,
    int wMsg,
    int wParam,
    int lParam);

        private const int HT_CAPTION = 0x2; // what does this means
        private const int WM_NCLBUTTONDOWN = 0x00A1; // what does this means
       
      
        protected override void OnMouseDown(MouseEventArgs e)// what does this means
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Rectangle rct = DisplayRectangle;
                if (rct.Contains(e.Location))
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }

        #endregion

        //FolderBrowserDialog FolderDialog;

        private string m_selectedFolder;

        public string SelectedFolder
        {
            get { return m_selectedFolder; }
            set
            { 
                m_selectedFolder = value;
                Properties.Settings.Default.DefaultFolder = value;
                Properties.Settings.Default.Save();
            }
        }

        private string  m_selectedFile;

        public string SelectedFile
        {
            get { return m_selectedFile; }
            set { m_selectedFile = value; }
        }

        UndoManager m_undoManager =null;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            m_undoManager = new UndoManager();

            Properties.Settings.Default.Reload();
            SelectedFolder = Properties.Settings.Default.DefaultFolder;
            FolderDialog.SelectedPath = SelectedFolder;
            clrDialogSelection = Color.White;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            transparency = trackBar2.Value;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_undoManager.Clean();
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderDialog.SelectedPath = Properties.Settings.Default.DefaultFolder;
            if (FolderDialog.SelectedPath.Length==0)
            {
                if (FolderDialog.ShowDialog() == DialogResult.OK)
                    SelectedFolder = FolderDialog.SelectedPath;
            }
            // Show the dialog and get result.
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = m_selectedFolder;
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    SelectedFile = ofd.FileName;
                    m_selectedFolder = Path.GetDirectoryName(SelectedFile);
                    ofd.InitialDirectory = m_selectedFolder;
                    Properties.Settings.Default.DefaultFolder = m_selectedFolder;
                    Properties.Settings.Default.Save();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            Image image = Image.FromFile(SelectedFile);
            // Set the PictureBox image property to this image.
            // ... Then, adjust its height and width properties.
            SetImageInPicturebox((Bitmap)new Bitmap(image));

            pictureBox1.Height = image.Height;
            pictureBox1.Width = image.Width;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private bool  m_topMostProperty;

        public bool  TopMostProperty
        {
            get { return m_topMostProperty; }
            set { 
                m_topMostProperty = value;
                this.TopMost = value;            }
        }

        private void setTopMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMostProperty = !TopMostProperty;

            if (TopMostProperty)
            {
                setTopMostToolStripMenuItem.CheckState = CheckState.Checked;
                setTopMostToolStripMenuItem.ForeColor = Color.Red;
            }
            else
            {
                setTopMostToolStripMenuItem.ForeColor = Color.DarkGray;
                setTopMostToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            

            

        }

        enum enRotate
        {
            left, right, flipX, flipY
        }

        void Flip(enRotate en)
        {
            Bitmap bitmap1 = (Bitmap)pictureBox1.Image;
            if (bitmap1 == null)
                return;

                switch (en)
            {
                case enRotate.left:
                    {
                        bitmap1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    break;
                case enRotate.right:
                    {
                        bitmap1.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    break;
                case enRotate.flipX:
                    {
                        bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
                    }
                    break;
                case enRotate.flipY:
                    {
                        bitmap1.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    }
                    break;
                default:
                    break;
            }
            SetImageInPicturebox((Bitmap)bitmap1.Clone());
        }

        private void SetImageInPicturebox(Bitmap bitmap1)
        {
            pictureBox1.Image = bitmap1;
            m_undoManager.AddNewImage(bitmap1);
            UpdateUndoMenuEnability();
        }

        private void UpdateUndoMenuEnability()
        {
            undoToolStripMenuItem.Enabled = m_undoManager.GetCurrentIndex > 0;
            redoToolStripMenuItem.Enabled = m_undoManager.GetCurrentIndex < m_undoManager.GetTotalItemsInStorage() - 1;
        }

        private void rorateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip(enRotate.flipX);
            Bitmap bitmap1 = (Bitmap)pictureBox1.Image;
            if (bitmap1 != null)
            {
                bitmap1.RotateFlip(RotateFlipType.Rotate180FlipNone);
                SetImageInPicturebox((Bitmap)bitmap1.Clone());
            }
        }

        private void flipVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip(enRotate.flipY);
        }

        private void rotateXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip(enRotate.right);
        }

        private void rotateLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip(enRotate.left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Flip(enRotate.right);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Flip(enRotate.left);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Flip(enRotate.flipX);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Flip(enRotate.flipY);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap capcha = new Bitmap(pictureBox1.Image); 
          //  capcha.MakeTransparent(selectedByMouse);
            //pictureBox1.Image = capcha;

            Color color = m_selectedByMouse;// Color.Black; //Your desired colour
            Color clrTransparent = Color.FromArgb(transparency, clrDialogSelection.R, clrDialogSelection.G, clrDialogSelection.B);
            
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int tolerance = Tolerance;
                    Color gotColor = bmp.GetPixel(x, y);
                    int delta_R = Math.Abs(color.R - gotColor.R);
                    int delta_G = Math.Abs(color.G - gotColor.G);
                    int delta_B = Math.Abs(color.B - gotColor.B);
                    if (delta_R < tolerance && delta_G < tolerance && delta_B < tolerance)
                    {
                        if (!bMIxWithOriginalImage)
                        {
                            bmp.SetPixel(x, y, clrTransparent);
                        }
                        else
                        {
                            Byte R=0, G=0, B=0;
                            R = (Byte)((clrDialogSelection.R + gotColor.R)/2 < (Byte)255 ? clrDialogSelection.R/2 + gotColor.R/2 : (Byte)255);
                            G = (Byte)((clrDialogSelection.G + gotColor.G)/2 < (Byte)255 ? clrDialogSelection.G/2 + gotColor.G/2 : (Byte)255);
                            B = (Byte)((clrDialogSelection.B + gotColor.B)/2 < (Byte)255 ? clrDialogSelection.B/2 + gotColor.B/2 : (Byte)255);

                            Color clr= Color.FromArgb(transparency, R, G, B);
                            bmp.SetPixel(x, y, clr);// gotColor);
                        }
                    }
                                                  //gotColor = Color.FromArgb(r, gotColor.G, gotColor.B);

                }
            }
            SetImageInPicturebox((Bitmap)bmp.Clone());

        }

        private Color m_selectedByMouse;

        public Color selectedByMouse
        {
            get { return m_selectedByMouse; }
            set
            {
                m_selectedByMouse = value;
                try
                {
                    panel1.BackColor = value;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drag)
                return;

            Bitmap capcha = (Bitmap)pictureBox1.Image;
            if (capcha == null)
                return;

            //private void originalmaster_MouseClick(object sender, MouseEventArgs e)
            //{
            Point mDown = Point.Round(stretched(e.Location, pictureBox1));
            PointF pf = stretched(mDown,pictureBox1);

            //Color c = ((Bitmap)capcha).GetPixel((int) pf.X, (int)pf.Y);
            Color c = ((Bitmap)capcha).GetPixel((int)mDown.X, (int)mDown.Y);
            // do your stuff:
            selectedByMouse = c;

            //}
        }

        private PointF stretched(Point p0, PictureBox pb)
        {
            if (pb.Image == null) return PointF.Empty;

            float scaleX = 1f * pb.Image.Width / pb.ClientSize.Width;
            float scaleY = 1f * pb.Image.Height / pb.ClientSize.Height;

            return new PointF(p0.X * scaleX, p0.Y * scaleY);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public Color  clrDialogSelection { get; set; }
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                clrDialogSelection = colorDialog1.Color;
                button6.BackColor = clrDialogSelection;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
            Tolerance = trackBar1.Value;
        }
        private int m_Tolerance;

        public int Tolerance
        {
            get { return m_Tolerance; }
            set { m_Tolerance = value; }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void makeBorderlessToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.None)
                this.FormBorderStyle = FormBorderStyle.None;
            else
                this.FormBorderStyle = FormBorderStyle.Sizable;


        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SelectedFolder!=null)
              saveFileDialog1.InitialDirectory = SelectedFolder;

            saveFileDialog1.FileName = SelectedFile;

            saveFileDialog1.ShowDialog();

            if(saveFileDialog1.FileName!=null && !String.IsNullOrEmpty(saveFileDialog1.FileName))
            {

                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoUndo();
        }

        private void DoUndo()
        {
            System.Drawing.Bitmap bm = m_undoManager.SetOperation(enOperation.undo);
            if (bm != null)
                pictureBox1.Image = bm;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRedo();
        }

        private void DoRedo()
        {
            System.Drawing.Bitmap bm = m_undoManager.SetOperation(enOperation.redo);
            if (bm != null)
                pictureBox1.Image = bm;
        }

        private void editToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            UpdateUndoMenuEnability();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                DoUndo();
            }
            else if (keyData == (Keys.Control | Keys.Y))
            {
                DoRedo();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                DoUndo();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                DoRedo();
            }
        }

        bool bMIxWithOriginalImage { get; set; }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bMIxWithOriginalImage = checkBox1.Checked;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = trackBar2.Value.ToString();
            transparency = trackBar2.Value;
        }

        private int m_transparency;

        public int transparency
        {
            get { return m_transparency; }
            set { m_transparency = value; }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }
    }
}

