using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            calc:

            #region Actions
            Action<string> PrintEnterjustNumber = delegate (string title)
            {
                Console.WriteLine("\n\tWarning!");
                Thread.Sleep(2000);
                Console.WriteLine($"\n\tinsert only number for {title}");
            };
            #endregion

            #region Operating

            Console.WriteLine("\n\twelcome to the panel");

           
            float answer;

            Console.Write("\n\tenter your first number : ");
            float a;
            while (!float.TryParse(Console.ReadLine(), out a))
            {
                PrintEnterjustNumber("for a");
                Thread.Sleep(1500);
                Console.Write("\n\tenter your first number: ");
                

            }

            Console.Write("\n\t+ , -, *, /, %          : ");
            string operand = Console.ReadLine();

            Console.Write("\n\tenter your second number: ");
            float b;
            while (!float.TryParse(Console.ReadLine(), out b))
            {
                PrintEnterjustNumber("for b");
                Thread.Sleep(1500);
                Console.Write("\n\tenter your second number : ");
              
            }

            switch (operand)
            {
                case "+":
                    answer = a + b;
                    break;
                case "-":
                    answer = a - b;
                    break;
                case "*":
                    answer = a * b;
                    break;
                case "/":
                    answer = a / b;
                    break;
                case "%":
                    answer = a % b;
                    break;
            
                default: 
                    answer = 0; 
                    break;
            }
            Console.WriteLine($"\n\n\t\t {a} {operand} {b} : {answer}");
            Console.ReadKey();
            #endregion
        }
    }
}
