using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
  public class EnergyConsumptionModel
    { 
        public int identificator;
        public string region;
        public string city;
        public int year;
        public double usageOfElectricEnergyPerYear;
        public string path;

        public int Identificator
        {
            get { return identificator; }
            set { identificator = value; }
        }
        

        public string Region 
        {
            get { return region; }
            set { region = value; }
        }
      
        public string City
        {
            get { return city; }
            set { city = value; }
        }

   
        public int Year 
        {
            get { return year; }
            set { year = value; }
        }


        public double UsageOfElectricEnergyPerYear
        {
            get { return usageOfElectricEnergyPerYear; }
            set { usageOfElectricEnergyPerYear= value; }
        }
       

        public string Path
        {
            get { return path; }
            set { path = value; }
        }



        public EnergyConsumptionModel()
        {
           

        }

       
    }
}
