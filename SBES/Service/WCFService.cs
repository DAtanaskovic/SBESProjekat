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
        private IDataManagement dataBase = null;

        public bool Add(EnergyConsumptionModel item)
        {
            if (dataBase == null)
            {
                return false;
            }

            bool result = dataBase.Create(item);
            return result;
        }

        public bool CreateDataBase(string dbname)
        {
           
            dataBase = new DataManagement()
            {
                path = dbname

            };
                
            return true;
        }
        public List<EnergyConsumptionModel> Read()
        {
            return dataBase.GetAll();
        }

        public void TestCommunication()
        {
            Console.WriteLine("TEST");
        }
    }
}
