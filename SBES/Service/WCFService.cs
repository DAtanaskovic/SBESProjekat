using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;
using System.Security.Permissions;

namespace Service
{
    public class WCFService : IWCFContract
    {
        private IDataManagement dataBase = new DataManagement();

        [PrincipalPermission(SecurityAction.Demand, Role = "Write")]
        public bool Add(string dbPath, EnergyConsumptionModel item)
        {
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database does not exists!");
            }

            bool result = dataBase.Create(dbPath, item);
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
                
            return true;
        }

        public bool DatabaseExists(string path)
        {
            return File.Exists(path);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modify")]
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

            List<EnergyConsumptionModel> consumptions = dataBase.GetAll(path).Where(c => c.city == city).ToList();

            if (consumptions.Count == 0)
            {
                return -1;
            }

            consumptions.ForEach(c => sum += c.usageOfElectricEnergyPerYear);

            return sum / consumptions.Count;
        }
    }
}
