using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureOnTop
{
    
    public partial class ScreenForm : Form
    {
        public ScreenForm()
        {
            InitializeComponent();
        }

        public event copyToFatherTextBox copytoFather;
        public bool begin = false;
        public bool isDoubleClick = false;
        public Point firstPoint = new Point(0, 0);
        public Point secondPoint = new Point(0, 0);
        public Image cachImage = null;
        public int halfWidth = 0;
        public int halfHeight = 0;

        /*Copy the entire screen, and form fill the screen*/
        public void copyScreen()
        {
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Image img = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), r.Size);

            //Maximize form
           // this.Width = r.Width;
          // this.Height = r.Height;
           // this.Left = 0;
           // this.Top = 0;

            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            //pictureBox1.Width = r.Width;
            //pictureBox1.Height = r.Height;
            pictureBox1.BackgroundImage = img;
            cachImage = img;
            halfWidth = r.Width / 2;
            halfHeight = r.Height / 2;
      //      this.Cursor = new Cursor(GetType(), "MyCursor.cur");
        }

        private void ScreenForm_Load(object sender, EventArgs e)
        {
            copyScreen();
        }

        /*Begins when the mouse is pressed screenshots*/
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDoubleClick)
            {
                begin = true;
                firstPoint = new Point(e.X, e.Y);
                changePoint(e.X, e.Y);
                msg.Visible = true;
            }
        }
        /*Displayed when the mouse moves to intercept the border*/
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (begin)
            {
                //Redraw the background image
                secondPoint = new Point(e.X, e.Y);
                int minX = Math.Min(firstPoint.X, secondPoint.X);
                int minY = Math.Min(firstPoint.Y, secondPoint.Y);
                int maxX = Math.Max(firstPoint.X, secondPoint.X);
                int maxY = Math.Max(firstPoint.Y, secondPoint.Y);


                Image tempimage = new Bitmap(cachImage);
                Graphics g = Graphics.FromImage(tempimage);
                //Picture cropping frames
                g.DrawRectangle(new Pen(Color.Red), minX, minY, maxX - minX, maxY - minY);
                pictureBox1.Image = tempimage;
                //Calculation of coordinate information
                msg.Text = "Upper left corner coordinates：(" + minX.ToString() + "," + minY.ToString() + ")\r\n";
                msg.Text += "Lower right corner coordinates：(" + maxX.ToString() + "," + maxY.ToString() + ")\r\n";
                msg.Text += "Screenshot size：" + (maxX - minX) + "×" + (maxY - minY) + "\r\n";
                msg.Text += "Double-click end screenshots anywhere!";
                msg.Update();
                changePoint((minX + maxX) / 2, (minY + maxY) / 2);
                pictureBox1.Update();
            }
        }
        /*Dynamically adjusts the displayed location, enter the parameters for the current screen mouse position*/
        public void changePoint(int x, int y)
        {
            if (x < halfWidth)
            {
                if (y < halfHeight)
                { msg.Top = halfHeight; msg.Left = halfWidth; }
                else
                { msg.Top = 0; msg.Left = halfWidth; }
            }
            else
            {
                if (y < halfHeight)
                { msg.Top = halfHeight; msg.Left = 0; }
                else
                { msg.Top = 0; msg.Left = 0; }
            }
        }

        /*Screenshot is completed when you let go of the mouse operation */
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            begin = false;
            isDoubleClick = true;
        }
        /*When I double-click a screenshot when notifying parent form complete screenshot action, while closing the form*/
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (firstPoint != secondPoint)
            {
                int minX = Math.Min(firstPoint.X, secondPoint.X);
                int minY = Math.Min(firstPoint.Y, secondPoint.Y);
                int maxX = Math.Max(firstPoint.X, secondPoint.X);
                int maxY = Math.Max(firstPoint.Y, secondPoint.Y);
                Rectangle r = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                copytoFather(r);
            }
            this.Close();
            //msg.Text = r.ToString();
        }

    }
}
