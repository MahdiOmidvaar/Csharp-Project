using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeProject.Classes
{
    class Customer
    {
       
        public string Firstname { get; set; }
        public string Lastname { get; set; }
       
        private string _favCoffee;

        public string FavCoffee
        {
            get{return _favCoffee;}
            set{_favCoffee = value;}
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            set { phoneNumber = value; }
            get { return phoneNumber; }
        }

      


        private int _custId;

        public void setId(int id)
        {
            if (id <= 0)
            {
                throw new Exception("ID cant be negative number");
            }
            //set
            this._custId = id;
        }

        //get
        public int getId()
        {
            return this._custId;
        }

        private int _test;

       
        public  const double FEE = .3;

        public Customer( string F, string L,  string phone, string fCoffee)
        {
            Firstname = F;
            Lastname = L;
            _favCoffee = fCoffee;
            PhoneNumber = phone;
        }
        public string DisplayCustomer()
        {
            return this.Firstname + " " + this.Lastname + " " + " Phone Number: " + this.phoneNumber + Environment.NewLine + "Favorite Coffee: " + this._favCoffee;
        }
        public string GetFullName()
        {
            return this.Firstname + " " + this.Lastname;
        }
        public double getFee()
        {
            return FEE;
        }
    }
}
