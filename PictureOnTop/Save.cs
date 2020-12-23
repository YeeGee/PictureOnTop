using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureOnTop
{
    public partial class Save : Form
    {
        public Save()
        {
            InitializeComponent();
            this.pbCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        }

        Bitmap bmp;

        private Form1 m_parentForm;
        public Form1 M_parentForm
        {
            get { return m_parentForm; }
            set { m_parentForm = value; }
        }

        public Save(Int32 x, Int32 y, Int32 w, Int32 h, Size s, Form1 parent)
        {
            InitializeComponent();
            M_parentForm = parent;
            Rectangle rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);
            pbCapture.Image = bmp;
            M_parentForm.SetImageInPicturebox((Bitmap)pbCapture.Image.Clone());

            
            M_parentForm.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CheckPathExists = true;
            sfd.FileName = "Capture";
            sfd.Filter = "PNG Image(*.png)|*.png|JPG Image(*.jpg)|*.jpg|BMP Image(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pbCapture.Image.Save(sfd.FileName);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
          //  Form1 home = new frmHome();
            this.Hide();
            M_parentForm.Show();
          //  home.Show();
        }
    }
}
