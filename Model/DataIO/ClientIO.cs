using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model.DataIO
{
    internal class ClientIO
    {
        private static string ClientsDataPath = "../../Data/Clients.txt";
        private static List<Client> Clients;
        private static List<string> ClientsTXT;
        public static List<Client> LoadClients()
        {
            List<Client> clients = new List<Client>();

            string[] lines = System.IO.File.ReadAllLines(ClientsDataPath);
            ClientsTXT = new List<string>(lines);
            foreach (string line in lines)
            {
                string[] data = line.Split('|');
                Client client = new Client
                {
                    Name = data[0],
                    Surname = data[1],
                    Email = data[2],
                    DateofBirth = DateTime.ParseExact(data[3], "dd/MM/yyyy", null),
                    Password = data[4]
                };
                clients.Add(client);

            }

            Clients = clients;
            return clients;
        }

        public static void AddClient(Client c)
        {
            Clients.Add(c);
            ClientsTXT.Add(c.ToDataString());
            System.IO.File.WriteAllText(ClientsDataPath, String.Join("\n", ClientsTXT));
        }

        public static bool ClientExists(string email, string password)
        {
            bool result = false;

            foreach (Client client in Clients)
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
            foreach (Client c in Clients)
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
