using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
  public class EnergyConsumptionModel
    { 
        public int identificator { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public int year { get; set; }
        public double usageOfElectricEnergyPerYear { get; set; }
      


        public EnergyConsumptionModel()
        {
           

        }

       
    }
}
