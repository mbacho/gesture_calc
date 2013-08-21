using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Calc.data
{
    public class Vector
    {
        public Point start { get; set; }
        public Point stop { get; set; }
        public static enum DIRECTIONS { NONE,NORTH, EAST, WEST, SOUTH, NEAST, NWEST, SEAST, SWEST };
        public DIRECTIONS direction { get; set; }
        public double angle { get; set; }
    }
}
