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
    public class GestureAnalyser
    {
        public Vector generalDirection(Point start, Point end)
        {
            double x_diff = start.X - end.X, y_diff = start.X - end.X, angle = 0;
            Vector.DIRECTIONS direction = Vector.DIRECTIONS.NONE;
            if (x_diff < 0) //going left
            {
                if (y_diff < 0) //going down
                { direction = Vector.DIRECTIONS.SWEST; }
                else if (y_diff > 0) //going up
                { direction = Vector.DIRECTIONS.NWEST; }
                else if (y_diff == 0) //going neither up nor down
                { direction = Vector.DIRECTIONS.WEST; }
            }
            else if (x_diff > 0)//going right
            {
                if (y_diff < 0)  //going down
                { direction = Vector.DIRECTIONS.SEAST; }
                else if (y_diff > 0)  //going up
                { direction = Vector.DIRECTIONS.NEAST; }
                else if (y_diff == 0)  //going neither
                { direction = Vector.DIRECTIONS.EAST; }
            }
            else if (x_diff == 0)//going neither right nor left
            {
                if (y_diff < 0)  //going down
                { direction = Vector.DIRECTIONS.SOUTH; }
                else if (y_diff > 0)  //going up
                { direction = Vector.DIRECTIONS.NORTH; }
                else if (y_diff == 0)  //going neither
                { direction = Vector.DIRECTIONS.NONE; }
            }
            angle = getAngle(start, end);
            return new Vector { direction = direction, start = start, stop = end, angle = angle };
        }

        private double getAngle(Point start, Point end) 
        { 
            //TODO Code to get angle
            return 0.0; 
        }
    }
}
