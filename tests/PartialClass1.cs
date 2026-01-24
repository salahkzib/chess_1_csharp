using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    internal partial class PartialClass1
    {
        public static void Main1()
        {
            PartialClass1 pr = new PartialClass1();
            pr.Login("admin", "other1admin");
            pr.Display();
        }
        public void Display()
        {
            Console.WriteLine("Username: " + username);
            Console.WriteLine("Password: " + password);
        }
    }
}
