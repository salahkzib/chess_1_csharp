using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    internal class test3
    {
        public static void Main11()
        {
            int[] nums = { 1, 2, 3, 4, 5 };
            try
            {
                nums[0] = 999;
                nums[10] = 10;
            }
            catch
            {
                
            }
            for(int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i]);
            }
        }
    }
}
