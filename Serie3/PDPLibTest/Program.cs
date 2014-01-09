using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDPLib;
using PDPLib.Models;

namespace PDPLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PDP.ConnStringName = "Local";
            PDP lib = new PDP();

            //isActionAllowedOfUserWithResourceTest(lib);
            getRolesOfUserTest(lib);
            Console.ReadKey();
        }

        static void isActionAllowedOfUserWithResourceTest(PDP lib)
        {
            
            String actionName = "Executar ficheiros";
            String userName = "Ricardo";
            String resourceName = "/folder";
            if (lib.isActionAllowedOfUserWithResource(actionName,userName,resourceName))
                Console.WriteLine("A acção {1} é permitida sobre o recurso {2} para o utilizador {3}.");
            else Console.WriteLine("A acção {1} NÃO é permitida sobre o recurso {2} para o utilizador {3}.");
        }

        static void getRolesOfUserTest(PDP lib)
        {
            String userName = "Ricardo";
            List<Role> result = lib.getRolesOfUser(userName);

            if (result == null)
                Console.WriteLine("O utilizador {0} não tem Roles", userName);
            else
            {
                Console.WriteLine("O utilizador {0} tem os seguintes Roles:", userName);
                foreach (Role r in result)
                    Console.WriteLine("\t {0}", r.RoleName);
            }
        }
    }
}
