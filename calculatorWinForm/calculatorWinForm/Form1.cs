namespace calculatorWinForm
{
    public partial class Form1 : Form
    {
        double x, y, z;
        string op;
         public Form1()
        {
            InitializeComponent();
        }

        private void Operators(object sender, MouseEventArgs e)
        {
            if (txtDisplay.Text.Length>0)//age jabe matn khali bood amal nkn 

            x= Convert.ToDouble(txtDisplay.Text);
            op= ((Button)sender).Text;
            txtDisplay.Text = "";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            y= Convert.ToDouble(txtDisplay.Text);
            switch (op)
            {
                case "+":
                    z = x + y;
                    break;
                case "-":
                    z = x - y;
                    break;
                case "*":
                    z = x * y;
                    break;
                case "/":
                   z= x / y;
                    break;

            }
            txtDisplay.Text = z.ToString();
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            //if (txtDisplay.Text.Contains(".")==true) 
            //{
            //    btnPoint.Enabled = false;
            //}
            //else
            //{
            //    btnPoint.Enabled = true; 
            //}
            btnPoint.Enabled = !txtDisplay.Text.Contains(".");
            btnBackSpace.Enabled= Convert.ToBoolean(txtDisplay.Text.Length);
        }
         
        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length>0)//age textbox bishtar a sefre ejra kn yani age chizi to textbox nbod ejra nkn
   
            txtDisplay.Text=txtDisplay.Text.Substring(0,txtDisplay.Text.Length-1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDisplay_TextChanged(null, null);   
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
        }

        private void btnPower_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            MessageBox.Show("Now the Keypress of btnPower is running: "+ e.KeyChar.ToString()); 
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            foreach (Button x in panel1.Controls) // focus mikne ro on dokmei ke ma az keyboard vared krdim
            if (x.Text == e.KeyChar.ToString())//agar x.text == ba kilid zade shode 
            {
             x.Focus(); // boro roosh focus kn
            }
            
            
            
            Button temp = new Button();
            
            
            temp.Text = e.KeyChar.ToString();
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                numbers(temp, null);
            else if (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/')

                Operators(temp, null);
            else if (e.KeyChar == '=')
            {
                btnEqual_Click(null, null);
            }
            else if (e.KeyChar == '.' && txtDisplay.Text.Contains(".") == false)
            {
                numbers(temp, null);
            }
            else if (e.KeyChar == '\b')
            {
                btnBackSpace_Click(null, null);
            }
           
            

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(" im in keyup");
            if (e.KeyCode==Keys.Enter)
            {
                btnEqual_Click(null,null);
            }
        }

        private void btnPower_Click_1(object sender, EventArgs e)
        {
            panel1.Enabled =! panel1.Enabled;
            if (btnPower.Text=="On")
            {
                btnPower.Text = "Off";
                this.KeyPreview = true;
            }
            else
            {
                btnPower.Text="On";
                this.KeyPreview = false;
            }
        }

        private void btnPower_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("im in btnPower MC");
        }

        private void btn0_Click(object sender, EventArgs e)
        {

        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            string s;
            s = "abcdf"; 
            //MessageBox.Show( s.Substring(2,2));
            MessageBox.Show(s.Length.ToString());
            
        }

        private void numbers(object sender, MouseEventArgs e)
        {
            
            txtDisplay.Text += ((Button)sender).Text;
        }
    }
}