using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoyalLondonCsvReader;
using System.Data;
using System.IO;

namespace UnitTest
{
    // <summary>
    ///  Unit Test Case of Customer
    /// </summary>
    [TestClass]
    public class UnitTestCustomer
    {
        #region constant
        /// <summary>
        /// inputfile variable
        /// </summary>
        private string inputFile;
        /// <summary>
        /// inputFilePath
        /// </summary>
        private const string inputFilePath = @"\Customer.csv";
        #endregion

        /// <summary>
        /// Constructor UnitTestCustomer
        /// </summary>
        public UnitTestCustomer()
        {
            inputFile = ReadCsvFile();
        }

        #region Testmethod

        /// <summary>
        /// To hceck csv File value
        /// </summary
        [TestMethod]
        public void ToCheckCsv_fileContent_Record_Found()
        {
            // arrange
            //act 
            var dt = CustomerRenewalInvitation.ReadCsvFile(inputFile);
            DataRow dr = dt.Rows[0];
            // assert
            Assert.AreEqual("1", dr["Id"]);
            Assert.AreEqual("Miss", dr["Title"]);
        }

        /// <summary>
        /// To hceck csv File value
        /// </summary
        [TestMethod]
        public void ToCheckCsv_fileContent_Record_Not_Found()
        {
            // arrange
            //act 
            var dt = CustomerRenewalInvitation.ReadCsvFile(inputFile);
            DataRow dr = dt.Rows[0];
            // assert
            Assert.AreNotEqual("2", dr["ID"]);
        }

        /// <summary>
        /// To Check Csv read File or not  
        /// </summary>
        [TestMethod]
        public void TocheckCalculation()
        {
            // arrange
            //act
            var dt = CustomerRenewalInvitation.ReadCsvFile(inputFile);
            DataRow dr = dt.Rows[0];
            CustomerRenewalInvitation custRenewalInvitation = new CustomerRenewalInvitation();
            bool res= custRenewalInvitation.Caluclation(dr);
            // assert
            decimal num = 0;
            //To check currency value in correct formate or /*not*/
            bool valueFormate = decimal.TryParse(dr["AnnualPremium"].ToString(), out num);
            Assert.IsTrue(valueFormate);

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Read Csv File Path
        /// </summary>
        /// <returns></returns>
        private string ReadCsvFile()
        {
           
            string inputFile = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName + inputFilePath;          
            return inputFile;
        }

        #endregion

    }
}
