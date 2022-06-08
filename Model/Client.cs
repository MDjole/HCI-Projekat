using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerbRailway.Model.Interfaces;

namespace SerbRailway.Model
{
    public class Client : IUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Password { get; set; }

        public Client() { }

        public Client(string Name, string Surname, string Email, DateTime DateofBirth, string Password)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Email = Email;
            this.DateofBirth = DateofBirth;
            this.Password = Password;
        }

        public string ToDataString()
        {
            return Name + "|" + Surname + "|" + Email + "|" + DateofBirth.ToString("dd/MM/yyyy") + "|" + Password;
        }

        
        public bool Equals(Client c)
        {
            return this.Email == c.Email;
        }
    }
}
