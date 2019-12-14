using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public interface IDataManagement
    {

        bool Create(string path, EnergyConsumptionModel em);
        bool Delete(string path, string identificator);
        bool Update(string path, EnergyConsumptionModel em);
        EnergyConsumptionModel Get(string path, string identificator);
        List<EnergyConsumptionModel> GetAll(string path);


    }
}
