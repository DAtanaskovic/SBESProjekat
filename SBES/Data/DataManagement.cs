using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Data
{
    public class DataManagement : IDataManagement
    {
       public string path { get; set; } = @"fajl.txt";
       List<EnergyConsumptionModel> listOfObjects = new List<EnergyConsumptionModel>();

       public bool Create(string path, EnergyConsumptionModel em)
        {
            bool isCreated = true;

            if (!File.Exists(path))
            {
                isCreated = false;
            }

            listOfObjects = GetAll(path);

            if (listOfObjects == null)
            {
                listOfObjects = new List<EnergyConsumptionModel>();
            }

            listOfObjects.Add(em);

            string json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);
            File.WriteAllText(path, json);
            Console.WriteLine("Creating is done successfully");

            return isCreated;
        }

        public bool Delete(string path, string identificator)
        {
            bool isDeleted = false;

            listOfObjects = GetAll(path);

            if (listOfObjects == null)
            {
                return false;
            }

            for (int i = 0; i < listOfObjects.Count; i++)
            {
                if (listOfObjects[i].identificator == identificator)
                {
                    listOfObjects.Remove(listOfObjects[i]);
                    isDeleted = true;
                    string json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);
                    File.WriteAllText(path, json);
                    Console.WriteLine("Deleting is done successfully.");
                }
                else
                {
                    Console.WriteLine("Couldn't delete object with that identificator.");
                }
            }

            return isDeleted;
        }

        public EnergyConsumptionModel Get(string path, string identificator)
        {
            listOfObjects = GetAll(path);

            if (listOfObjects == null)
            {
                return null;
            }

            for (int i = 0; i < listOfObjects.Count; i++)
            {
                if (listOfObjects[i].identificator == identificator)
                {
                    return listOfObjects[i];
                }
            }

            return null;
        }

        public List<EnergyConsumptionModel> GetAll(string dbPath)
        {
            string json = File.ReadAllText(dbPath);
            List<EnergyConsumptionModel> list = JsonConvert.DeserializeObject<List<EnergyConsumptionModel>>(json);
            return list;
        }

        public bool Update(string path, EnergyConsumptionModel em)
        {
            bool isUpdated = false;

            listOfObjects = GetAll(path);

            if (listOfObjects == null)
            {
                return false;
            }

            for (int i = 0; i < listOfObjects.Count; i++)
            {
                if (listOfObjects[i].identificator == em.identificator)
                {
                    listOfObjects[i] = em;
                    isUpdated = true;
                    string json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);
                    File.WriteAllText(path, json);
                    Console.WriteLine("Updating is done successfully.");
                }
                else
                {
                    Console.WriteLine("Couldn't update this object");
                    isUpdated = false;
                }
            }
            return isUpdated;
        }

    }
    }

