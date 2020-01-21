using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoyalLondonCsvReader
{
    /// <summary>
    /// To read the Csv value from Input file and Write in txt file
    /// </summary>
    public class Program
    {
        /// <summary>
        /// variable csvLocation
        /// </summary>
        private const string csvLocation = @"\Customer.csv";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("-----------------Reading CSV file..............");
                string inputFile = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + csvLocation;
                if (File.Exists(inputFile))
                {
                    var dt = CustomerRenewalInvitation.ReadCsvFile(inputFile);
                    Console.WriteLine("-----------------Reading CSV file Completed ..............");
                    Console.WriteLine("-----------------Writing file..............");
                    CustomerRenewalInvitation custRenewalInvitation = new CustomerRenewalInvitation();
                    custRenewalInvitation.WriteFile(dt);                   
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("File Not Found");
                    Console.ReadLine();
                }

            }
            catch (Exception e)
            {
                ErrorLog.LogError(e);
                Console.WriteLine("Exception caught here");
                Console.ReadLine();
            }
        }


    }
}
