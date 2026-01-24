using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    internal partial class PartialClass1
    {
        private string username;
        private string password;
        public void Login(string user, string pwd)
        {
            username = user;
            password = pwd;
        }
    }
}
