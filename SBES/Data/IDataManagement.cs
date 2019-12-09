using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public interface IDataManagement
    {

        bool Create(EnergyConsumptionModel em);
        bool Delete(int identificator);
        bool Update(EnergyConsumptionModel em);
        EnergyConsumptionModel Get(int identificator);
        List<EnergyConsumptionModel> GetAll();


    }
}
