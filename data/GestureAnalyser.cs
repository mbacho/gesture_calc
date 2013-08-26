using System.Windows;

namespace Calc.data
{
    public class GestureAnalyser
    {
        public char probableChar(Vector v)
        {
            switch (v.direction)
            {
                case Vector.DIRECTIONS.NONE: return '~';
                case Vector.DIRECTIONS.NORTH: return '1';
                case Vector.DIRECTIONS.NEAST: return '1';
                case Vector.DIRECTIONS.NWEST: return '1';
                case Vector.DIRECTIONS.SOUTH: return '1';
                case Vector.DIRECTIONS.SEAST: return '1';
                case Vector.DIRECTIONS.SWEST: return '1';
                case Vector.DIRECTIONS.WEST: return '~';
                case Vector.DIRECTIONS.EAST: return '~';
                default: return '~';
            }
        }

        public Vector getVector(Point start, Point end)
        {
            double x_diff = end.X - start.X, y_diff = end.X - start.X, angle = 0;
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
