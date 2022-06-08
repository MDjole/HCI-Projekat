using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SerbRailway.Model.DataIO;
using SerbRailway.Model;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        RegistrationWindow rg = new RegistrationWindow();
        
        
        public LoginWindow()
        {
            ClientIO.LoadClients();
            ManagerIO.LoadClients();
            InitializeComponent();
            textBoxEmail.Focus();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Unesite email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Unesite validni email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                Client c = ClientIO.ClientExists(email, password);
                if (ManagerIO.ManagerExists(email, password))
                {
                    ManagerWindow mw = new ManagerWindow();
                    mw.Show();
                    Close();
                } else if (c != null)
                {
                    ClientWindow cw = new ClientWindow(c);
                    cw.Show();
                    Close();
                } else
                {
                    errormessage.Text = "Kredencijali profila nisu validni.";
                    textBoxEmail.Focus();
                }
                
                
            }
        }
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            rg.Show();
            Close();
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            button1_Click(sender, e);
        }
    }
}
