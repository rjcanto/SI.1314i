using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IdentityModel;
using System.Security.Claims;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.IdentityModel.Services;
using System.Security.Principal;

namespace Serie2_IdentityProvider
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityTokenServiceConfiguration conf = new Serie2SecurityTokenServiceConfiguration();
            // simulate successful authentication
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity("jose"), new String[0]);
            // Create security token service instance 
            SecurityTokenService sts = conf.CreateSecurityTokenService();
            // Use facade class FederatedPassiveSecurityTokenServiceOperations to execute WS-Federation protocol
            FederatedPassiveSecurityTokenServiceOperations.ProcessRequest(
                HttpContext.Current.Request,
                HttpContext.Current.User as ClaimsPrincipal,
                new Serie2SecurityTokenService(new SecurityTokenServiceConfiguration("ClaimsProducer-Localhost")),
                HttpContext.Current.Response);

        }
    }

    public class Serie2SecurityTokenService : SecurityTokenService
    {
        public Serie2SecurityTokenService(SecurityTokenServiceConfiguration serie2SecurityTokenServiceConfiguration) :
            base(serie2SecurityTokenServiceConfiguration)
        {

        }

        protected override System.Security.Claims.ClaimsIdentity GetOutputClaimsIdentity(System.Security.Claims.ClaimsPrincipal principal, System.IdentityModel.Protocols.WSTrust.RequestSecurityToken request, Scope scope)
        {
            ClaimsIdentity outgoingIdentity = new ClaimsIdentity();
            outgoingIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Student"));
            return outgoingIdentity;
            
        }

        protected override Scope GetScope(System.Security.Claims.ClaimsPrincipal principal, System.IdentityModel.Protocols.WSTrust.RequestSecurityToken request)
        {
            Scope scope = new Scope(request.AppliesTo.Uri.AbsoluteUri);
            scope.TokenEncryptionRequired = false;
            scope.SymmetricKeyEncryptionRequired = false;
            return scope;
        }
    }

    public class Serie2SecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {         
        public Serie2SecurityTokenServiceConfiguration()
            : base("SI1314i-LI51N-Grupo2")
        {
            this.SecurityTokenService = typeof(Serie2SecurityTokenService);
        }

    }

}