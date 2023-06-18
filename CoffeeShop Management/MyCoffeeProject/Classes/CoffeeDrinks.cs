using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeProject.Classes
{
     public abstract class CoffeeDrinks
    {
        public string size { get; set; }
        public string brewTime { get; set; }
        public double price { get; set; }
        protected const double STOREFEE = .05;

        protected double retailPrice;

        public double RetailPrice
        {
            set { calcPrice(); }
            get { return retailPrice; }
        }

        //abstract method
        //public abstract void calcPrice();
        public void calcPrice()
        {
            RetailPrice = (price * STOREFEE);
        }

    }
   
    #region Hot Coffee
    class HotCoffee : CoffeeDrinks
    {
  
        public HotCoffee(string siz, string brewTi, double pric)
        {
            size = siz;
            brewTime = brewTi;           
            price = pric;
        }
        //public override void calcPrice()  //Not nesseary as the coffee drinks dont need to have different ways to calculate
        //{
        //    RetailPrice = (price * storeFee);
        //}
    }
    #endregion

    #region Hot Latte
    class HotLatte : CoffeeDrinks
    {
        public string MilkAmount { get; set; }

        public HotLatte(string siz, string brewTi, double pric)
        {
            size = siz;
            brewTime = brewTi;
            price = pric;
        }
    }
    #endregion
}
