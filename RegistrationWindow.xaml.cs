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
using System.Text.RegularExpressions;
using SerbRailway.Model;
using SerbRailway.Model.DataIO;

namespace SerbRailway
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
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
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                string birthday = dateOfBirth.Text;
                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Unesite lozinku.";
                    passwordBox1.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Potvrdite lozinku.";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Lozinke se moraju podudarati.";
                    passwordBoxConfirm.Focus();
                } else if (birthday.Length == 0)
                {
                    errormessage.Text = "Odaberite datum rođenja.";
                    dateOfBirth.Focus();
                }
                else
                {
                    errormessage.Text = "";
                    Client newClient = new Client
                    {
                        Name = firstname,
                        Surname = lastname,
                        Email = email,
                        Password = password,
                        DateofBirth = DateTime.Parse(birthday)
                    };
                    if (ClientIO.IsEmailUnique(email))
                    {
                        ClientIO.AddClient(newClient);
                        errormessage.Text = "Uspešno ste se registrovali!";
                    } else
                    {
                        errormessage.Text = "Nalog sa datim emailom već postoji.";
                        textBoxEmail.Focus();
                    }
                    Reset();
                }
            }
        }
    }
}
