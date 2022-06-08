using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerbRailway.Model.Interfaces;

namespace SerbRailway.Model
{
    internal class Client : IUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Password { get; set; }

        public string ToDataString()
        {
            return Name + "|" + Surname + "|" + Email + "|" + DateofBirth.ToString("dd/MM/yyyy") + "|" + Password;
        }

        public Client(string name, string surname, string email, DateTime dateofBirth, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateofBirth = dateofBirth;
            Password = password;
        }

        public Client() { }
    }
}
