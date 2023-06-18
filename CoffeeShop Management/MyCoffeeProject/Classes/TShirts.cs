using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoffeeProject
{
    class TShirts
    {
        public string Design { get; set; }
        public string Color { get; set; }
        public string Size{ get; set; }

        public TShirts(string d, string c, string s)
        {
            Design = d;
            Color = c;
            Size = s;
        }
        public string DisplayTShirt()
        {
            return "Design: " + this.Design + " Size: " + this.Size + " " + "Color: " + this.Color;
        }
    }
   
}
