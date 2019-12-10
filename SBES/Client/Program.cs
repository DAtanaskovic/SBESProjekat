using CertManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "wcfservice";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = Manager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (WCFClient proxy = new WCFClient(binding, address))
            {
                while(true)
                {

                    Console.WriteLine("1.Create DataBase:");
                    Console.WriteLine("2.Add Energy Conusmption:");
                    Console.WriteLine("3.Read all items");
                    string number =Console.ReadLine();
                    if(number=="1")
                    {
                        Console.WriteLine("Enter path:");
                        string path = Console.ReadLine();
                        proxy.CreateDataBase(path);
                    }
                    else if (number == "2")
                    {
                   
                        string id = Guid.NewGuid().ToString();

                        Console.WriteLine("Enter region:");
                        string region = Console.ReadLine();

                        Console.WriteLine("Enter city:");
                        string city= Console.ReadLine();

                        Console.WriteLine("Enter year:");
                        int year = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter usage:");
                        double usage = double.Parse(Console.ReadLine());

                        EnergyConsumptionModel e1 = new EnergyConsumptionModel()
                        {
                            identificator = id,
                            region = region,
                            city = city,
                            year = year,
                            usageOfElectricEnergyPerYear = usage

                        };

                        proxy.Add(e1);

                    }
                    else if(number=="3")
                    {
                      List<EnergyConsumptionModel> list=proxy.Read();
                        for(int i=0; i<list.Count;i++)
                        {

                            Console.WriteLine(list[i].city);

                        }
                    }

                }

            }
        }
    }
}
