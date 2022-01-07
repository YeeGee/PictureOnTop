using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureOnTop.CustomTypes
{

    public enum EnDrawMode { none, arrow, text }

    public class Arrow
    {
        

        // Dictionary<int, List<Point>> dict_arrows = new Dictionary<int, List<Point>>();
        public int index { get; private set;}
        public List<Point> point { get; private set; }
        public Color color { get; private set; }
        public Arrow (int a, List<Point> b, Color c)
        {
            index = a;
            point = b;
            color = c;
        }

        public int Count { get { return point.Count; } }

    }

    public class Comment
    {
        public int index { get; private set; }

        private Point m_point;
        public Point point {
            get { return m_point; } 
            set { 
                m_point = value;
                if(text!=null && text.Length>0)
                    RectTextBoundaries = new Rectangle(m_point, new Size((int)(text.Length * font.Size), font.Height));
            }
        }

        public Color color { get; private set; }
        public string text { get; private set; }

        //public bool MousePointerInsideRegion { get; private set; }
        private bool m_MousePointerInsideRegion;

        private Point m_point_mouseDown;

        public Point point_mouseDown
        {
            get
            {
                return m_point_mouseDown;
            }
            set
            {
                m_point_mouseDown = value;
                int shiftX = m_point_mouseDown.X - point.X;
                int shiftY = m_point_mouseDown.Y - point.Y;

                mouseDownOffset = new Point(shiftX, shiftY);
            }
        }

        private Point m_point_mouseUp;

        public Point point_mouseUp
        {
            get { return m_point_mouseUp; }
            set { m_point_mouseUp = value; }
        }



        public Point mouseDownOffset { get; set; }

        public bool MousePointerInsideRegion
        {
            get { return m_MousePointerInsideRegion; }
            set
            {
                if (value)
                {
                }
                m_MousePointerInsideRegion = value;
            }
        }


        public void UpdateText(string p) { 
            text = p;
            if (text != null && text.Length > 0)
                RectTextBoundaries = new Rectangle(m_point, new Size((int)(text.Length * font.Size), font.Height));

        }
        public bool IsPointInsideRegion(Point a)
        {
            MousePointerInsideRegion = RectTextBoundaries.Contains(a);
            return MousePointerInsideRegion;
        }



        public Rectangle RectTextBoundaries { get; private set; }

        public Font font { get; private set;}

        public float character_size_in_pixels { get; private set; }

        public Comment(int a, Point b, Color c, string d , Font e , float f)
        {
            index = a;
            point = b;
            color = c;
            text =  d;
            font = e;
            character_size_in_pixels = f;

            
            RectTextBoundaries = new Rectangle(point, new Size((int)(text.Length * font.Size), font.Height));
        }

        //public int Count { get { return point.Count; } }

    }
}
