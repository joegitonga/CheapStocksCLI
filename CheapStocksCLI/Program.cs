using System;
using CheapStocksCLI.Models;
using System.Collections.Generic;
using System.Data;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Linq;
using System.Threading.Channels;

namespace CheapStocksCLI
{
    public class Program
    {
        /// <summary>
        /// Application's entry point.
        /// </summary>
        static void Main(string[] args)
        {
            bool Menu = true;

            Console.Title = "Cheap Stocks Intl.";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("................................................................");
            Console.WriteLine("Welcome to Cheap Stocks Intl");
            Console.WriteLine("................................................................");
            
            //This part calls the display menu method
            while (Menu)
            {
                Menu = DisplayMenu();
            }
        }

        /// <summary>
        /// This method returns a menu option selected by the user.
        /// </summary>
        public static bool DisplayMenu()
        {
            //Console.Clear();
            Console.WriteLine("What do you want to do");
            Console.WriteLine("1. Check Currency availability");
            Console.WriteLine("2. Get Help?");
            Console.WriteLine("3. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Enter the currency iso code");
                    string curr = Console.ReadLine();
                    Console.WriteLine(CheckCurrencySupport(curr));
                    Console.ReadLine();
                    return true;
                case "2":
                    Console.WriteLine("Contact System Administrator");
                    Console.ReadLine();
                    return true;
                case "3":
                    Console.WriteLine("Goodbye!");
                    return false;
                default:
                    Console.WriteLine("This input is not recognized, Please try again from the menu items 1,2 or 3.");
                    return true;

            }
        }

        /// <summary>
        /// This method reads the files from the csv file, loads the data to a data table then that
        /// data gets moved to a List. 
        /// </summary>
        public static List<Currencies> CsvContent()
        {
            List<Currencies> _currencies = new List<Currencies>();
            try
            {
                var csvTable = new DataTable();
                using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(@"D:\source\C#\CheapStocksCLI\Cheap.Stocks.Internationalization.Currencies.csv")), true))
                {
                    csvTable.Load(csvReader);
                }                

                for (int i = 0; i < csvTable.Rows.Count; i++)
                {
                    _currencies.Add(new Currencies { Country = csvTable.Rows[i][0].ToString(), Currency = csvTable.Rows[i][1].ToString(), ISO = csvTable.Rows[i][2].ToString() });
                }

                return _currencies;
            }
            catch (Exception ex)
            {
                return _currencies;
                Console.Write(ex);
            }

        }

        /// <summary>
        /// This method accepts user input and compares with the data on the list to check for 
        /// currency availability.
        /// </summary>
        public static string CheckCurrencySupport(string iso)
        {
            string availability = "";
            List<Currencies> currencies = CsvContent();            
            try
            {
               var matchingvalues = currencies.Where(p => p.ISO.Contains(iso));

                if (!string.IsNullOrEmpty(iso))
                {
                    if (currencies.Any(p => p.ISO.Contains(iso) == true))
                    {
                        availability = "This currency is available";

                    }
                    else
                    {
                        availability = "This currency is NOT available";
                    }                    
                }
                else
                {
                    availability = "Currency cannot be blank";
                }

                return availability;
            }
            catch (Exception ex)
            {
                return availability;
                Console.WriteLine(ex);
            }

        }
    }
}
