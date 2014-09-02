using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Kentor.AuthServices
{
    static class IdentityProviderSingleSignOnDescriptorExtensions
    {
        public static void Load(this IdentityProviderSingleSignOnDescriptor idpSsoDescriptor, XElement xmlData)
        {
            if(idpSsoDescriptor == null)
            {
                throw new ArgumentNullException("idpSsoDescriptor");
            }
            
            if(xmlData == null)
            {
                throw new ArgumentNullException("xmlData");
            }

            ValidateProtocolSupportEnumeration(xmlData);
            LoadKey(idpSsoDescriptor, xmlData);

            foreach(var ssoServiceXml in xmlData.Elements(Saml2Namespaces.Saml2Metadata + "SingleSignOnService"))
            {
                var endpoint = new ProtocolEndpoint();
                endpoint.Load(ssoServiceXml);
                idpSsoDescriptor.SingleSignOnServices.Add(endpoint);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "protocolSupportEnumeration")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IDPSSODescriptor")]
        private static void ValidateProtocolSupportEnumeration(XElement xmlData)
        {
            var protocolSupportEnumeration = xmlData.Attribute("protocolSupportEnumeration");
            if (protocolSupportEnumeration == null)
            {
                throw new InvalidMetadataException("Missing protocolSupportEnumeration attribute in IDPSSODescriptor.");
            }

            if (protocolSupportEnumeration.Value != "urn:oasis:names:tc:SAML:2.0:protocol")
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Invalid protocolSupportEnumeration \"{0}\".",
                    protocolSupportEnumeration.Value);
                throw new InvalidMetadataException(msg);
            }
        }

        private static void LoadKey(IdentityProviderSingleSignOnDescriptor idpSsoDescriptor, XElement xmlData)
        {
            var keyDescriptorXml = xmlData.Element(Saml2Namespaces.Saml2Metadata + "KeyDescriptor");

            var keyDescriptor = new KeyDescriptor()
                {
                    KeyInfo = new SecurityKeyIdentifier()
                };

            idpSsoDescriptor.Keys.Add(keyDescriptor);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(
                keyDescriptorXml.Element(XNamespace.Get(SignedXml.XmlDsigNamespaceUrl) + "KeyInfo").ToString());

            keyDescriptor.KeyInfo.Add(new GenericXmlSecurityKeyIdentifierClause(xmlDocument.DocumentElement));            
        }
    }
}
