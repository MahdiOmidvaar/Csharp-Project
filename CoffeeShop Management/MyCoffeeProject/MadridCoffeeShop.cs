using MyCoffeeProject.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using MyCoffeeProject.Classes.Derived_Coffee_Drinks; //becuase in folder need to import additonal folder

namespace MyCoffeeProject
{

    public partial class MadridCoffeeShop : Form, IHello //need partial. WHY?
    {
       // 'public List<Customer> YodaCustomerList = new List<Customer>;
      
        ArrayList yodaCustomerList = new ArrayList();
        ArrayList coffeesOrdered = new ArrayList();
        public MadridCoffeeShop()
        {
            InitializeComponent();

        }

        private void saveCustBtn_Click(object sender, EventArgs e)
        {
            //TODO
            //look into why i cant use a variable 

            //private string fName;
            //private string lName;
            //private string pNumber;
            //private string fCoffee;

            //private string fName = fNametextBox.Text;
            //string fName = fNametextBox.Text;
            //string fName = fNametextBox.Text;
            //string fName = fNametextBox.Text;

            Customer YodasCustomer = new Customer(fNametextBox.Text, lastNametextBox.Text, phoneNumtextBox.Text, favCoffeeTxtBox.Text );

            //add customer to list
            yodaCustomerList.Add(YodasCustomer);
            
        }

        private void displayCustBtn_Click(object sender, EventArgs e)
        {
            foreach (Customer cust in yodaCustomerList)
            {
                outputListBox.Items.Add(cust.DisplayCustomer()); //add customer obj in string form
                outPutLabel.Text = cust.DisplayCustomer();//display the customer object as string form
            }           
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        #region Clear All Method
        private void ClearAll()
        {
            fNametextBox.Text = "";
            lastNametextBox.Text = "";
            phoneNumtextBox.Text = "";
            favCoffeeTxtBox.Text = "";

            outPutLabel.Text = string.Empty;
            //clear the list box
            outputListBox.Items.Clear();

            priceLabel.Text = "Price:";

            smRadioBtn.Checked = false;
            medRadioBtn.Checked = false;
            lgRadioBtn.Checked = false;

            icedCoffeeRdBtn.Checked = false;
            icedLatteRdBtn.Checked = false;
        }
        #endregion

        #region Calculate Cost Button Click
        private void calcCostBtn_Click(object sender, EventArgs e)
        {//make cleaner and easier   https://stackoverflow.com/questions/18547326/how-do-i-get-which-radio-button-is-checked-from-a-groupbox
            //https://social.msdn.microsoft.com/Forums/windows/en-US/c0c36595-9d98-40f7-8e6a-176a5a3af929/how-to-detect-which-radio-button-is-checked-inside-a-groupbox-control?forum=winforms

            var buttons = this.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);

            string  coffeeType;
            string iceamt = "normal";
            string size;

            if (icedCoffeeRdBtn.Checked == true)
            {
                string bTime = "long";
                coffeeType = "Iced Coffee"; 

                if (smRadioBtn.Checked == true)
                {
                    size = "Small";
                }
                else if (medRadioBtn.Checked == true)
                {
                    size = "Medium";
                }
                else
                {
                    size = "Large";
                }
                //create new Iced Coffee object
                IcedCoffee custsCoffee = new IcedCoffee(size,bTime, iceamt, 3.35  );
                //Set Price
                priceLabel.Text = "Price: " + custsCoffee.price.ToString("C2");
                //Add object to list
                coffeesOrdered.Add(custsCoffee);
                //Count total
                ttlCoffeesLabel.Text = "Total:" + coffeesOrdered.Capacity.ToString();
            }
            else if (icedLatteRdBtn.Checked == true)
            {
                string bTime = "short";
                string icea = "normal";
                string milkAmount = "normal";
                coffeeType = "Iced Latte";

                if (smRadioBtn.Checked == true)
                {
                    size = "Small";
                }
                else if (medRadioBtn.Checked == true)
                {
                    size = "Medium";
                }
                else
                {
                    size = "Large";
                }

                //create new Iced Latte object
                IcedLatte custsCoffee = new IcedLatte(size, bTime, icea, milkAmount, 5.35);
                //Set Price
                priceLabel.Text = "Price: " + custsCoffee.price.ToString("C2");

                //Add object to list
                coffeesOrdered.Add(custsCoffee); //TODO wrong
                //Count total
                ttlCoffeesLabel.Text = "Total:" + coffeesOrdered.Capacity.ToString();
            }

           // CoffeeDrinks customersCoffee = new CoffeeDrinks();     
        }
        #endregion

        private void tShirtBtn_Click(object sender, EventArgs e)
        {
            TShirtForm form = new TShirtForm();
            form.Show();  //show t-shirt form
        }

        //need to define and use the hello () because I impllement the interface at top of the program
        public void Hello()
        {
            Console.WriteLine("Yoda says Hello");
        }

        private void MadridCoffeeShop_Load(object sender, EventArgs e)
        {

        }
    }
}
