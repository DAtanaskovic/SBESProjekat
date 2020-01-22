using CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CustomPrincipal : IPrincipal
    {
        private GenericIdentity identity = null;
        private List<string> roles = new List<string>();
        private string group = string.Empty;

        public CustomPrincipal(GenericIdentity genericIdentity)
        {
            this.identity = genericIdentity;
            string name = Formatter.ParseClientSubjectName(identity.Name);

            X509Certificate2 clientCert = Manager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, name);

            if (clientCert != null)
            {
                string subjectName = clientCert.SubjectName.Name;
                string[] parseStrings = subjectName.Split(',');
                group = parseStrings[1].Remove(0, 4);
            }
        }

        public IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string permission)
        {
            string[] permissions;

            if (RolesConfig.GetPermissions(group, out permissions))
            {
                foreach (string permision in permissions)
                {
                    if (permision.Equals(permission))
                        return true;
                }
            }
            return false;
        }
    }
}
