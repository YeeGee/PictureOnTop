using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DraggableForm
{
    public class FormBase : Form
    {
        #region broadcast event

        public event EventHandler OnDragging;
        public event EventHandler OnCaptureRequest;
        public event EventHandler OnShowMainFormRequest;
        public event EventHandler OnShift;

        #endregion

        #region Declarations
        public bool drag  { get; private set; }

        private Point start_point = new Point(0, 0);

        private bool draggable = true;
        private string exclude_list = "";

        /// <span class="code-SummaryComment"><SUMMARY></span>
        /// Required designer variable.
        /// <span class="code-SummaryComment"></SUMMARY></span>
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Constructor , Dispose

        public FormBase()
        {
            InitializeComponent();

            //
            //Adding Mouse Event Handlers for the Form
            //
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            this.KeyDown += FormBase_KeyDown;
            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 16)
            {
                bShift = true;
                if (OnShift != null)
                    OnShift(true, e);
            }
            else
            {
                bShift = !true;
                if (OnShift != null)
                    OnShift(false, e);
            }


            base.OnKeyDown(e);
        }

        private void FormBase_KeyDown(object sender, KeyEventArgs e)
        {


        }

        /// <span class="code-SummaryComment"><SUMMARY></span>
        /// Clean up any resources being used.
        /// <span class="code-SummaryComment"></SUMMARY></span>
        /// true if managed resources should be disposed; otherwise, false.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <span class="code-SummaryComment"><SUMMARY></span>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// <span class="code-SummaryComment"></SUMMARY></span>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(120, 61);
            this.Name = "FormBase";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AlerterForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBase_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        #region Overriden Functions

        protected override void OnControlAdded(ControlEventArgs e)
        {
            //
            //Add Mouse Event Handlers for each control added into the form,
            //if Draggable property of the form is set to true and the control
            //name is not in the ExcludeList.Exclude list is the comma separated
            //list of the Controls for which you do not require the mouse handler 
            //to be added. For Example a button.  
            //
            if (this.Draggable && (this.ExcludeList.IndexOf(e.Control.Name) == -1))
            {
                e.Control.MouseDown += new MouseEventHandler(Form_MouseDown);
                e.Control.MouseUp += new MouseEventHandler(Form_MouseUp);
                e.Control.MouseMove += new MouseEventHandler(Form_MouseMove);
            }
            base.OnControlAdded(e);
        }

        #endregion

        #region Event Handlers

        void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (bShift)
                return;

            //
            //On Mouse Down set the flag drag=true and 
            //Store the clicked point to the start_point variable
            //
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
            if(OnDragging!=null)
            {
                OnDragging(this, new EventArgs());
            }
        }

        void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (bShift)
                return;

            //
            //Set the drag flag = false;
            //
            this.drag = false;

            if (OnDragging != null)
            {
                OnDragging(this, new EventArgs());
            }
        }

        void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (bShift)
                return;

            //
            //If drag = true, drag the form
            //
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.start_point.X,
                                     p2.Y - this.start_point.Y);
                this.Location = p3;
            }
        }

        #endregion

        #region Properties

        public string ExcludeList
        {
            set
            {
                this.exclude_list = value;
            }
            get
            {
                return this.exclude_list.Trim()  ;
            }
        }


        public bool Draggable
        {
            set
            {
                this.draggable = value;
            }
            get
            {
                return this.draggable;
            }
        }

        private int m_transparency=255;

        public int transparency
        {
            get { return m_transparency; }
            set 
            {
                m_transparency = value;
            }
        }

        public bool bShift { get; private set; }

        public void UpdateImage()
        {
            PictureBox pb = (PictureBox)this.Controls.Find("pb1", false)[0];

            Bitmap bmp = new Bitmap(pb.Image);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                   // int tolerance = Tolerance;
                    Color gotColor = bmp.GetPixel(x, y);

                    int R = gotColor.R;
                    int G = gotColor.G;
                    int B = gotColor.B;

                    Color clr = Color.FromArgb(transparency, R, G, B);
                    bmp.SetPixel(x, y, clr);
                }
            }

            pb.Image = bmp;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.Handle == msg.HWnd)
            {
                try
                {

                if (keyData == (Keys.Control | Keys.A))
                {
                    OnCaptureRequest(this, new EventArgs());
                }
                else if (keyData == (Keys.Control | Keys.S))
                {
                    OnShowMainFormRequest(this, new EventArgs());
                }

                }
                catch (Exception)
                {

                  
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        private void FormBase_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}