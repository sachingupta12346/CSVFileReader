using System;
using System.Text;

namespace RoyalLondonCsvReader
{
    /// <summary>
    /// TemplateFormat class to create the template
    /// </summary>
    #region TemplateFormat
    public class TemplateFormat
  {
        /// <summary>
        /// Method of the creating Template
        /// </summary>
        public static string Template()
        {
            StringBuilder sb = new StringBuilder();          
            sb.Append(DateTime.Now.ToString("dd/MM/yyyy"));
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("FAO: Title FirstName SurName");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("RE: Your Renewal");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Dear Title SurName");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("We hereby invite you to renew your Insurance Policy, subject to the following terms.");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Your chosen insurance product is ProductName.");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("The amount payable to you in the event of a valid claim will be £PayoutAmount.");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Your annual premium will be £AnnualPremium.");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("If you choose to pay by Direct Debit, we will add a credit charge of £creditCharge, bringing the total to £totalAnnualCreditCharge.");
            sb.Append("This is payable by an initial payment of £initialMonthlyPaymentAmount, followed by 11 payments of £otherMonthlyPaymentAmount each.");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Please get in touch with us to arrange your renewal by visiting https://www.regallutoncodingtest.co.uk/renew or calling us on 01625 123456.");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Kind Regards");
            sb.AppendLine();
            sb.Append("Regal Luton");
            return sb.ToString();
        }
  }
    #endregion
}
