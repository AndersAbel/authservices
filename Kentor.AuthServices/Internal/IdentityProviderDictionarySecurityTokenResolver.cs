using Kentor.AuthServices.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kentor.AuthServices.Internal
{
    class IdentityProviderDictionarySecurityTokenResolver : SecurityTokenResolver
    {
        IdentityProviderDictionary idpDictionary;

        public IdentityProviderDictionarySecurityTokenResolver(IdentityProviderDictionary idpDictionary)
        {
            this.idpDictionary = idpDictionary;
        }

        protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
        {
            key = keyIdentifierClause.CreateKey();
            return true;
        }

        protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
        {
            throw new NotImplementedException();
        }

        protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
        {
            throw new NotImplementedException();
        }
    }
}
