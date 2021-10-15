using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureOnTop.CustomTypes
{
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
}
