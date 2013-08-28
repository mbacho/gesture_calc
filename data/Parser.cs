using System;
using System.Collections.Generic;
using System.Text;

namespace Calc.data
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }

    public class Parser
    {
        public double evaluate(string exp)
        {
            try
            {
                string[] postfix = infixToPostfix(exp);
                return evaluatePostFix(postfix);
            }
            catch (Exception ex) { throw new ParserException("Invalid expression"); }
        }

        //evaluates infix expression to a space separated postfix expression
        public string[] infixToPostfix(string infix)
        {
            Stack<string> tokens = new Stack<string>();
            Stack<char> ops = new Stack<char>();
            StringBuilder sb = new StringBuilder();
            foreach (char i in infix)
            {
                if ((i >= '0' && i <= '9') || i == '.')
                {
                    sb.Append(i);
                    continue;
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        tokens.Push(sb.ToString());
                        sb.Clear();
                    }
                }

                if (i == '(')
                {
                    ops.Push(i);
                }
                else if (i == ')')
                {
                    while (ops.Count > 0 && ops.Peek() != '(')
                    {
                        tokens.Push(ops.Pop().ToString());
                    } if (ops.Count > 0) ops.Pop();
                }
                else if (i == '+' || i == '-' || i == '*' || i == '/')
                {
                    bool not_ok = true;
                    while (not_ok)
                    {
                        if (ops.Count == 0) { ops.Push(i); not_ok = false; }
                        else if (ops.Peek() == '(') { ops.Push(i); not_ok = false; }
                        else if (higherPrecedence(i, ops.Peek())) { ops.Push(i); not_ok = false; }
                        else { tokens.Push(ops.Pop().ToString()); not_ok = true; }
                    }
                }
            }
            if (sb.Length > 0) tokens.Push(sb.ToString());
            if (ops.Count > 0)
            {
                while (ops.Count > 0) { tokens.Push(ops.Pop().ToString()); }
            }
            string[] vals = tokens.ToArray();
            Array.Reverse(vals);
            return vals;
            //return String.Join(" ", vals);
        }

        private bool higherPrecedence(char i, char p)
        {
            //BODMAS
            if (i == '/' || p == '-') return true;
            if (i == '-' || p == '/') return false;
            if (i == '*' && p == '+') return true;
            return false;
        }

        private double evaluatePostFix(string[] postfix)
        {
            Stack<string> nums = new Stack<string>();
            for (int i = 0; i < postfix.Length; i++)
            {
                string s = postfix[i];
                if (s.Equals("+")) { double b = Double.Parse(nums.Pop()), a = Double.Parse(nums.Pop()); nums.Push((a + b).ToString()); }
                else if (s.Equals("-")) { double b = Double.Parse(nums.Pop()), a = Double.Parse(nums.Pop()); nums.Push((a - b).ToString()); }
                else if (s.Equals("*")) { double b = Double.Parse(nums.Pop()), a = Double.Parse(nums.Pop()); nums.Push((a * b).ToString()); }
                else if (s.Equals("/")) { double b = Double.Parse(nums.Pop()), a = Double.Parse(nums.Pop()); nums.Push((a / b).ToString()); }
                else { nums.Push(s); }
            }
            return Double.Parse(nums.Pop());
        }
    }
}
