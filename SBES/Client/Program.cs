using CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
            //binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            //X509Certificate2 srvCert = Manager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"));

            Dictionary<int, string> idMap = new Dictionary<int, string>();

            using (WCFClient proxy = new WCFClient(binding, address))
            {
                while(true)
                {
                    string option = ShowMenu();

                    if (option == "1")
                    {
                        Console.WriteLine("Enter path:");
                        string path = Console.ReadLine();

                        if (proxy.CreateDataBase(path))
                        {
                            Console.WriteLine("Database created successfuly");
                        }
                        
                    }
                    else if (option == "2")
                    {
                        Console.WriteLine("Enter path:");
                        string path = Console.ReadLine();

                        if (!proxy.DatabaseExists(path))
                        {
                            Console.WriteLine("Database does not exists!");
                            continue;
                        }

                        string id = Guid.NewGuid().ToString();

                        Console.Write("Enter region: ");
                        string region = Console.ReadLine();

                        Console.Write("Enter city: ");
                        string city= Console.ReadLine();

                        Console.Write("Enter year: ");
                        int year = int.Parse(Console.ReadLine());

                        Console.Write("Enter usage: ");
                        double usage = double.Parse(Console.ReadLine());

                        EnergyConsumptionModel e1 = new EnergyConsumptionModel()
                        {
                            identificator = id,
                            region = region,
                            city = city,
                            year = year,
                            usageOfElectricEnergyPerYear = usage
                        };

                        if (proxy.Add(path, e1))
                        {
                            Console.WriteLine("Item added successfully");
                        }
                    }
                    else if (option == "3")
                    {
                        Console.WriteLine("Enter path:");
                        string path = Console.ReadLine();

                        if (!proxy.DatabaseExists(path))
                        {
                            Console.WriteLine("Database does not exists!");
                            continue;
                        }

                        List<EnergyConsumptionModel> items = proxy.Read(path);

                        if (items == null)
                        {
                            Console.WriteLine("Database is empty!");
                            continue;
                        }

                        int itemCounter = 1;

                        for(int i=0; i< items.Count;i++)
                        {
                            Console.WriteLine("---------------------");
                            Console.WriteLine($"ID: {itemCounter++}");
                            Console.WriteLine(items[i].region);
                            Console.WriteLine(items[i].city);
                            Console.WriteLine(items[i].year);
                            Console.WriteLine(items[i].usageOfElectricEnergyPerYear);
                        }
                    }
                    else if (option == "4")
                    {
                        Console.WriteLine("Enter path:");
                        string path = Console.ReadLine();

                        if (!proxy.DatabaseExists(path))
                        {
                            Console.WriteLine("Database does not exists!");
                            Console.WriteLine();
                            continue;
                        }

                        Console.Write("Enter item ID: ");
                        int id = int.Parse(Console.ReadLine());

                        idMap = CreateIdMap(proxy, path);

                        if (proxy.Delete(path, idMap[id]))
                        {
                            Console.WriteLine($"Item with id: {id} deleted succesfully");
                        }
                    }
                    else if (option == "5")
                    {
                        Console.WriteLine("Enter path:");
                        string path = Console.ReadLine();

                        if (!proxy.DatabaseExists(path))
                        {
                            Console.WriteLine("Database does not exists!");
                            continue;
                        }

                        Console.Write("Enter item ID: ");
                        int id = int.Parse(Console.ReadLine());

                        idMap = CreateIdMap(proxy, path);

                        var energyModel = proxy.ReadItem(path, idMap[id]);

                        Console.Write("Enter region: ");
                        string region = Console.ReadLine();

                        Console.Write("Enter city: ");
                        string city = Console.ReadLine();

                        Console.Write("Enter year: ");
                        int year = int.Parse(Console.ReadLine());

                        Console.Write("Enter usage: ");
                        double usage = double.Parse(Console.ReadLine());

                        energyModel.region = region;
                        energyModel.city = city;
                        energyModel.year = year;
                        energyModel.usageOfElectricEnergyPerYear = usage;

                        if (proxy.Update(path, energyModel))
                        {
                            Console.WriteLine($"Item with id: {id} updated succesfully");
                        }
                    }
                }
            }
        }

        public static string ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Create Database:");
            Console.WriteLine("2. Add Energy Conusumption:");
            Console.WriteLine("3. Read Database");
            Console.WriteLine("4. Delete Item");
            Console.WriteLine("5. Update Item");
            Console.WriteLine();

            return Console.ReadLine();
        }

        public static Dictionary<int, string> CreateIdMap(WCFClient proxy, string path)
        {
            Dictionary<int, string> idMap = new Dictionary<int, string>();

            List<EnergyConsumptionModel> items = proxy.Read(path);

            if (items == null)
            {
                return null;
            }

            int itemCounter = 1;

            for (int i = 0; i < items.Count; i++)
            {
                idMap[itemCounter++] = items[i].identificator;
            }

            return idMap;
        }
    }
}
