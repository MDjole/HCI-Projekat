using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbRailway.Model.Interfaces
{
    internal interface IUser
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        DateTime DateofBirth { get; set; }
        string Password { get; set; }

        string ToDataString();

    }
}
