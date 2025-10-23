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
        static void Main(string[] args)
        {
            string pa = @"C:\Users\kzibs\Desktop\projects\chess\chess_main_project\tests\TextFile1.txt";
            string[] lines = File.ReadAllLines(pa);
            string moha = "moha";
            File.AppendAllLines(pa, new string[] { moha });

            string tx = lines[0].Trim();
            Console.WriteLine(tx);
        }
    }
}
