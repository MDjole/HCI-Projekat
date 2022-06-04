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

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        RegistrationWindow rg = new RegistrationWindow();
        ManagerWindow mw = new ManagerWindow(); 
        public LoginWindow()
        {
            ClientIO.LoadClients();
            ManagerIO.LoadClients();
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if (ManagerIO.ManagerExists(email, password))
                {
                    mw.Show();
                    Close();
                } else if (ClientIO.ClientExists(email, password))
                {
                    // Redirect to client's home screen.
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
    }
}
