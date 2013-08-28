using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Calc.data;

namespace Calc.views
{
    public partial class GesturePage : PhoneApplicationPage
    {
        public GesturePage()
        {
            InitializeComponent();

            btnGest.Click += (sender, args) => { NavigationService.GoBack(); };
            btnClearHist.Click += (sender, args) => { txtInput.Text = ""; };

            
        }

        private void lstHist_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            Point start = new Point { X = e.ManipulationOrigin.X, Y = e.ManipulationOrigin.Y };
            Point end = e.TotalManipulation.Translation;
            GestureAnalyser ga = new GestureAnalyser();
            Vector v = ga.getVector(start, end);
            char possible = ga.probableChar(v);
            txtInput.Text += possible.ToString();
        }
    }
}