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
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }

    public class Parser
    {
        //char nums[] = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        //char ops = { '*', '/', '+', '-' };
        //char paren = { '(', ')' };

        private bool isValid(string exp) { return true; }
        public double evaluate(string exp) { if (isValid(exp))return 0.0; else { throw new ParserException("Invalid expression"); } }
    }
}
