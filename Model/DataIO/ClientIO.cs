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

        /// <summary>
        /// Used to write new client down to a file. Called for registration.
        /// </summary>
        /// <param name="c"></param>
        public static void AddClient(Client c)
        {
            Clients.Add(c);
            ClientsTXT.Add(c.ToDataString());
            System.IO.File.WriteAllText(ClientsDataPath, String.Join("\n", ClientsTXT));
        }

        /// <summary>
        /// Checks credentials when logging in.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Client ClientExists(string email, string password)
        {

            foreach (Client client in Clients)
            {
                if (email.Equals(client.Email) && password.Equals(client.Password))
                {
                    return client;
                }
            }

            return null;
        }

        /// <summary>
        /// Only property that needs to be unique for registration.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
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
