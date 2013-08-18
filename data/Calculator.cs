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
    public class Calculator
    {
        public double add(double a, double b) { return a + b; }
        public double sub(double a, double b) { return a - b; }
        public double mult(double a, double b) { return a * b; }
        public double div(double a, double b) { if (b == 0)throw new DivideByZeroException("divide by zero error"); return a / b; }
        public double sqrt(double a) { return Math.Sqrt(a); }

    }
}
