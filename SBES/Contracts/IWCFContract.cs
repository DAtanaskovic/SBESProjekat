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
        void TestCommunication();
        [OperationContract]
        bool CreateDataBase(string dbname);
        [OperationContract]
        bool Add(EnergyConsumptionModel item);
        [OperationContract]
        List<EnergyConsumptionModel> Read();
        



    }
}
