using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests;
using System.IO;

namespace tests
{
    public class Program
    {
        public void tst()
        {

            string text = "Hello, World!";
            string path = "C:\\Users\\kzibs\\Desktop\\test.txt";
            using (File.Create(path)) ;
            StreamWriter s = new StreamWriter(path);
            s.WriteLine(text);
        }
        static void Main(string[] args)
        {
            int i = 0;
            int[] arr = new int[5];
            while (i < arr.Length & 1 != arr[i])
                i++;
        }
    }
}
