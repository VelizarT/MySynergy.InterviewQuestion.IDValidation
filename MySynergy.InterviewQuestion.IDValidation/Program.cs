using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MySynergy.InterviewQuestion.IDValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter ID: ");
            string idToCheck = Console.ReadLine();

            if(string.IsNullOrEmpty(idToCheck))
            {
                Console.WriteLine("Invalid Input");
                return;
            }
            
            string DoB = BuildDoB(idToCheck);

            bool isValidString = IsIdValid(idToCheck);
            bool isValidDoB = IsDoBValid(DoB);

            switch (isValidString && isValidDoB)
            {
                case true:
                    Console.WriteLine("TRUE");
                    Console.WriteLine(DoB);
                    Console.WriteLine(Sex(idToCheck));
                    break;
                case false:
                    Console.WriteLine("FALSE");
                    break;
            }
        }

        static bool IsIdValid(string id)
        {
            string regex = @"^[0-9]{10}$";
            Match checkNumbers = Regex.Match(id, regex);

            bool isLastDigitValid = CheckLastDigit(id);

            return checkNumbers.Success && isLastDigitValid; 
        }

        static bool CheckLastDigit(string id)
        {
            int[] coeficientArray = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int sum = 0;

            for(int i = 0; i < id.Length - 1; i++)
            {
                sum += (int)Char.GetNumericValue(id[i]) * coeficientArray[i];
            }

            return (sum % 11) % 10 == (int)Char.GetNumericValue(id[^1]);
        }

        static string BuildDoB(string id)
        {
            string year = id.Substring(0, 2);
            string firstMonthDigit = id.Substring(2, 1);
            string secondMonthDigit = id.Substring(3, 1);
            string month;
            string day = id.Substring(4, 2);

            switch (firstMonthDigit)
            {
                case "2":
                case "3":
                    year = "18" + year;
                    month = (firstMonthDigit == "2" ? "0" : "1") + secondMonthDigit;
                    break;
                case "4":
                case "5":
                    year = "20" + year;
                    month = (firstMonthDigit == "4" ? "0" : "1") + secondMonthDigit;
                    break;
                default:
                    year = "19" + year;
                    month = firstMonthDigit + secondMonthDigit;
                    break;
            }

            return year + "-" + month + "-" + day;
        }

        static bool IsDoBValid(string DoB)
        {
            DateTime result;
            Console.WriteLine("Attempting to parse strings using {0} culture.",
                        CultureInfo.CurrentCulture.Name);
            return DateTime.TryParse(DoB, out result);
        }

        static string Sex(string id)
        {
            return (int)Char.GetNumericValue(id[^2]) % 2 == 0 ? "M" : "F";
        }
    }
}
