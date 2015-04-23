using Kentor.AuthServices.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kentor.AuthServices
{
    /// <summary>
    /// Factory for Saml2SecurityTokenHandler.
    /// </summary>
    public static class Saml2SecurityTokenHandlerFactory
    {
        /// <summary>
        /// Create a security token handler capable of handling assertions
        /// received under the given configuration.
        /// </summary>
        /// <param name="options">Configuration</param>
        /// <returns>Saml2SecurityTokenHandler</returns>
        public static Saml2SecurityTokenHandler Create(IOptions options)
        {
            return new Saml2SecurityTokenHandler()
            {
                Configuration = new SecurityTokenHandlerConfiguration()
                {

                }
            };
        }
    }
}
