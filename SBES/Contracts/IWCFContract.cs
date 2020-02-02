using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IWCFContract
    {
        [OperationContract]
        bool CreateDataBase(string dbname);
        [OperationContract]
        bool Add(string dbPath, EnergyConsumptionModel item);
        [OperationContract]
        List<EnergyConsumptionModel> Read(string path);
        [OperationContract]
        bool DatabaseExists(string path);
        [OperationContract]
        bool Delete(string path, string id);
        [OperationContract]
        bool Update(string path, EnergyConsumptionModel item);
        [OperationContract]
        EnergyConsumptionModel ReadItem(string path, string id);
        [OperationContract]
        double AverageConsumptionPerCity(string path, string city);
        [OperationContract]
        double AverageConsumptionPerRegion(string path, string city);
        [OperationContract]
        string MaxConsumptionPerRegion(string path, string city);
    }
}
