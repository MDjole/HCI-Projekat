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
            Uri u = new Uri(String.Format("C:/Users/LAZAR/Desktop/HCI/VelikiProjekat/HCI-Projekat/HelpPages/managerHelp.html"));
            ch = new JavascriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);

        }

        public HelpViewer(ClientWindow originator)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            string path = String.Format("{0}/HelpPages/clientHelp.htm", curDir);
            Uri u = new Uri(String.Format("C:/Users/LAZAR/Desktop/HCI/VelikiProjekat/HCI-Projekat/HelpPages/clientHelp.html"));
            ch = new JavascriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);

        }

        private void wbHelp_Navigating(object sender, EventArgs args) { }
    }
}
