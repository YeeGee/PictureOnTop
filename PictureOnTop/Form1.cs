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
using System.Diagnostics;

namespace PictureOnTop
{
    #region ENUM
    enum enActionType  { pickup_color_old, pickup_color_new }
    enum enColorSource { colordialog, point }
    enum enRotate      { left, right, flipX, flipY }
    #endregion

    public delegate void copyToFatherTextBox(Rectangle r);

    public partial class Form1
    {
        #region Variables and properties
        UndoManager m_undoManager = null;
        enActionType m_enActionType { get; set; }
        enColorSource m_enColorSource { get; set; }
        Point[] m_pointsArrow { get; set; }
        Bitmap _image;
        public Bitmap m_image
        {
            get { return _image; }
            set
            {
                if (_image == null)
                {

                }

                _image = value;
                if (value != null)
                {
                    //  PointF[] points = CreateCirclePointArray(10.0, value);
                    //  PictureBox pb = pdCapture;
                    //  DrawCircle(60, value, pb);

                }
            }
        }

        bool mouseDownPictBox;
        public bool MouseDownPictBox
        {
            get { return mouseDownPictBox; }
            set 
            {
                if (mouseDownPictBox!=value)
                {
                    if (mouseDownPictBox == false && value == true)
                    {
                        segment_line_id = dict_points.Count;
                        dict_points.Add(segment_line_id, new List<Point>());
                        if (m_pointsArrow == null)
                            m_pointsArrow = new Point[2];

                        m_pointsArrow[0].X = CursorPositionmyX;
                        m_pointsArrow[0].Y = CursorPositionmyY;
                    }
                    
                    if(mouseDownPictBox==true && value==false)
                    {//mouse UP after been down

                        if (bDraw_Arrow)
                        {
                            dict_arrows_.Add(segment_arrow_id, new CustomTypes.Arrow(segment_arrow_id, m_pointsArrow.ToList(), clrArrow));

                            //dict_arrows.Add(segment_arrow_id, m_pointsArrow.ToList());

                            segment_arrow_id++;
                        }

                        if (bmpArrowDraw_Temp != null)
                        {
                           // m_image = (Bitmap)bmpArrowDraw_Temp.Clone();
                            //SetImageInPicturebox((Bitmap)bmpArrowDraw_Temp.Clone(), true);
                            
                        }
                    }
                }
                
                mouseDownPictBox = value;
            }
        }

        Color m_selectedByMouse;
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
        public Color clrDialogSelection { get; set; }
        int m_Tolerance;
        public int Tolerance
        {
            get { return m_Tolerance; }
            set { m_Tolerance = value; }
        }
        string m_selectedFolder;
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
        string m_selectedFile;
        public string SelectedFile
        {
            get { return m_selectedFile; }
            set { m_selectedFile = value; }
        }
        bool m_topMostProperty;
        public bool TopMostProperty
        {
            get { return m_topMostProperty; }
            set
            {
                m_topMostProperty = value;
                this.TopMost = value;
            }
        }
        bool bMIxWithOriginalImage { get; set; }
        int m_transparency = 255;
        public int transparency
        {
            get { return m_transparency; }
            set { m_transparency = value; }
        }
        int m_sizeMode;
        public int sizeMode
        {
            get { return m_sizeMode; }
            set { m_sizeMode = value; }
        }
        public DraggableForm.FormBase frmDraggable { get; set; }
        Rectangle rect = new Rectangle(5, 5, 5, 5);
        Point lastPoint = Point.Empty;
        Point p1, p2;
        List<Point> p1List = new List<Point>();
        List<Point> p2List = new List<Point>();
        int count = 0;
        List<Point> myPointList { get; set; }
        Dictionary<int, List<Point>> dict_points = new Dictionary<int, List<Point>>();
        int segment_line_id { get; set; }
        int segment_arrow_id { get; set; }
        Dictionary<int, CustomTypes.Arrow> dict_arrows_ = new Dictionary<int, CustomTypes.Arrow>();
        bool m_bDraw_Arrow;
        public bool bDraw_Arrow
        {
            get
            {
                return m_bDraw_Arrow;
            }
            set
            {
                m_bDraw_Arrow = value;
            }
        }
        public Bitmap bmpArrowDraw_Temp { get; set; }
        int cursorPositionmyX;
        public int CursorPositionmyX
        {
            get { return cursorPositionmyX; }
            private
            set
            {
                if (cursorPositionmyX != value)
                {
                    lblMouseX.Text = value.ToString();
                }
                cursorPositionmyX = value;

            }
        }
        int cursorPositionmyY;
        public int CursorPositionmyY
        {
            get { return cursorPositionmyY; }
            private
                set
            {
                if (cursorPositionmyY != value)
                {
                    lblMouseY.Text = value.ToString();
                }
                cursorPositionmyY = value;
            }
        }
        public bool isMouseDown { get; private set; }
        bool bClearMouseDraw { get; set; }
        Bitmap bmpCaptured;
        public Bitmap BmpCaptured
        {
            get { return bmpCaptured; }
            set
            {
                bmpCaptured = value;
            }
        }
        bool m_clearDrawsActive;
        public bool M_clearDrawsActive
        {
            get { return m_clearDrawsActive; }
            set { m_clearDrawsActive = value; }
        }
        public Color clrArrow { get; private set; }

