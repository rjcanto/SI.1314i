using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDPLib;
using PDPLib.Models;
using Action = PDPLib.Models.Action;

namespace PDPLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PDP.ConnStringName = "Local";
            PDP lib = new PDP();

            //getRolesOfUserTest(lib);
            //getPermissionsOfUserTest(lib);
            //getActionsAllowedOfUserWithResourceTest(lib);
            isActionAllowedOfUserWithResourceTest(lib);
            Console.ReadKey();
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

        static void getPermissionsOfUserTest(PDP lib)
        {
            String userName = "Ricardo";
            List<Permission> result = lib.getPermissionsOfUser(userName);

            if (result == null)
                Console.WriteLine("O utilizador {0} não tem Permissões", userName);
            else
            {
                Console.WriteLine("O utilizador {0} tem os seguintes permissões:", userName);
                foreach (Permission p in result)
                    Console.WriteLine("\t Permissão {0} sobre o recurso {1}.", p.ActionId,p.ResourceId);
            }
        }

        static void getActionsAllowedOfUserWithResourceTest(PDP lib)
        {
            String userName = "Ricardo";
            String resourceName = "/folder/file1.txt";
            List<Action> result = lib.getActionsAllowedOfUserWithResource(userName,resourceName).ToList<Action>();

            if (result == null)
                Console.WriteLine("O utilizador {0} não tem Permissões", userName);
            else
            {
                Console.WriteLine("O utilizador {0} tem as seguintes acções sobre o recurso {1}:", userName, resourceName);
                foreach (Action a in result.Distinct())
                    Console.WriteLine("\t {0}", a.ActionName);
            }
        }

        static void isActionAllowedOfUserWithResourceTest(PDP lib)
        {
            String userName = "Miguel";
            String resourceName = "/folder/file1.txt";
            String actionName = "Criar ficheiros e pastas";
            if (!lib.IsUserAuthorized(userName, actionName, resourceName))
                Console.WriteLine("O utilizador {0} não tem permissão {1} sobre o recurso {2}.", userName, actionName, resourceName);
            else
                Console.WriteLine("O utilizador {0} tem permissão {1} sobre o recurso {2}.", userName, actionName, resourceName);
                
        }
    }
}