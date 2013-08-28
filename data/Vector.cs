using System;
using System.Windows;

namespace Calc.data
{
    /**
     * There is an XNA Vector2 data structure
     */
    public class Vector
    {
        public Point start { get; set; }
        public Point stop { get; set; }
        public enum DIRECTIONS { NONE, NORTH, EAST, WEST, SOUTH, NEAST, NWEST, SEAST, SWEST };
        public DIRECTIONS direction { get; set; }
        public double angle { get; set; }
        public double length
        {
            get
            {
                try
                {
                    double x_diff = start.X - stop.X;
                    double y_diff = start.Y - stop.Y;
                    return Math.Sqrt((x_diff * x_diff) + (y_diff + y_diff));
                }
                catch (Exception ex) { throw new Exception("Calculation failed"); }
            }
        }
    }
}
