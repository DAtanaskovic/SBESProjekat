using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Data
{
    public class DataManagement:IDataManagement
    {
        public string path { get; set; } = @"fajl.txt";
        List<EnergyConsumptionModel> listOfObjects = new List<EnergyConsumptionModel>();
       public bool Create(EnergyConsumptionModel em)
        {

            bool isCreated = false;
            if (!listOfObjects.Contains(em))
            {
                listOfObjects.Add(em);

                string json = JsonConvert.SerializeObject(listOfObjects, Formatting.Indented);
                File.WriteAllText(path, json);
                isCreated = true;
                Console.WriteLine("Creating is done successfully");
                return isCreated;
            }
            else
            {
                Console.WriteLine("There is already the same object in file.");
                isCreated = false;
                return isCreated;
            }

        }

        public bool Delete(string identificator)
        {
            bool isDeleted = false;
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
                    isDeleted = false;
                }
            }
            return isDeleted;
        }

        public EnergyConsumptionModel Get(string identificator)
        {
            EnergyConsumptionModel em = new EnergyConsumptionModel();
            for (int i = 0; i < listOfObjects.Count; i++)
            {
                if (listOfObjects[i].identificator == identificator)
                {
                    em = listOfObjects[i];
                }
            }
            return em;

        }

        public List<EnergyConsumptionModel> GetAll()
        {
            string json = File.ReadAllText(path);
            List<EnergyConsumptionModel> list = JsonConvert.DeserializeObject<List<EnergyConsumptionModel>>(json);
            return list;
        }

        public bool Update(EnergyConsumptionModel em)
        {
            bool isUpdated = false;
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

