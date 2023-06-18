using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeProject.Classes.Derived_Coffee_Drinks
{
    #region Iced Coffee
    class IcedCoffee : CoffeeDrinks
    {
        public string iceAmount { get; set; }

        public IcedCoffee(string siz, string brewTi, string iceAmt, double pric)
        {
            size = siz;
            brewTime = brewTi;
            iceAmount = iceAmt;
            price = pric;
        }
    }
    #endregion
}
