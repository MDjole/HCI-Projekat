using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace SerbRailway
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavascriptControlHelper
    {
        ManagerWindow managerw;
        ClientWindow clientw;
        public JavascriptControlHelper(ManagerWindow w)
        {
            managerw = w;
        }

        public JavascriptControlHelper(ClientWindow w)
        {
            clientw = w;
        }

        public void RunFromJavascript(string param)
        {
            Console.Write("here");
        }
    }
}
