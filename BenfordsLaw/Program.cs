using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BenfordsLaw
{
    class Program
    {
        static LinkedList<int> extractDigitsFromText(string text)
        {
            LinkedList<int> result = new LinkedList<int>();
            Regex r = new Regex(@"\d+", RegexOptions.IgnoreCase);
            Match m = r.Match(text);
            while (m.Success)
            {
                Group g = m.Groups[0];
                int netDigit = highestDigit(Int32.Parse(g.Value));
                result.AddFirst(netDigit);
                m = m.NextMatch();
            }
            return result;
        }

        static Dictionary<int,int> countDigits(LinkedList<int> numbers)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            for(int i = 0; i < 10; i++)
            {
                result.Add(i, 0);
            }
            foreach (int i in numbers)
            {
                result[i] = result[i] + 1; 
            }
            return result;
        }
        static void printOutResult(Dictionary<int,int> countedNumbers)
        {
            int numberOfNumbers = sumValues(countedNumbers);
            for(int i = 1; i < 10; i++)
            {
                int pom = howMuchProcent(countedNumbers[i], numberOfNumbers);
                Console.WriteLine("Digit {0} occurred {1}\t times, which is {2}\t procent.", i, countedNumbers[i], pom);
            }
        }
        static int howMuchProcent(int part, int all)
        {
            return part * 100 / all;
        }
        static int sumValues(Dictionary<int,int> digitsAndHowManyTimesTheyOccured)
        {
            int result = 0;
            for(int i = 0; i < 10; i++)
            {
                result += digitsAndHowManyTimesTheyOccured[i]; 
            }
            return result;
        }
        static int highestDigit (int anyNumber)
        {
            if (anyNumber < 0)
            {
                anyNumber *= -1;
            }
            int result = 0;
            while (anyNumber != 0)
            {
                result = anyNumber;
                anyNumber /= 10;
            }
            return result;
        }
        static string glueStringsTogether(string[] lines)
        {
            string result = "";
            foreach(string line in lines)
            {
                result += line;
            }
            return result;
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Choose Your file location:");
            //string checkedFilePath = Console.ReadLine();
            try {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sebac\Documents\Visual Studio 2017\Projects\BenfordsLaw\file.txt");
                string textToAnalize = glueStringsTogether(lines);
                printOutResult(countDigits(extractDigitsFromText(textToAnalize)));
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong");
            }
            Console.ReadKey();
        }
    }
}
