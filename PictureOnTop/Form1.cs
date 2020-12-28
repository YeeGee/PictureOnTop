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
using System.Windows.Forms.VisualStyles;
using System.Drawing.Imaging;

namespace PictureOnTop
{
    enum enActionType { pickup_color_old, pickup_color_new }
    enum enColorSource { colordialog, point }

    public delegate void copyToFatherTextBox(Rectangle r);

    public partial class Form1 
    {
        
        public event EventHandler copyToFatherTextBox;


       
        enActionType m_enActionType { get; set; }
        enColorSource m_enColorSource { get; set; }

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

        #region Screenshot by mouse

        /*Start screenshot*/
        private void button10_Click(object sender, EventArgs e)
        {
            //ScreenForm screen = new ScreenForm();
            //screen.copytoFather += new copyToFatherTextBox(copytoTextBox);
            //screen.ShowDialog();
            this.Hide();
            //Save save = new Save(this.Location.X, this.Location.Y, this.Width, this.Height, this.Size);
            //save.Show();

            SelectArea area = new SelectArea();
            area.KeyPreview = true;
            area.PreviewKeyDown += Area_PreviewKeyDown;
            this.Hide();
            area.M_parentForm = this;
            area.Show();

            /*
             this.pbCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
             * */

        }

        private void Area_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        /*Screenshot of subsequent operations*/
        public void copytoTextBox(Rectangle rec)
        {
            Rectangle rec2 = rec;
            if (rec.Width > 2 && rec.Height > 2)
                rec2 = new Rectangle(rec.X + 1, rec.Y + 1, rec.Width - 2, rec.Height - 2);
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Image img = new Bitmap(rec2.Width, rec2.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(rec2.Location, new Point(0, 0), rec2.Size);
            Clipboard.SetDataObject(img, false);
            //richTextBox1.Paste();
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
            m_enColorSource = enColorSource.colordialog;
            m_enActionType = enActionType.pickup_color_old;
            this.FormClosing += Form1_FormClosing;
            m_undoManager = new UndoManager();

            //persistence
            Properties.Settings.Default.Reload();


            SelectedFolder = Properties.Settings.Default.DefaultFolder;
            FolderDialog.SelectedPath = SelectedFolder;
            clrDialogSelection = Color.White;
            this.Load += Form1_Load;

            pdCapture.MouseDown += PictureBox1_MouseDown;
            
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // throw new NotImplementedException();
            if (drag)
                return;

            Bitmap capcha = (Bitmap)pdCapture.Image;
            if (capcha == null)
                return;

            if (m_enColorSource == enColorSource.point)
            {

                //private void originalmaster_MouseClick(object sender, MouseEventArgs e)
                //{
                Point mDown = Point.Round(stretched(e.Location, pdCapture));
                PointF pf = stretched(mDown, pdCapture);

                //Color c = ((Bitmap)capcha).GetPixel((int) pf.X, (int)pf.Y);
                Color c = ((Bitmap)capcha).GetPixel((int)mDown.X, (int)mDown.Y);
                // do your stuff:
                selectedByMouse = c;
                colorDialog1.Color = clrDialogSelection = selectedByMouse;
                //switch (m_enActionType)
                //{
                //    case enActionType.pickup_color_old:
                //        lbl_color_old.BackColor = selectedByMouse;
                //        break;
                //    case enActionType.pickup_color_new:
                //        lbl_color_new.BackColor = selectedByMouse;
                //        break;
                //    default:
                //        break;
                //}
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            transparency = trackBar2.Value;
            trackBar1.Value = 1;
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
            Bitmap bitmap1 = (Bitmap)pdCapture.Image;
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

        private Bitmap _image;
        public Bitmap m_image
        {
            get { return _image; }
            set
            {
                _image = value;
                if (value != null)
                {
                    // PointF[] points = CreateCirclePointArray(10.0, value);
                    PictureBox pb = pdCapture;
                    DrawCircle(60, value, pb);
                    
                }
            }
        }

        private void DrawCircle(int circle_diameter, Bitmap bmp, PictureBox pb)
        {
            //e.Graphics.DrawEllipse(myPen, x1, y1, width, height);
            //Graphics g = pb.CreateGraphics();
            using (var g = Graphics.FromImage(bmp))
            {
                // Probably necessary for you:
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;// Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                Pen p = new Pen  (new SolidBrush(Color.Red), 0.01f);
                //g.DrawEllipse(p, bmp.Width/2, bmp.Height/2, circle_diameter, circle_diameter);
                
                //g.                DrawCurve(_penAxisMain, points);
                pb.Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diameter in mm"></param>
        /// <returns></returns>
        private PointF[] CreateCirclePointArray(double diameter_in_milimiters, Bitmap bmp)
        {
            
            //let's see total length - circumference of the circle 2*p*r
            double len = diameter_in_milimiters * Math.PI;
            //how many pixels fits that?
            //we need to know size of one pixel
            //get DPI - density per inch
            //so per inch
            float dpi = bmp.HorizontalResolution;
            // calculate densiti per mm
            double dpi_mm = dpi / 25.4;
            //now, how many pixels it would take to draw circle
            int pixels = Convert.ToInt32(Math.Round(len / dpi_mm));
            
            //generate array
            PointF[] points = new PointF[pixels];
            //assign value to each point by interpolating circumference path - sort of like placing each point one after another to fill it up...
            //let's figure size of pixel
            //then giving it size find position of this point should be placed .It should be on a cirtain distance  
            //this 'step' is basically of size of each pixel
            //let s get it
            double pixel_size = (2.54 / dpi);
            //so , now it gets interesting   

            return  points;
        }

        public void SetImageInPicturebox(Bitmap bitmap1)
        {
            m_image = bitmap1;

            pdCapture.Image = bitmap1;
            pdCapture.Height = bitmap1.Height;
            pdCapture.Width = bitmap1.Width;
          //  pdCapture.SizeMode = PictureBoxSizeMode.StretchImage;

            m_undoManager.AddNewImage((Bitmap)bitmap1.Clone());
            UpdateUndoMenuEnability();
            SetStandaloneFormImage((Bitmap)bitmap1.Clone());
            pdCapture.Invalidate();
        }

        public void DrawData(PointF[] points, Bitmap bitmap1)
        {
           // b
            var bmp = bitmap1;
            using (var g = Graphics.FromImage(bmp))
            {
                // Probably necessary for you:
                g.Clear(Color.Transparent);
                Pen p = new Pen(Color.Red, 2.0f);
                g.DrawCurve(p, points);
            }

            pdCapture.Invalidate(); // Trigger redraw of the control.
        }

        private void UpdateUndoMenuEnability()
        {
            undoToolStripMenuItem.Enabled = m_undoManager.GetCurrentIndex > 0;
            redoToolStripMenuItem.Enabled = m_undoManager.GetCurrentIndex < m_undoManager.GetTotalItemsInStorage() - 1;
        }

        private void rorateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip(enRotate.flipX);
            Bitmap bitmap1 = (Bitmap)pdCapture.Image;
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
            Bitmap capcha = new Bitmap(pdCapture.Image);
            //  capcha.MakeTransparent(selectedByMouse);
            //pictureBox1.Image = capcha;

            Color color = pn_color_to_replace.BackColor;// m_selectedByMouse;// Color.Black; //Your desired colour
            Color clrTransparent = Color.FromArgb(transparency, clrDialogSelection.R, clrDialogSelection.G, clrDialogSelection.B);
            
            Bitmap bmp = new Bitmap(pdCapture.Image);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int tolerance = Tolerance;
                    Color gotColor = bmp.GetPixel(x, y);
                    int delta_R = Math.Abs(color.R - gotColor.R);
                    int delta_G = Math.Abs(color.G - gotColor.G);
                    int delta_B = Math.Abs(color.B - gotColor.B);
                    if ((delta_R < tolerance) && (delta_G < tolerance) && (delta_B < tolerance))
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
                    
                    switch (m_enActionType)
                    {
                        case enActionType.pickup_color_old:
                            pn_color_to_replace.BackColor = value;

                            break;
                        case enActionType.pickup_color_new:
                            pn_color_new.BackColor = value;
                            break;
                        default:
                            break;
                    }

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

            Bitmap capcha = (Bitmap)pdCapture.Image;
            if (capcha == null)
                return;

            //private void originalmaster_MouseClick(object sender, MouseEventArgs e)
            //{
            Point mDown = Point.Round(stretched(e.Location, pdCapture));
            PointF pf = stretched(mDown,pdCapture);

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
            switch (m_enColorSource)
            {
                case enColorSource.colordialog:
                    break;
                case enColorSource.point:
                    break;
                default:
                    break;
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

                pdCapture.Image.Save(saveFileDialog1.FileName);
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
                pdCapture.Image = bm;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRedo();
        }

        private void DoRedo()
        {
            System.Drawing.Bitmap bm = m_undoManager.SetOperation(enOperation.redo);
            if (bm != null)
                pdCapture.Image = bm;
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

            if (frmDraggable != null && frmDraggable.Controls.Count > 0)
            {
                frmDraggable.transparency = transparency;
            }
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

        private void rb_colordialog_CheckedChanged(object sender, EventArgs e)
        {
            m_enColorSource = enColorSource.colordialog;
        }

        private void rb_point_CheckedChanged(object sender, EventArgs e)
        {
            m_enColorSource = enColorSource.point;
        }

        private void pn_color_to_replace_MouseClick(object sender, MouseEventArgs e)
        {
            m_enActionType = enActionType.pickup_color_old;
            lbl_color_old.BorderStyle = BorderStyle.Fixed3D;
            lbl_color_new.BorderStyle = BorderStyle.None;
            pn_color_new.BorderStyle = BorderStyle.None;
            pn_color_to_replace.BorderStyle=BorderStyle.Fixed3D;
//            if( m_enColorSource == enColorSource.colordialog)
//            {
//                #region pickup color from dialog
//                DialogResult result = colorDialog1.ShowDialog();
//                // See if user pressed ok.
//                if (result == DialogResult.OK)
//                {
//                    // Set form background to the selected color.
//                    clrDialogSelection = colorDialog1.Color;
//                    pn_color_to_replace.BackColor = clrDialogSelection;
//                    pn_color_to_replace.Refresh();

//                }
//                #endregion

//            }
//            else
//            {
////                pn_color_to_replace.BackColor = selectedByMouse;
////                pn_color_to_replace.Refresh();

//            }
        }

        private void pn_color_new_Click(object sender, EventArgs e)
        {
            m_enActionType = enActionType.pickup_color_new;
            lbl_color_new.BorderStyle = BorderStyle.Fixed3D;
            lbl_color_old.BorderStyle = BorderStyle.None;
            pn_color_new.BorderStyle = BorderStyle.Fixed3D;
            pn_color_to_replace.BorderStyle = BorderStyle.None;
            if (m_enColorSource == enColorSource.colordialog)
            {
                #region pickup color from dialog
                DialogResult result = colorDialog1.ShowDialog();
                // See if user pressed ok.
                if (result == DialogResult.OK)
                {
                    // Set form background to the selected color.
                    clrDialogSelection = colorDialog1.Color;
                    pn_color_new.BackColor = clrDialogSelection;
                    pn_color_new.Refresh();
                }
                #endregion

            }


        }

        private int m_sizeMode;

        public int sizeMode
        {
            get { return m_sizeMode; }
            set { m_sizeMode = value; }
        }



        DraggableForm.FormBase frmDraggable { get; set; } 
        void SetStandaloneFormImage (Bitmap bitmap1)
        {
            
            if (frmDraggable!=null  && frmDraggable.Controls.Count>0)
            {
                frmDraggable.transparency = transparency;
                PictureBox pb = (PictureBox)frmDraggable.Controls.Find("pb1", false)[0];
                pb.Image = bitmap1;
                pb.Height = bitmap1.Height;
                pb.Width = bitmap1.Width;
                pb.SizeMode = chImageStretch.Checked?PictureBoxSizeMode.StretchImage: PictureBoxSizeMode.CenterImage;
                frmDraggable.UpdateImage();
            }
        }

        private void lunchWpfFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(frmDraggable==null)
            {
                frmDraggable = new DraggableForm.FormBase();
                frmDraggable.WindowState = FormWindowState.Normal;
                frmDraggable.FormBorderStyle = FormBorderStyle.None;
                frmDraggable.MouseDoubleClick += FrmDraggable_MouseDoubleClick;
                frmDraggable.FormClosing += FrmDraggable_FormClosing;
                frmDraggable.Draggable = true;
                frmDraggable.TopMost = true;
                //frmDraggable.Cursor=Cursor.

                //add controls
                PictureBox pb1 = new PictureBox();
                pb1.Tag = "pb1";
                pb1.Name = "pb1";
                pb1.Dock = DockStyle.Fill;pb1.BackColor = Color.DarkGray;
                pb1.MouseDoubleClick += FrmDraggable_MouseDoubleClick;

                


                frmDraggable.ControlAdded += FrmDraggable_ControlAdded;
                frmDraggable.Controls.Add(pb1);
                frmDraggable.Shown += FrmDraggable_Shown;

                if (pdCapture.Image != null)
                    SetStandaloneFormImage((Bitmap)pdCapture.Image.Clone());


                frmDraggable.Show();
            }
        }

        private void FrmDraggable_Shown(object sender, EventArgs e)
        {
            frmDraggable.Location = Properties.Settings.Default.DraggableFormStartPostion;
            frmDraggable.Cursor = Cursors.SizeAll;
        }

        private void FrmDraggable_ControlAdded(object sender, ControlEventArgs e)
        {
            //we know it is picture box :)
            //align it in a middle
            //adjust form size to fit
            PictureBox pb = (PictureBox)frmDraggable.Controls.Find("pb1", false)[0];
            if (pdCapture.Image != null)
            {
                pb.Image = (Bitmap)pdCapture.Image.Clone();
                pb.Height = pb.Image.Height;
                pb.Width = pb.Image.Width;
                frmDraggable.Bounds = new Rectangle(frmDraggable.Left, frmDraggable.Top, (int)pb.Image.Width + 5, (int)pb.Image.Height + 5);
            }
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.SizeMode = ((CheckBox)chImageStretch).Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;


            
            frmDraggable.Size= new Size((int)pb.Width + 15, (int)pb.Height + 25);
            
        }

        private void FrmDraggable_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Properties.Settings.Default.DraggableFormStartPostion = new Point(frmDraggable.Bounds.Location.X, frmDraggable.Bounds.Location.Y);
            Properties.Settings.Default.Save();

            frmDraggable = null;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void FrmDraggable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(frmDraggable.FormBorderStyle==FormBorderStyle.None)
            {
                frmDraggable.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                frmDraggable.FormBorderStyle = FormBorderStyle.None;
            }
            
            
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pdCapture.Image != null)
                SetStandaloneFormImage((Bitmap)pdCapture.Image.Clone());
        }

        private void chImageStretch_CheckedChanged(object sender, EventArgs e)
        {
            pdCapture.SizeMode = ((CheckBox)sender).Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;

        }

        private void pictureBox1_SizeModeChanged(object sender, EventArgs e)
        {
             if (frmDraggable != null)
            {
                PictureBox pb = (PictureBox)frmDraggable.Controls.Find("pb1", false)[0];
                pb.SizeMode = pdCapture.SizeMode;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Create array of two points.
            Point[] points = { new Point(0, 0), new Point(100, 50) };
         //   e.Graphics.DrawLine(new Pen(Color.Blue, 3), points[0], points[1]);
            ////if (CursorPositionmyX ==0  &&  CursorPositionmyY == 0)
            ////    return;
            //try
            //{


            //using (Graphics g = e.Graphics)
            //{

            //    try
            //    {
            //     //   if (((float)(CursorPositionmyX) >= g.ClipBounds.X) && (((float)(CursorPositionmyX) <= g.ClipBounds.Width)))
            //        {
            //        //    if (((float)(CursorPositionmyY) >= g.ClipBounds.Y) && ((float)(CursorPositionmyY) <= g.ClipBounds.Height))
            //            {
            //              //  g.Clear(Color.Transparent);
            //             //   Pen p = new Pen(Color.Red, 2.0f);

            //                for (int n = 1; n <= 50; n++)
            //                {
            //                 //   g.DrawLine(p, n * (CursorPositionmyX), CursorPositionmyY - 30.0f, n * (CursorPositionmyX), CursorPositionmyY + 30.0f);

            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //   //     throw;
            //    }

            //}
            //}
            //catch (Exception ex )
            //{


            //}

        }

        private int cursorPositionmyX;

        public int CursorPositionmyX
        {
            get { return cursorPositionmyX; }
            private set { cursorPositionmyX = value; }
        }

        private int cursorPositionmyY;

        public int CursorPositionmyY
        {
            get { return cursorPositionmyY; }
            private set { cursorPositionmyY = value; }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPositionmyX = e.X;
            CursorPositionmyY = e.Y;
        }

        private void lunchWpfFormToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            
                refreshToolStripMenuItem.Enabled = frmDraggable != null;

            
        }
    }


}

