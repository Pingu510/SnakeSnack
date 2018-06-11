using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeSnack.Models
{
    public class SnakeBodyPart
    {
        public double X { get; set; }
        public double Y { get; set; }
        public static double SnakePartSize { get { return 15; } set { SnakePartSize = value; } }

        public SnakeBodyPart(double startX, double startY)
        {
            X = startX;
            Y = startY;
        }
    }
}
