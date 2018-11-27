using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twentyci_Test_1
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            string test = Checkreverse_Text("This is another test");
            Console.WriteLine(test);
            Console.ReadLine();
        }
        public static string Checkreverse_Text(string Input)
        {
            char[] whitespace = new char[] { ' ', '\t' };
            string[] strNewInput = Input.Split(whitespace);
            string result = "";
            for (int i = 0; i < strNewInput.Length; i++)
            {
                if (strNewInput[i].Length > 4)
                {
                    strNewInput[i] = Reverse(strNewInput[i]);
                }
                result += " " + strNewInput[i];
            }
            return result;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
