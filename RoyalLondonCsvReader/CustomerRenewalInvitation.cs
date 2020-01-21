using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace RoyalLondonCsvReader
{
    /// <summary>
    ///  Read the Csv value and Write into Txt file 
    /// </summary>
    public class CustomerRenewalInvitation
    {

        #region Private Variable
        /// <summary>
        /// private variable creditCharge
        /// </summary>
        private decimal creditCharge;
        /// <summary>
        /// private variable totalAnnualCreditCharge
        /// </summary>
        private decimal totalAnnualCreditCharge;
        /// <summary>
        /// private variable averageMonthlyPremium
        /// </summary>
        private decimal averageMonthlyPremium;
        /// <summary>
        /// private variable initialMonthlyPaymentAmount
        /// </summary>
        private decimal initialMonthlyPaymentAmount;
        /// <summary>
        /// private variable otherMonthlyPaymentAmount
        /// </summary>
        private decimal otherMonthlyPaymentAmount;
        #endregion


        /// <summary>
        /// Read the Csv File 
        /// </summary>
        /// <param name="sourceFile">sourceFile path of csv</param>
        /// <returns>returns datatable</returns>
        #region ReadCsvFile
        public static DataTable ReadCsvFile(string sourceFile)
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string Fulltext;
                if (File.Exists(sourceFile))
                {
                    using (StreamReader sr = new StreamReader(File.OpenRead(sourceFile)))
                    {
                        while (!sr.EndOfStream)
                        {
                            Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                            string[] rows = Fulltext.Split('\n'); //split full file text into rows 
                            rows = rows.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                            for (int i = 0; i < rows.Count(); i++)
                            {
                                string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < rowValues.Count(); j++)
                                        {
                                            dtCsv.Columns.Add(rowValues[j].Replace("\r", "")); //add headers  
                                        }
                                    }
                                    else
                                    {                                       
                                         DataRow dr = dtCsv.NewRow();
                                         for (int k = 0; k < rowValues.Count(); k++)
                                         {
                                             dr[k] = rowValues[k].ToString();
                                         }
                                            dtCsv.Rows.Add(dr); //add other rows  
                                    }
                                }
                            } // Closing for loop
                        } //Closing while loop
                    }
                }
                else
                {
                    Console.WriteLine("File Not Found");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                ErrorLog.LogError(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ErrorLog.LogError(ex);
                throw ex;
            }

            return dtCsv;
        }


        #endregion

        /// <summary>
        /// Write the Txt file based on calulation
        /// </summary>
        /// <param name="dtCsv">Data Table of Source value as provided </param>
        #region WriteTextFile
        public void WriteFile(DataTable dtCsv)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                var template = TemplateFormat.Template();
                if (dtCsv != null)
                {
                    if (dtCsv.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCsv.Rows)
                        {
                            //Caluation of the field
                            if (Caluclation(dr))
                            {                                                        
                                //Replacing the static value from dynamic value
                                string text = template.Replace("Title", dr["Title"].ToString())
                                                .Replace("FirstName", dr["FirstName"].ToString())
                                                .Replace("SurName", dr["SurName"].ToString())
                                                .Replace("ProductName", dr["ProductName"].ToString())
                                                .Replace("PayoutAmount", dr["PayoutAmount"].ToString())
                                                .Replace("AnnualPremium", dr["AnnualPremium"].ToString())
                                                .Replace("creditCharge", creditCharge.ToString())
                                                .Replace("totalAnnualCreditCharge", totalAnnualCreditCharge.ToString())
                                                .Replace("initialMonthlyPaymentAmount", initialMonthlyPaymentAmount.ToString())
                                                .Replace("otherMonthlyPaymentAmount", otherMonthlyPaymentAmount.ToString());        
                                                 
                              var path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName
                                   + @"\Destination\";
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }

                                var filePath = path + dr["ID"] + "_" + dr["FirstName"] + dr["SurName"] + ".txt";
                                var files = Directory.GetFiles(path);
                                //Checking the file exist or not and write the file
                                if (!files.Contains(filePath))
                                {
                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                    {                                     
                                        file.WriteLine(text);
                                    }
                                    Console.WriteLine("--------Writing file Completed for ID:" + dr["ID"] + "............. ");
                                }
                                else
                                {
                                    Console.WriteLine("-------File already exists in the Destination folder for ID:" + dr["ID"] + "............. ");
                                }
                               
                            } //Closing If Condition calculation is done or not

                        }

                    }
                    else
                    {
                        Console.WriteLine("File is Empty.");
                    } 
                   
                }
             
               
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// To calculate the value 
        /// </summary>
        /// <param name="dr">DataRow of each Table of Customer</param>
        #region Calucaltion 
        public bool Caluclation(DataRow dr)
        {
            try
            {
                decimal num = 0;
                //To check PayoutAmount value in correct formate or not
                bool payoutAmountConvert = decimal.TryParse(dr["PayoutAmount"].ToString(), out num);                
                if (payoutAmountConvert == false)
                {
                    Console.WriteLine("PayoutAmount is not in the correct formate for ID:" + dr["ID"] + " ");
                    return false;
                }

                //To check AnnualPremium value in correct formate or not
                bool annualPremiumConvert = decimal.TryParse(dr["AnnualPremium"].ToString(), out num);
                if (annualPremiumConvert == true)
                {
                    // AnnualPremium
                    var annualPremium = Convert.ToDecimal(dr["AnnualPremium"].ToString().Replace("\r", ""));
                    //     creditCharge  =  5% of the annual premium  rounded upto 2 decimal
                    creditCharge = Math.Round((decimal)5 / 100 * annualPremium, 2);
                    // TotalAnnualCreditCharge  = creditCharge + annualPremium rounded upto 2 decimal
                    totalAnnualCreditCharge = Math.Round(creditCharge + annualPremium, 2);
                    //AverageMonthlyPremium  will totalAnnualCreditCharge divided by the number of months in a year.
                    averageMonthlyPremium = totalAnnualCreditCharge / 12;
                    //otherMonthlyPaymentAmount will be AverageMonthlyPremium rounded upto 2 decimal
                    otherMonthlyPaymentAmount = Math.Round(averageMonthlyPremium, 2);
                    //initialMonthlyPaymentAmount will be TotalAnnualCreditCharge - otherMonthlyPaymentAmount *11.
                    initialMonthlyPaymentAmount = totalAnnualCreditCharge - (otherMonthlyPaymentAmount) * 11;
                    return true;
                }
                else
                {
                    Console.WriteLine("AnnualPremium is not in the correct formate for ID:" + dr["ID"] + " ");                   
                    return false; 
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogError(ex);
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion
    }
}
