using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Kentor.AuthServices.Internal
{
    class InjectingKeyInfoSerializer : KeyInfoSerializer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Only called by framework code")]
        protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
        {
            if (reader.IsStartElement("KeyInfo", SignedXml.XmlDsigNamespaceUrl))
            {
                return base.ReadKeyIdentifierCore(reader);
            }
            else
            {
                return CreateFakeKeyIdentifier();
            }
        }

        private SecurityKeyIdentifier CreateFakeKeyIdentifier()
        {
            return new SecurityKeyIdentifier(
                new Saml2IssuerKeyIdentifierClause(Issuer));
        }

        protected override bool CanReadKeyIdentifierCore(System.Xml.XmlReader reader)
        {
            // Yes we can! Always claim that we can, because that will
            // allow us to inject a faked key identifier if there is none,
            // because WIFs signature validation freaks out if there is no
            // key identifier at all.
            return true;
        }

        public EntityId Issuer { get; set; }
    }
}
