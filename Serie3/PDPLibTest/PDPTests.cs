using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PDPLib;

namespace PDPLibTest
{
    [TestFixture]
    public class PDPTests
    {

        [TestFixtureSetUp]
        public void Setup()
        {
            PDP.ConnStringName = "Local";
        }

        [Test]
        public void GetRolesOExistingfUsersSucceeds()
        {
            var pdp = new PDP();
            
            // Admin
            Assert.That(pdp.getRolesOfUser("Ricardo").Select(role => role.RoleName), Is.EquivalentTo(new[] {"Admin", "Director", "User", "Manager", "Guest"}));

            // Director
            Assert.That(pdp.getRolesOfUser("Luís").Select(role => role.RoleName), Is.EquivalentTo(new[] { "Director", "User", "Guest" }));

            // Manager
            Assert.That(pdp.getRolesOfUser("Pedro").Select(role => role.RoleName), Is.EquivalentTo(new[] { "Manager", "User", "Guest" }));

            // Auditor
            Assert.That(pdp.getRolesOfUser("João").Select(role => role.RoleName), Is.EquivalentTo(new[] { "Auditor", "User", "Guest" }));

            // User
            var users = new[] {"Lídia", "Teresa", "Sara", "Elsa", "Mário", "Cristina"};
            foreach (var user in users)
            {
                Assert.That(pdp.getRolesOfUser(user).Select(role => role.RoleName), Is.EquivalentTo(new[] { "User", "Guest" }));
            }

            // Guest
            var guests = new[] { "Maria", "Miguel" };
            foreach (var guest in guests)
            {
                Assert.That(pdp.getRolesOfUser(guest).Select(role => role.RoleName), Is.EquivalentTo(new[] { "Guest" }));
            }
        }

        [Test]
        public void GetRolesOfUnknownUserReturnsEmptyList()
        {
            var pdp = new PDP();

            Assert.That(pdp.getRolesOfUser("Unknown").Select(role => role.RoleName), Is.EquivalentTo(new string[] {}));
        }

        [Test]
        public void GetResourcesOfAuthorizedUserSucceeds()
        {
            const string updateAction = "Alterar ficheiros", insertAction = "Criar ficheiros e pastas", executeAction = "Executar ficheiros";
            
            var pdp = new PDP();

            // Admin
            Assert.That(
                pdp.getResourcesOfAuthorizedUser("Ricardo", updateAction).Select(resource => resource.ResourceName),
                Is.EquivalentTo(new[]
                    {"/folder", "/folder/file1.txt", "/folder/file2.txt"}));

            Assert.That(
                pdp.getResourcesOfAuthorizedUser("Ricardo", executeAction).Select(resource => resource.ResourceName),
                Is.EquivalentTo(new[] { "/folder", "/program.exe" }));

            // Director
            Assert.That(
                pdp.getResourcesOfAuthorizedUser("Luís", executeAction).Select(resource => resource.ResourceName),
                Is.EquivalentTo(new[] { "/folder", "/program.exe" }));

            // Guest
            Assert.That(
                pdp.getResourcesOfAuthorizedUser("Maria", executeAction).Select(resource => resource.ResourceName),
                Is.EquivalentTo(new[] { "/program.exe" }));
        }

        [Test]
        public void GetResourcesOfUnknownUserReturnsEmptyList()
        {
            var pdp = new PDP();

            Assert.That(pdp.getResourcesOfAuthorizedUser("Unknown", "Executar ficheiros").Select(resource => resource.ResourceName), Is.EquivalentTo(new string[] { }));
        }

        [Test]
        public void GetResourcesForUnknownThrowsActionNotFoundException()
        {
            var pdp = new PDP();

            Assert.That(() => pdp.getResourcesOfAuthorizedUser("Unknown user", "Unknown action"), Throws.TypeOf<ActionNotFoundException>());
        }

        [Test]
        public void GetPermissionsForUnknownUserReturnsEmptyList()
        {
            var pdp = new PDP();

            Assert.That(pdp.getPermissionsOfUser("Unknown user"), Is.EquivalentTo(new string[] { }));
        }