        #endregion

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

        public delegate void Delegate_fn_show_capture_form();
        public Delegate_fn_show_capture_form m_Delegate_fn_show_capture_form { get; set; }

        /*Start screenshot*/
        private void button10_Click(object sender, EventArgs e)
        {
            m_Delegate_fn_show_capture_form.Invoke();
        }
        private void fn_show_capture_form()
        {
            if (frmDraggable != null)
            {
                frmDraggable.Hide();
            }
            this.Hide();
            SelectArea area = new SelectArea();
            area.KeyPreview = true;
            area.PreviewKeyDown += Area_PreviewKeyDown;

            area.M_parentForm = this;
            area.Show();
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

           
            m_Delegate_fn_show_capture_form = new Delegate_fn_show_capture_form(fn_show_capture_form);

           // pdCapture.ContextMenu = contextMenuStrip1;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            m_pointsArrow = new Point[2];
            transparency = trackBar2.Value;
            trackBar1.Value = 1;
            clrArrow = Color.FromArgb(125, 0, 0, 255);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_undoManager.Clean();
        }

        
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderDialog.SelectedPath = Properties.Settings.Default.DefaultFolder;
            if (FolderDialog.SelectedPath.Length == 0)
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
            if (SelectedFile != null)
            {
                Image image = Image.FromFile(SelectedFile);
                // Set the PictureBox image property to this image.
                // ... Then, adjust its height and width properties.
                SetImageInPicturebox((Bitmap)new Bitmap(image), true);
            }


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
            SetImageInPicturebox((Bitmap)bitmap1.Clone(), true);
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

                Pen p = new Pen(new SolidBrush(Color.Red), 0.01f);
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

            return points;
        }
        public void SetImageInPicturebox(Bitmap bitmap1, bool addToUndoManager)
        {
            m_image = bitmap1;

            pdCapture.Image = bitmap1;
            pdCapture.Height = bitmap1.Height;
            pdCapture.Width = bitmap1.Width;
            UpdateMonitor(bitmap1);

            //  pdCapture.SizeMode = PictureBoxSizeMode.StretchImage;
            if (addToUndoManager)
            {
                m_undoManager.AddNewImage((Bitmap)bitmap1.Clone());
            }
            UpdateUndoMenuEnability();
            SetStandaloneFormImage(bitmap1);
        }

