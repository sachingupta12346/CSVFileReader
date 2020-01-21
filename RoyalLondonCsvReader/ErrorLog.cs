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
    /// Error Log class to save the exception
    /// </summary>
    public class ErrorLog
    {

        /// <summary>
        /// Method to log error 
        /// </summary>
        /// <param name="ex"></param>
        public static void LogError(Exception ex)
        {
            var logFilePath = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName
                              + @"\ErrorLogFiles\";

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;


            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            var FilePath = logFilePath + "ErrorLog" + "_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";         
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