        [Test]
        public void IsUserAuthorizedForUnknownActionThrowsActionNotFoundException()
        {
            var pdp = new PDP();

            Assert.That(() => pdp.IsUserAuthorized("Ricardo", "unknown action", "/folder"), Throws.TypeOf<ActionNotFoundException>());
        }

        [Test]
        public void IsUserAuthorizedForUnknownResourceThrowsResourceNotFoundException()
        {
            var pdp = new PDP();

            Assert.That(() => pdp.IsUserAuthorized("Ricardo", "Executar ficheiros", "unknown resource"), Throws.TypeOf<ResourceNotFoundException>());
        }

        [Test]
        public void IsUserAuthorizedForUnknownUserReturnsFalse()
        {
            var pdp = new PDP();

            Assert.That(pdp.IsUserAuthorized("Unknown user", "Executar ficheiros", "/folder"), Is.False);
        }

        [Test]
        public void GetActionsAllowedForUnknownResourceThrowsResourceNotFoundException()
        {
            var pdp = new PDP();

            Assert.That(() => pdp.getActionsAllowedOfUserWithResource("Ricardo", "unknown resource"), Throws.TypeOf<ResourceNotFoundException>());
        }

        [Test]
        public void GetActionsAllowedForUnknownUserReturnsEmptyList()
        {
            var pdp = new PDP();

            Assert.That(pdp.getActionsAllowedOfUserWithResource("Unknown user", "/folder"), Is.EquivalentTo(new string[] {}));
        }

        [Test]
        public void GetActionsAllowedForExistingUserAndResourceSucceeds()
        {
            var pdp = new PDP();

            Assert.That(pdp.getActionsAllowedOfUserWithResource("João", "/folder").Select(action => action.ActionName),
                        Is.EquivalentTo(new[]
                            {
                                "Ver o conteúdo de ficheiros e listar conteúdo de pastas",
                                "Criar ficheiros e pastas",
                                "Executar ficheiros"
                            }));

            Assert.That(pdp.getActionsAllowedOfUserWithResource("Pedro", "/folder/file3.txt").Select(action => action.ActionName),
                       Is.EquivalentTo(new[] { "Ver o conteúdo de ficheiros e listar conteúdo de pastas" }));
        }

        [Test]
        public void GetRolesForUnknownUserReturnsEmptyList()
        {
            var pdp = new PDP();

            Assert.That(pdp.getRolesOfUser("unknown user"), Is.EquivalentTo(new string[] { }));
        }

        [Test]
        public void GetRolesForExistingUserSucceeds()
        {
            var pdp = new PDP();

            Assert.That(pdp.getRolesOfUser("Ricardo").Select(role => role.RoleName),
                Is.EquivalentTo(new[] {"Admin", "Manager", "Director", "User", "Guest"}));
        }

        [Test]
        public void GetUsersForExistingRoleSucceeds()
        {
            var pdp = new PDP();

            Assert.That(pdp.getUsersWithRole("Admin").Select(user => user.UserName),
                Is.EquivalentTo(new[] { "Ricardo"}));

            Assert.That(pdp.getUsersWithRole("User").Select(user => user.UserName),
                Is.EquivalentTo(new[] { "Teresa", "Lídia", "Sara", "Elsa", "Mário", "Cristina" }));
        }

        [Test]
        public void GetUsersForUnknownRoleReturnsEmptyList()
        {
            var pdp = new PDP();

            Assert.That(pdp.getUsersWithRole("Unknown role").Select(user => user.UserName),
                Is.EquivalentTo(new string[] {}));
        }

        [Test]
        public void GetUsersWithPermissionThrowsActionNotFoundForUnknownAction()
        {
            var pdp = new PDP();

            Assert.That(() => pdp.getUsersWithPermission("Unknown action", "/folder"), Throws.TypeOf<ActionNotFoundException>());
        }

        [Test]
        public void GetUsersWithPermissionThrowsResourceNotFoundForUnknownResource()
        {
            var pdp = new PDP();

            Assert.That(() => pdp.getUsersWithPermission("Executar ficheiros", "Unknown resource"), Throws.TypeOf<ResourceNotFoundException>());
        }
    }
}
