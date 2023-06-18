using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeProject.Classes
{
    class Coffee
    {
        //properties
        private string coffeeBrand;
        public string grindType { get; set; }
        public string orgin { get; set; }

        private string _roastType;
        //public static string
        public string CoffeeBrand
        {
            get { return coffeeBrand; }
            set { coffeeBrand = value; }

        }
     
        // default constructor
        public Coffee()
        {
            grindType = "espresso";
            orgin = "Italy";
            coffeeBrand = "LaVazza";
        }
        //overloaded constructor taking arguments
        public Coffee (String cofBrand, string grindTpe, string orin)
        {
            coffeeBrand = cofBrand;
            grindType = grindTpe;
            orgin = orin;
        }
    }
    
}
