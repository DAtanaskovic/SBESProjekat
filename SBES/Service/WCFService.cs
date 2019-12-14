using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;

namespace Service
{
    public class WCFService : IWCFContract
    {
        private IDataManagement dataBase = new DataManagement();

        public bool Add(string dbPath, EnergyConsumptionModel item)
        {
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("Database does not exists!");
            }

            bool result = dataBase.Create(dbPath, item);
            return result;
        }

        public bool CreateDataBase(string dbname)
        {
            if (File.Exists(dbname))
            {
                //throw new Exception("Database already exists!");
                Console.WriteLine("Database already exists!");
            }

            File.Create(dbname).Dispose();
                
            return true;
        }

        public bool DatabaseExists(string path)
        {
            return File.Exists(path);
        }

        public bool Delete(string path, string id)
        {
            if (!File.Exists(path))
            {
                //throw new Exception("Database already exists!");
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.Delete(path, id);
        }

        public bool Update(string path, EnergyConsumptionModel item)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.Update(path, item);
        }

        public List<EnergyConsumptionModel> Read(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.GetAll(path);
        }

        public EnergyConsumptionModel ReadItem(string path, string id)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            return dataBase.Get(path, id);
        }
    }
}
