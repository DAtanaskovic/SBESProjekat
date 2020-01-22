using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using System.ServiceModel;
using CertManager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using Data;
using System.ServiceModel.Security;

namespace Client
{
    public class WCFClient : ChannelFactory<IWCFContract>, IWCFContract, IDisposable
    {
        IWCFContract factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            //string cltCertCN = "sbesclientadmin";

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = Manager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = CreateChannel();
        }

        public bool CreateDataBase(string a)
        {
            try
            {
                return factory.CreateDataBase(a);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[CreateDataBase] ERROR = {0}", e.Message);
                return false;
            }
        }

        public bool Add(string path, EnergyConsumptionModel item)
        {
            try
            {
                return factory.Add(path, item);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[Add] ERROR = {0}", e.Message);
                return false;
            }
        }
        public List<EnergyConsumptionModel> Read(string path)
        {
            try
            {
                return factory.Read(path);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[Read] ERROR = {0}", e.Message);
                return null;
            }
        }

        public bool DatabaseExists(string path)
        {
            try
            {
                return factory.DatabaseExists(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("[DatabaseExists] ERROR = {0}", e.Message);
                return false;
            }
        }

        public bool Delete(string path, string id)
        {
            try
            {
                return factory.Delete(path, id);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[Delete] ERROR = {0}", e.Message);
                return false;
            }
        }

        public bool Update(string path, EnergyConsumptionModel item)
        {
            try
            {
                return factory.Update(path, item);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[Update] ERROR = {0}", e.Message);
                return false;
            }
        }

        public EnergyConsumptionModel ReadItem(string path, string id)
        {
            try
            {
                return factory.ReadItem(path, id);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[Update] ERROR = {0}", e.Message);
                return null;
            }
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public double AverageConsumptionPerCity(string path, string city)
        {
            try
            {
                return factory.AverageConsumptionPerCity(path, city);
            }
            catch (SecurityAccessDeniedException e)
            {
                Console.WriteLine("[Update] ERROR = {0}", e.Message);
                return -1;
            }
        }
    }
}
