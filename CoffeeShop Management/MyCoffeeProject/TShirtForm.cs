using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCoffeeProject
{
    public partial class TShirtForm : Form
    {
        public TShirtForm()
        {
            InitializeComponent();

            ArrayList tShirtList = new ArrayList();

            //Customer YodasCustomer = new Customer(fNametextBox.Text, lastNametextBox.Text, phoneNumtextBox.Text, favCoffeeTxtBox.Text);
            TShirts madridShirt = new TShirts("Yoda", "Green", "Large");
            tShirtList.Add(madridShirt.DisplayTShirt());

            comboBoxTShirts.Items.Add(madridShirt.DisplayTShirt());
        }

        private void TShirtForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblSelected_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxTShirts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxSelected_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