        private void UpdateMonitor(Bitmap bitmap1)
        {
            pbMonitor.Image = (Bitmap)bitmap1.Clone();
            pbMonitor.Refresh();

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

        #region UNDO faunctionality
        private void UpdateUndoMenuEnability()
        {
            undoToolStripMenuItem.Enabled = m_undoManager.GetCurrentIndex > 0;
            redoToolStripMenuItem.Enabled = m_undoManager.GetCurrentIndex < m_undoManager.GetTotalItemsInStorage()-1;
        }
        private void DoUndo()
        {
            //
            
            System.Drawing.Bitmap bm = m_undoManager.SetOperation(enOperation.undo);
            if (bm != null)
            {
                pdCapture.Image = bm;
                pdCapture.Refresh();
                UpdateMonitor(bm);
            }
            else
            {
                Debug.Write("Undo returned Null\n");
            }
        }
        private void DoRedo()
        {
            System.Drawing.Bitmap bm = m_undoManager.SetOperation(enOperation.redo);
            if (bm != null)
            {
                pdCapture.Image = bm;
                UpdateMonitor(bm);
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoUndo();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoRedo();
        }

        #endregion

        #region rotate image

        private void rorateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flip(enRotate.flipX);
            Bitmap bitmap1 = (Bitmap)pdCapture.Image;
            if (bitmap1 != null)
            {
                bitmap1.RotateFlip(RotateFlipType.Rotate180FlipNone);
                SetImageInPicturebox((Bitmap)bitmap1.Clone(), true);
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
        #endregion

        #region User interaction

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
            Bitmap capcha = null;
            if (pdCapture.Image == null)
            {
                pdCapture.Image = new Bitmap(pdCapture.Width, pdCapture.Height);
            }
            else
                capcha = new Bitmap(pdCapture.Image);
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
                            Byte R = 0, G = 0, B = 0;
                            R = (Byte)((clrDialogSelection.R + gotColor.R) / 2 < (Byte)255 ? clrDialogSelection.R / 2 + gotColor.R / 2 : (Byte)255);
                            G = (Byte)((clrDialogSelection.G + gotColor.G) / 2 < (Byte)255 ? clrDialogSelection.G / 2 + gotColor.G / 2 : (Byte)255);
                            B = (Byte)((clrDialogSelection.B + gotColor.B) / 2 < (Byte)255 ? clrDialogSelection.B / 2 + gotColor.B / 2 : (Byte)255);

                            Color clr = Color.FromArgb(transparency, R, G, B);
                            bmp.SetPixel(x, y, clr);// gotColor);
                        }
                    }
                    //gotColor = Color.FromArgb(r, gotColor.G, gotColor.B);

                }
            }
            SetImageInPicturebox((Bitmap)bmp.Clone(), true);

        }
        #endregion

       
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drag)
                return;

            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
                return;
            }
            if (mouseDownPictBox == false)
            {
                CursorPositionmyX = e.X;
                CursorPositionmyY = e.Y;
            }

            m_pointsArrow[0].X= m_pointsArrow[1].X = CursorPositionmyX;
            m_pointsArrow[0].Y = m_pointsArrow[1].Y = CursorPositionmyY;

            MouseDownPictBox = true;
            if (pdCapture.Image == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                Bitmap capcha = (Bitmap)pdCapture.Image;
                if (capcha == null)
                    return;

                Point mDown = Point.Round(stretched(e.Location, pdCapture));
                PointF pf = stretched(mDown, pdCapture);
                Color c = ((Bitmap)capcha).GetPixel((int)mDown.X, (int)mDown.Y);
                // do your stuff:
                selectedByMouse = c;
            }
        }
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
        private PointF stretched(Point p0, PictureBox pb)
        {
            if (pb.Image == null) return PointF.Empty;

            float scaleX = 1f * pb.Image.Width / pb.ClientSize.Width;
            float scaleY = 1f * pb.Image.Height / pb.ClientSize.Height;

            return new PointF(p0.X * scaleX, p0.Y * scaleY);
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
            if (SelectedFolder != null)
                saveFileDialog1.InitialDirectory = SelectedFolder;

            saveFileDialog1.FileName = SelectedFile;

            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != null && !String.IsNullOrEmpty(saveFileDialog1.FileName))
            {
                if (pdCapture.Image == null)
                    return;

                pdCapture.Image.Save(saveFileDialog1.FileName);
            }
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
            pn_color_to_replace.BorderStyle = BorderStyle.Fixed3D;
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

        #region draggable standalone windows
        private void SetStandaloneFormImage(Bitmap bm)
        {

            if (frmDraggable != null && frmDraggable.Controls.Count > 0)
            {
                Bitmap bitmap1 = (Bitmap)bm.Clone();
                frmDraggable.transparency = transparency;
                PictureBox pb = (PictureBox)frmDraggable.Controls.Find("pb1", false)[0];
                pb.Image = bitmap1;
                pb.Height = bitmap1.Height;
                pb.Width = bitmap1.Width;
                pb.SizeMode = chImageStretch.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;
                pb.Paint += Pb_Paint;
                frmDraggable.UpdateImage();
            }
        }
        private void Pb_Paint(object sender, PaintEventArgs e)
        {
            bool IsDrawRect = true;
            int shift_left = 0; int shift_right = 1; int shift_top = 0; int shift_bottom = 1;
            if (IsDrawRect) // Flag Variable to check if need to draw rect
            {
                RectangleF r = e.Graphics.VisibleClipBounds;
                Rectangle RectMark = new Rectangle(shift_left, shift_top, (int)r.Width - shift_right - 1, (int)r.Height - shift_bottom - 1); // your location to draw
                e.Graphics.DrawRectangle(new Pen(Color.Gray, 1), RectMark);
            }
        }
        private void lunchWpfFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDraggable == null)
            {
                frmDraggable = new DraggableForm.FormBase();
                frmDraggable.WindowState = FormWindowState.Normal;
                frmDraggable.FormBorderStyle = FormBorderStyle.None;


                frmDraggable.MouseDoubleClick += FrmDraggable_MouseDoubleClick;
                //events
                frmDraggable.FormClosing += FrmDraggable_FormClosing;
                frmDraggable.OnCaptureRequest += FrmDraggable_OnCaptureRequest;
                frmDraggable.OnShowMainFormRequest += FrmDraggable_OnShowMainFormRequest;
                frmDraggable.Shown += FrmDraggable_Shown;
                frmDraggable.Load += FrmDraggable_Load;

                frmDraggable.OnShift += FrmDraggable_OnShift;



                frmDraggable.Draggable = true;
                frmDraggable.TopMost = true;
                //frmDraggable.Cursor=Cursor.

                //add controls
                PictureBox pb1 = new PictureBox();
                pb1.Tag = "pb1";
                pb1.Name = "pb1";
                pb1.Dock = DockStyle.Fill; pb1.BackColor = Color.Transparent;
                pb1.MouseDoubleClick += FrmDraggable_MouseDoubleClick;

                //now, we need some staff to resize image by mouse
                pb1.Paint += Pb1_Paint;
                pb1.MouseDown += Pb1_MouseDown;
                pb1.MouseMove += Pb1_MouseMove;
                pb1.MouseUp += Pb1_MouseUp;




                frmDraggable.ControlAdded += FrmDraggable_ControlAdded;
                frmDraggable.Controls.Add(pb1);


                if (pdCapture.Image != null)
                    SetStandaloneFormImage((Bitmap)pdCapture.Image);


                frmDraggable.WindowState = FormWindowState.Normal;
                frmDraggable.BringToFront();
                frmDraggable.Show();
            }
        }
        private void FrmDraggable_OnShift(object sender, EventArgs e)
        {
            isMouseDown = (bool)sender;
        }
        private void Pb1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = !true;
            //lastPoint = Point.Empty;

        }
        private void Pb1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                rect.Location = e.Location;
                /*
                if (rect.Right > pdCapture.Width)
                {
                    rect.X = pdCapture.Width - rect.Width;
                }
                if (rect.Top < 0)
                {
                    rect.Y = 0;
                }
                if (rect.Left < 0)
                {
                    rect.X = 0;
                }
                if (rect.Bottom > pdCapture.Height)
                {
                    rect.Y = pdCapture.Height - rect.Height;
                }
                */
                PictureBox pb = (PictureBox)frmDraggable.Controls.Find("pb1", false)[0];
                if (lastPoint != null)//if our last point is not null, which in this case we have assigned above

                {

                    if (pb.Image == null)//if no available bitmap exists on the picturebox to draw on

                    {
                        //create a new bitmap
                        Bitmap bmp = new Bitmap(pb.Width, pb.Height);

                        pb.Image = bmp; //assign the picturebox.Image property to the bitmap created


                    }

                    using (Graphics g = Graphics.FromImage(pb.Image))

                    {//we need to create a Graphics object to draw on the picture box, its our main tool

                        //when making a Pen object, you can just give it color only or give it color and pen size

                        //    g.DrawLine(new Pen(new SolidBrush(Color.Black),1), lastPoint, e.Location);
                        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//.AntiAliasing;
                        //this is to give the drawing a more smoother, less sharper look

                    }

                    pb.Invalidate();//refreshes the picturebox

                    //lastPoint = e.Location;//keep assigning the lastPoint to the current mouse position

                }

                Refresh();
            }
        }
        private void Pb1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            lastPoint = e.Location;
            if (p1.X == 0)
            {
                p1.X = e.X;
                p1.Y = e.Y;
                count++;
            }
            else
            {
                p2.X = e.X;
                p2.Y = e.Y;
                count++;

                p1List.Add(p1);
                p2List.Add(p2);

                PictureBox pb = (PictureBox)frmDraggable.Controls.Find("pb1", false)[0];

                pb.Invalidate();
                //Refresh();
                // Sets X to 0 and choose p1 again
                p1.X = 0;
            }


        }
        private void Pb1_Paint(object sender, PaintEventArgs e)
        {

            return;

            e.Graphics.FillRectangle(new SolidBrush(Color.RoyalBlue), rect);
            using (var p = new Pen(Color.Blue, 1))
            {
                for (int x = 0; x < p1List.Count; x++)
                {
                    e.Graphics.DrawLine(p, p1List[x], p2List[x]);
                }
            }
        }
        private void FrmDraggable_Load(object sender, EventArgs e)
        {

        }
        private void FrmDraggable_OnShowMainFormRequest(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.Show();
            this.BringToFront();
        }
        private void FrmDraggable_OnCaptureRequest(object sender, EventArgs e)
        {
            m_Delegate_fn_show_capture_form.Invoke();
        }
        private void FrmDraggable_Shown(object sender, EventArgs e)
        {
            frmDraggable.Location = Properties.Settings.Default.DraggableFormStartPostion;
            //frmDraggable.Cursor = Cursors.SizeAll;
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
                frmDraggable.Bounds = new Rectangle(frmDraggable.Left, frmDraggable.Top, (int)pb.Image.Width, (int)pb.Image.Height);
            }
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.SizeMode = ((CheckBox)chImageStretch).Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.CenterImage;



            frmDraggable.Size = new Size((int)pb.Width - 10, (int)pb.Height + 0);

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
            if (frmDraggable.FormBorderStyle != FormBorderStyle.Sizable)
            {
                frmDraggable.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                frmDraggable.FormBorderStyle = FormBorderStyle.None;
            }


        }
        #endregion

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
            bool IsDrawRect = true;
            bool bDrawDot = false;
            bool bDrawVline = false;
            int shift_left = 0; int shift_right = 1; int shift_top = 0; int shift_bottom = 1;
            int transparency = 155;
            int tempX = 0;
            int tempY = 0;
            Bitmap image = new Bitmap(pdCapture.Width, pdCapture.Height);
            SolidBrush brush = new SolidBrush(Color.Empty);

            if (pdCapture.Image == null)
                pdCapture.Image = new Bitmap(pdCapture.Width, pdCapture.Height, e.Graphics);

            Graphics G1 = Graphics.FromImage(pdCapture.Image);


            // Bitmap bmp = new Bitmap(pdCapture.Width, pdCapture.Height, g2);

            // draws border around captured image
            if (IsDrawRect) 
            {
                RectangleF r = e.Graphics.VisibleClipBounds;
                Rectangle RectMark = new Rectangle(shift_left, shift_top, (int)r.Width - shift_right-1, (int)r.Height - shift_bottom-1); // your location to draw
                //Graphics.FromImage(bmp).DrawRectangle(new Pen(Color.Red, 1), RectMark);
                G1.DrawRectangle(new Pen(Color.YellowGreen, 1), RectMark);
            }

            if (true)
            {
                try
                {
                    #region NOT USED
                    if (MouseDownPictBox && false)
                {
                    RectangleF r = e.Graphics.VisibleClipBounds;

                    Color clrMouseDraw = Color.Green;// OnSystemColorsChanged(). bmp.GetPixel(x, y);
                    brush.Color = Color.FromArgb(transparency, clrMouseDraw.R, clrMouseDraw.G, clrMouseDraw.B);

                    G1.FillRectangle(brush, CursorPositionmyX, CursorPositionmyY, 2, 2);
                }
                    #endregion

                    int id = segment_line_id;
                    if (dict_points.Count > 0)
                         if (dict_points[id] != null)
                            //for each segment
                          for(int i=0; i < dict_points.Count; i++)   
                        {
                            if (dict_points[i].Count >= 2)
                                    G1.DrawLines(Pens.Black, dict_points[i].ToArray());
                        }

                    //dict_arrows
                    int id1 = segment_arrow_id;

                    if (dict_arrows_.Count > 0)
                    {
                        for (int i = 0; i < dict_arrows_.Count; i++)
                        {
                            if (dict_arrows_[i].Count >= 2)
                            {
                                Pen penMidlleLine = new Pen(Color.FromArgb(120, 0, 0, 200), 1);
                                G1.DrawLine(penMidlleLine, dict_arrows_[i].point[0], dict_arrows_[i].point[1]);
                                
                                // make subarrows
                                Point[] subarrow1 = new Point[2];
                                Point[] subarrow2 = new Point[2];

                                subarrow1[1].X = dict_arrows_[i].point[0].X;
                                subarrow1[1].Y = dict_arrows_[i].point[0].Y;
                                subarrow1[0].X = dict_arrows_[i].point[1].X;
                                subarrow1[0].Y = dict_arrows_[i].point[1].Y;

                                Pen pen = new Pen(dict_arrows_[i].color, 4);
                                pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                                pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                                G1.DrawLine(pen, subarrow1[0], subarrow1[1]);

                            }
                        }
                    }

                    #region old
                    /*
                    if (dict_arrows.Count > 0)

                            for (int i = 0; i < dict_arrows.Count; i++)
                            {
                                if (dict_arrows[i].Count >= 2)
                                {

                                    Pen penMidlleLine = new Pen(Color.FromArgb(120, 0, 0, 200), 1);
                                    g2.DrawLine(penMidlleLine, dict_arrows[i][0], dict_arrows[i][1]);
                                    // make subarrows
                                         Point[] subarrow1 = new Point[2];
                                         Point[] subarrow2 = new Point[2];

                                    subarrow1[1].X = dict_arrows[i][0].X;
                                    subarrow1[1].Y = dict_arrows[i][0].Y;
                                    subarrow1[0].X = dict_arrows[i][1].X;
                                    subarrow1[0].Y = dict_arrows[i][1].Y;

                                    Pen pen = new Pen(clrArrow, 4);
                                    pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                                    pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
                                    g2.DrawLine(pen, subarrow1[0], subarrow1[1]);
                                }
                            }
                      */
                    #endregion
                }
                catch (Exception)
                {

                    throw;
                }

            }

            if (!M_clearDrawsActive)
            {
                if (bDraw_Arrow)
                {
                    Rectangle r = new Rectangle(m_pointsArrow[0].X, m_pointsArrow[0].Y,
                        m_pointsArrow[1].X - m_pointsArrow[0].X, m_pointsArrow[1].Y - m_pointsArrow[0].Y);
                    Pen penMidlleLine = new Pen(Color.FromArgb(50, 20, 20, 20), 1);
                    G1.DrawLine(penMidlleLine, m_pointsArrow[0], m_pointsArrow[1]);
                    bmpArrowDraw_Temp = new Bitmap(pdCapture.Width, pdCapture.Height, e.Graphics);
                }
                else
                {
                    Rectangle r = new Rectangle(m_pointsArrow[0].X, m_pointsArrow[0].Y,
                        m_pointsArrow[1].X - m_pointsArrow[0].X, m_pointsArrow[1].Y - m_pointsArrow[0].Y);
                    if (m_pointsArrow[0].X != m_pointsArrow[1].X || m_pointsArrow[0].Y != m_pointsArrow[1].Y)
                    {
                        //e.Graphics.DrawLine(new Pen(Color.Red, 3), m_pointsArrow[0], m_pointsArrow[1]);
                        //bmpArrowDraw_Temp = new Bitmap(pdCapture.Width, pdCapture.Height, e.Graphics);
                    }
                }
            }


            //m_image = new Bitmap(pdCapture.Width, pdCapture.Height, e.Graphics);
            
            
            

            

            

            m_image = new Bitmap(pdCapture.Width, pdCapture.Height, G1);

            G1.Dispose();


            if (stat_tick_start == 0) stat_tick_start = DateTime.Now.Ticks;
            //interval 
            long interval_in_ticks = System.DateTime.Now.Ticks - stat_tick_start;
            //dateValue.ToString("MM/dd/yyyy hh:mm:ss.fff tt"))
            string s_interval_in_ms = System.DateTime.FromBinary(interval_in_ticks).ToString("hh:mm:ss.fff");
            Debug.WriteLine(stat_counter + " "+ s_interval_in_ms);
                
            
            stat_counter++;

            return;

            #region Not USED
           // Graphics gOriginal = e.Graphics;// Graphics.FromImage(image)
            
            //try
            //{
            //    using (Graphics g = Graphics.FromImage(bmp))
            //    {
            //        for (int x = 0; x < image.Width; x++)
            //        {
            //            if (x != shift_left && x != (image.Width - shift_right))
            //            {
            //                if (x < shift_left || x > (image.Width - shift_right))
            //                {
            //                    bDrawDot = false; bDrawVline = false;
            //                    continue;
            //                }
            //                else if (x == shift_left || x == (image.Width - shift_right))
            //                {
            //                    bDrawDot = false; bDrawVline = !false;
            //                }
            //                else if (x > shift_left && x < (image.Width - shift_right))
            //                {
            //                    bDrawDot = !false; bDrawVline = false;
            //                }
            //            }

            //            if (bDrawDot == false && bDrawVline == false)
            //                continue;


            //            tempX = x;
            //            if (tempX == 256)
            //            {
            //            }

            //            for (int y = 0; y < image.Height; y++)
            //            {
            //                tempY = y;

            //                if (bDrawDot == !false && bDrawVline == false)
            //                {
            //                    if ((y == shift_top) || (y == (image.Height - shift_bottom)))
            //                    {
            //                        Color colour = bmp.GetPixel(x, y);
            //                        brush.Color = Color.FromArgb(transparency, colour.R, colour.G, colour.B);
            //                        g.FillRectangle(brush, x, y, 1, 1);
            //                    }

            //                }
            //                else if (bDrawDot == !false && bDrawVline == !false)
            //                {
            //                    if ((y >= shift_top) || (y <= (image.Height - shift_bottom)))
            //                    {
            //                        Color colour = bmp.GetPixel(x, y);
            //                        brush.Color = Color.FromArgb(transparency, colour.R, colour.G, colour.B);
            //                        g.FillRectangle(brush, x, y, 1, 1);
            //                    }
            //                }





            //            }
            //        }
            //    }
                

            //    pdCapture.Image = bmp;
            //}
            //catch (Exception ex)
            //{

            //    //throw;
            //}
            #endregion

            #region NOT USED
            // Create array of two points.
            //  Point[] points = { new Point(0, 0), new Point(100, 50) };
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
            #endregion
        }

        #region repaint event statistics
        public long  stat_counter { get; set; }
        public long stat_tick_start { get; set; }


        #endregion
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            if (MouseDownPictBox)
            {
                if (!bClearMouseDraw)
                {

                    CursorPositionmyX = e.X;
                    CursorPositionmyY = e.Y;

                    int id = segment_line_id;

                    if (!bDraw_Arrow)
                    {
                        if (dict_points.Count > 0)
                            if (dict_points[id] != null)
                                dict_points[id].Add(e.Location);
                    }

                    m_pointsArrow[1].X= CursorPositionmyX;
                    m_pointsArrow[1].Y = CursorPositionmyY;
                }
                else
                {
                    bClearMouseDraw = false;
                    //myPointList.Clear();
                    //pdCapture.Invalidate(); //force a repaint
                    //myPointList.Add(e.Location);
                   // myPointList.Add(e.Location);
                }
                pdCapture.Refresh(); //force a repaint

            }
        }
        // short keys main form
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.C)
            {
                m_Delegate_fn_show_capture_form.Invoke();
            }
        }
        private void refreshPictureboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pdCapture.Refresh();
        }

        bool _bActionOnMouseUp;
        public bool bActionOnMouseUp
        { 
            get
            {
                return _bActionOnMouseUp;
            }
            set
            {
                _bActionOnMouseUp = value;

                if (_bActionOnMouseUp)
                {
                    _bActionOnMouseUp = false;

                    if (pdCapture.Image != null)
                    {
                        //pdCapture.Image = new Bitmap(pdCapture.Width, pdCapture.Height, e.Graphics);

                        

                        Bitmap im = (Bitmap)pdCapture.Image.Clone();// new Bitmap(pdCapture.Width, pdCapture.Height, e.Graphics);
                        SetImageInPicturebox(im, true);
                    }

                }
            }
        }
        private void pdCapture_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDownPictBox = false;

          //  m_pointsArrow[1].X = CursorPositionmyX= e.X;
          //  m_pointsArrow[1].Y = CursorPositionmyY = e.Y;

            if ((m_pointsArrow[1].X == m_pointsArrow[0].X) && (m_pointsArrow[1].Y == m_pointsArrow[0].Y))
                // do not act if position didn't change
                bActionOnMouseUp = !true;
            else
            {
                bActionOnMouseUp = true;
                
            }
            pdCapture.Refresh();
        }
        private void ClearMouseDraw()
        {
            //
            bClearMouseDraw = false;
            if (true)
            {
                segment_line_id = 0;
                dict_points.Clear();

                segment_arrow_id = 0;
                //dict_arrows.Clear();
                dict_arrows_.Clear();

                //myPointList.Clear();
                // m_image = BmpCaptured;
                pdCapture.Image = BmpCaptured;
                pdCapture.Refresh();
                //pdCapture.Invalidate(); //force a repaint
            }
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            bClearMouseDraw = true;
            ClearMouseDraw();
        }
        private void chboxDrawArrow_CheckedChanged(object sender, EventArgs e)
        {
            bDraw_Arrow = (sender as CheckBox).Checked==true;
            pboxArrow.Refresh();
        }
        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            M_clearDrawsActive = true;
            bClearMouseDraw = true;
            ClearMouseDraw();
            if (m_image != null && BmpCaptured != null)
            {
                // m_image = BmpCaptured;
                SetImageInPicturebox((Bitmap)BmpCaptured.Clone(), false);
            }
        }
        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            M_clearDrawsActive = !true;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            #region pickup color from dialog
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                clrArrow = colorDialog1.Color;
                chboxDrawArrow.BackColor = clrArrow;
                pboxArrow.Refresh();
            }
            #endregion
        }
        private void button8_Click(object sender, EventArgs e)
        {
            pdCapture.Image = null;
            m_image = null;
            pdCapture.Refresh();
        }
        private void pboxArrow_Paint(object sender, PaintEventArgs e)
        {
            Point[] arrow = new Point[2];
            arrow[0].X = 0; arrow[0].Y = 0;
            arrow[1].X = pboxArrow.Width; arrow[0].Y = pboxArrow.Height-5;
            Pen pen = new Pen(clrArrow, 6);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.NoAnchor;
            e.Graphics.DrawLine(pen, arrow[1], arrow[0]);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            m_undoManager.Clean();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            this.Location = Properties.Settings.Default.MainformStartPosition;
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            
            if (this.WindowState == FormWindowState.Normal)
            {
                // save location and size if the state is normal
                Properties.Settings.Default.MainformStartPosition = this.Location;
            }
            else
            {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.MainformStartPosition = this.RestoreBounds.Location;
            }

            // don't forget to save the settings
            Properties.Settings.Default.Save();
        }

        private void openSaveFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath=Properties.Settings.Default.DefaultFolder;
            fbd.ShowDialog();
        }

        private void openSaveFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFolder(Properties.Settings.Default.DefaultFolder);
        }

        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
        }

        private void lunchWpfFormToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            refreshToolStripMenuItem.Enabled = frmDraggable != null;
        }
    }
}