using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        private JavascriptControlHelper ch;
        public HelpViewer(ManagerWindow originator)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("{0}/HelpPages/managerHelp.htm", curDir);
            Uri u = new Uri(String.Format("file:///C:/VisualStudio_Projects/HCI%20Veliki%20projekat/HCI-Projekat/HelpPages/managerHelp.htm"));
            ch = new JavascriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);

        }

        public HelpViewer(ClientWindow originator)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("{0}/HelpPages/clientHelp.htm", curDir);
            Uri u = new Uri(String.Format("file:///C:/VisualStudio_Projects/HCI%20Veliki%20projekat/HCI-Projekat/HelpPages/clientHelp.htm"));
            ch = new JavascriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);

        }
    }
}
