using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using CertManager;
using System.Security.Principal;
using System.ServiceModel.Description;
using SecurityManager;
using System.IdentityModel.Policy;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();

            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


            ServiceHost host = new ServiceHost(typeof(WCFService));
            string address = "net.tcp://localhost:9999/Receiver";
            host.AddServiceEndpoint(typeof(IWCFContract), binding, address);

            // dodato
            //host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            //.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            //string srvCertCN = "wcfservice";
            //string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            // host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            // host.Credentials.ServiceCertificate.Certificate = Manager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host.Authorization.ServiceAuthorizationManager = new CustomServiceAuthorizationManager();

            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            // log event podesavanja
            ServiceSecurityAuditBehavior newAudit = new ServiceSecurityAuditBehavior();
            newAudit.AuditLogLocation = AuditLogLocation.Application;
            newAudit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;
            newAudit.SuppressAuditFailure = true;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAudit);


            Console.WriteLine("Korisnik {0} je pokrenuo servera", WindowsIdentity.GetCurrent().Name);

            try
            {
                host.Open();
                Console.WriteLine("WCFService is started.\nPress <enter> to stop ...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                host.Close();
            }

        }
    }
}
