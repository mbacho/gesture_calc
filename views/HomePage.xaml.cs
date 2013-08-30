using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Calc.data;

namespace Calc.views
{
    public partial class HomePage : PhoneApplicationPage
    {
        private bool keysHidden, isEvaluated;
        private HistoryModel hist;

        public HomePage()
        {
            InitializeComponent();
            keysHidden = isEvaluated = false;
            hist = new HistoryModel();
            hist.loadHistory();
            lstHist.DataContext = hist;
            lstHist.SelectionChanged += new SelectionChangedEventHandler(lstHist_SelectionChanged);
        }

        private void mnuGest_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/views/GesturePage.xaml", UriKind.RelativeOrAbsolute));
        }

        #region navigation
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.State["mode"] = keysHidden;
            this.State["eval"] = isEvaluated;
            this.State["input"] = txtInput.Text;
            base.OnNavigatedFrom(e);
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (this.State.Keys.Contains("mode")) { if (keysHidden != (bool)this.State["mode"]) toggleMode(); }
            if (this.State.Keys.Contains("eval")) { isEvaluated = (bool)this.State["eval"]; }
            if (this.State.Keys.Contains("input")) { txtInput.Text = (string)this.State["input"]; }
            base.OnNavigatedTo(e);
        }
        #endregion

        private void lstHist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHist.Items.Count > 0)
            {
                txtInput.Text = lstHist.SelectedItem.ToString(); isEvaluated = false;
                toggleMode();
            }
        }

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (isEvaluated) { txtInput.Text = ""; isEvaluated = false; }
            txtInput.Text += btn.Content.ToString();
        }

        private void btnOps_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (isEvaluated) { txtInput.Text = ""; isEvaluated = false; }
            txtInput.Text += btn.Content.ToString();
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = txtInput.Text;
                Parser parser = new Parser();
                double val = parser.evaluate(s);
                hist.addEntry(s);
                txtInput.Text = val.ToString();
                isEvaluated = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void toggleMode()
        {
            SlideTransition st = new SlideTransition { Mode = (keysHidden) ? SlideTransitionMode.SlideUpFadeIn : SlideTransitionMode.SlideDownFadeOut };
            ITransition it = st.GetTransition(ContentPanel);
            it.Completed += (s, eventargs) =>
            {
                it.Stop();
                if (!keysHidden)
                {
                    lstHist.Visibility = System.Windows.Visibility.Visible;
                    grdKeys.Visibility = System.Windows.Visibility.Collapsed;
                }
                try
                {
                    BitmapImage img = new BitmapImage(new Uri(((keysHidden) ? "/images/down.png" : "/images/up.png"), UriKind.Relative));
                    imgToggleKeys.Source = img;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                keysHidden = !keysHidden;
            };
            if (keysHidden) { lstHist.Visibility = System.Windows.Visibility.Collapsed; grdKeys.Visibility = System.Windows.Visibility.Visible; }
            it.Begin();
        }

        private void btnToggleKeys_Click(object sender, RoutedEventArgs e)
        {
            toggleMode();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            isEvaluated = false;
            int len = txtInput.Text.Length;
            if (len > 0)
                txtInput.Text = txtInput.Text.Substring(0, len - 1);
        }

        private void btnClearHist_Click(object sender, RoutedEventArgs e)
        {
            hist.clearHistory();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = string.Empty;
        }

        private void txtInput_TextInput(object sender, TextCompositionEventArgs e)
        {
            //change textblock style as text changes
            //
        }
    }
}