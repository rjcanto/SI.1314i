using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDPLib;

namespace PDPLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PDP lib = new PDP();

            foreach (var u in lib.getUsersWithPermission(null))
            {
                Console.WriteLine(u.ToString());
            }
        }
    }
}
