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
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace Calc.data
{
    public class HistoryModel : System.Collections.Specialized.INotifyCollectionChanged
    {
        private ObservableCollection<string> _hc;

        public ObservableCollection<string> histData { get { return _hc; } }

        public HistoryModel() { _hc = new ObservableCollection<string>(); }

        public void loadHistory()
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(App.ISO_HIST_XML, FileMode.Open, isf))
                {
                    XDocument doc = XDocument.Load(stream);
                    var ans = from r in doc.Element("history").Descendants("item") select r;
                    if (ans.Count() > 0)
                    {
                        foreach (var i in ans) { _hc.Add(i.Value); } if (CollectionChanged != null) CollectionChanged(this, null);
                    }
                    stream.Close();
                }
            }
        }

        public void addEntry(string s)
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = new IsolatedStorageFileStream(App.ISO_HIST_XML, FileMode.Open, storage),
                                                 temp = new IsolatedStorageFileStream(App.ISO_HIST_TEMP_XML, FileMode.Create, storage))
                {
                    XDocument doc = XDocument.Load(file);
                    XElement el = new XElement("item", s);
                    var els = from row in doc.Root.Descendants("item") select row;
                    if (els.Count() >= 50) { doc.Root.LastNode.Remove(); }
                    doc.Root.AddFirst(el);
                    doc.Save(temp);
                    file.Close();
                    temp.Close();
                    _hc.Insert(0, s);
                    if (CollectionChanged != null) CollectionChanged(this, null);
                }
                storage.DeleteFile(App.ISO_HIST_XML);
                storage.MoveFile(App.ISO_HIST_TEMP_XML, App.ISO_HIST_XML);
            }
        }

        public void clearHistory()
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = new IsolatedStorageFileStream(App.ISO_HIST_XML, FileMode.Open, storage),
                                                 temp = new IsolatedStorageFileStream(App.ISO_HIST_TEMP_XML, FileMode.Create, storage))
                {
                    XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", null), new XElement("history"));

                    doc.Save(temp);
                    file.Close();
                    temp.Close();
                    _hc.Clear();
                    if (CollectionChanged != null) CollectionChanged(this, null);
                }
                storage.DeleteFile(App.ISO_HIST_XML);
                storage.MoveFile(App.ISO_HIST_TEMP_XML, App.ISO_HIST_XML);
            }
        }

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
