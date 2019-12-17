using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;

namespace Replicator
{
    public class Replicate : IReplicate
    {
        private IDataManagement database;

        public bool Add(string path, EnergyConsumptionModel item)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Database does not exists!");
            }

            bool result = database.Create(path, item);
            return result;
        }

        public bool CreateDatabase(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("Database already exists!");
            }

            File.Create(path).Dispose();

            return true;
        }
    }
}
