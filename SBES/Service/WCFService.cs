using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;
using System.Security.Permissions;
using System.ServiceModel;
using SecurityManager.Log;
using System.Security.Principal;
using SecurityManager;
using System.Threading;

namespace Service
{
    public class WCFService : IWCFContract
    {
        private IDataManagement dataBase = new DataManagement();
        private IReplicate proxy;

        [PrincipalPermission(SecurityAction.Demand, Role = "Write")]
        public bool Add(string dbPath, EnergyConsumptionModel item)
        {
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database does not exists!");
            }

            bool result = dataBase.Create(dbPath, item);

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            try
            {
                Audit.AuthorizationSuccess(principal.Identity.Name, OperationContext.Current.IncomingMessageHeaders.Action);
                Audit.AddSuccess(principal.Identity.Name);
            }
            catch (ArgumentException ae)
            {
                Audit.AddFailed(principal.Identity.Name);
                Console.WriteLine(ae.Message);
            }

            proxy = Connect();
            proxy.Add(dbPath, item);

            return result;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Create")]
        public bool CreateDataBase(string dbname)
        {
            if (File.Exists(dbname))
            {
                Console.WriteLine("Database already exists!");
            }

            File.Create(dbname).Dispose();

            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            try
            {
                Audit.CreationSuccess(principal.Identity.Name);
                Audit.AuthorizationSuccess(principal.Identity.Name, OperationContext.Current.IncomingMessageHeaders.Action);
            }
            catch (ArgumentException ae)
            {
                Audit.CreationFailed(principal.Identity.Name);
                Console.WriteLine(ae.Message);
            }

            proxy = Connect();

            proxy.CreateDatabase(dbname);
                
            return true;
        }

        private IReplicate Connect()
        {
            string address = "net.tcp://localhost:10000/Endpoint2";

            NetTcpBinding binding = new NetTcpBinding();

            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;


            ChannelFactory<IReplicate> channel = new ChannelFactory<IReplicate>(binding, address);

            channel.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            IReplicate proxy = channel.CreateChannel();
            return proxy;
        }

        public bool DatabaseExists(string path)
        {
            return File.Exists(path);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Delete")]
        public bool Delete(string path, string id)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.Delete(path, id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public bool Update(string path, EnergyConsumptionModel item)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.Update(path, item);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public List<EnergyConsumptionModel> Read(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.GetAll(path);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
        public EnergyConsumptionModel ReadItem(string path, string id)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.Get(path, id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public double AverageConsumptionPerCity(string path, string city)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            double sum = 0;

            List<EnergyConsumptionModel> consumptions = dataBase.GetAll(path);

            if (consumptions.Count == 0)
            {
                return -1;
            }

            int count = 0;

            foreach (var consumption in consumptions)
            {
                if (consumption.city == city)
                {
                    sum += consumption.usageOfElectricEnergyPerYear;
                    count++;
                }
            }

            return sum / count;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public double AverageConsumptionPerRegion(string path, string region)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            double sum = 0;

            List<EnergyConsumptionModel> consumptions = dataBase.GetAll(path);

            if (consumptions.Count == 0)
            {
                return -1;
            }

            int count = 0;

            foreach (var consumption in consumptions)
            {
                if (consumption.region == region)
                {
                    sum += consumption.usageOfElectricEnergyPerYear;
                    count++;
                }
            }

            return sum / count;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Read")]
        public string MaxConsumptionPerRegion(string path, string region)
        {
            string city = null;

            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            List<EnergyConsumptionModel> consumptions = dataBase.GetAll(path);

            if (consumptions.Count == 0)
            {
                return null;
            }

            double max = consumptions[0].usageOfElectricEnergyPerYear;
            city = consumptions[0].city;

            foreach (var consumption in consumptions)
            {
                if (consumption.region == region)
                {
                    if (consumption.usageOfElectricEnergyPerYear > max)
                    {
                        max = consumption.usageOfElectricEnergyPerYear;
                        city = consumption.city;
                    }
                }
            }

            return city;
        }
    }
}
