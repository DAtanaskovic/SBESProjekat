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
    public interface IReplicate
    {
        [OperationContract]
        bool CreateDatabase(string path);
        [OperationContract]
        bool Add(string path, EnergyConsumptionModel item);
    }
}
