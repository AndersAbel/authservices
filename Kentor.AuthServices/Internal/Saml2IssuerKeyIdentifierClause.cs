using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kentor.AuthServices.Internal
{
    class Saml2IssuerKeyIdentifierClause : SecurityKeyIdentifierClause
    {
        public Saml2IssuerKeyIdentifierClause(EntityId issuer)
            : base(typeof(Saml2IssuerKeyIdentifierClause).ToString())
        {
            this.issuer = issuer;
        }

        EntityId issuer;

        public EntityId Issuer
        {
            get
            {
                return issuer;
            }
        }
    }
}
