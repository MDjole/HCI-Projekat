using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model.DataIO
{
    internal class ManagerIO
    {
        private static string ManagersDataPath = "../../Data/Managers.txt";
        private static List<Manager> Managers;
        private static List<string> ManagersTXT;
        public static List<Manager> LoadClients()
        {
            List<Manager> managers = new List<Manager>();

            string[] lines = System.IO.File.ReadAllLines(ManagersDataPath);
            ManagersTXT = new List<string>(lines);
            foreach (string line in lines)
            {
                string[] data = line.Split('|');
                Manager m = new Manager
                {
                    Name = data[0],
                    Surname = data[1],
                    Email = data[2],
                    DateofBirth = DateTime.ParseExact(data[3], "dd/MM/yyyy", null),
                    Password = data[4]
                };
                managers.Add(m);

            }

            Managers = managers;
            return managers;
        }

        // !!!!! Dodavanje novih menadzera nije trazeno u zadatku. !!!!!
        //
        //public static void AddManager(Manager c)
        //{
        //    Managers.Add(c);
        //    ManagersTXT.Add(c.ToDataString());
        //    System.IO.File.WriteAllText(ManagersDataPath, String.Join("\n", ManagersTXT));
        //}

        public static bool ManagerExists(string email, string password)
        {
            bool result = false;

            foreach (Manager client in Managers)
            {
                if (email.Equals(client.Email) && password.Equals(client.Password))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static bool IsEmailUnique(string email)
        {
            bool result = true;
            foreach (Manager c in Managers)
            {
                if (email.Equals(c.Email))
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
