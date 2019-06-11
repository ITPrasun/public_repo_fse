using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSoln.Entity
{
    public class Savings_Acct : Account, IROI, ITransaction
    {
        public string TypeOfAccount { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }

        public int MaxAmountPerDay = 10000;
        public Savings_Acct():base()
        {

        }

        public Savings_Acct(int balc, int acct_num, string name) : base( balc,  acct_num, name)
        {

        }

        public void GetRateofInterest()
        {
            Console.WriteLine("Rate of interest is 5%");
        }
        public Dictionary<string, object> TransferAmount(Dictionary<string, object> lst, int amt)
        {
            var tpl = lst;
            try
            {
                if (amt < MaxAmountPerDay)
                {
                    var lstSav = lst.Where(x => x.Key == "CURRENT") as List<Current_Acct>;
                    var item_Frm = lstSav.Where(x => x.Acct_Number == Convert.ToInt64(FromAccount)).FirstOrDefault();
                    var item_To = lstSav.Where(x => x.Acct_Number == Convert.ToInt64(ToAccount)).FirstOrDefault();
                    if (item_Frm != null && item_To != null)
                    {
                        lstSav.Remove(item_Frm);
                        lstSav.Remove(item_To);
                        item_Frm.Balance = item_Frm.Balance - amt;
                        item_To.Balance = item_To.Balance + amt;
                        lstSav.Add(item_Frm);
                        lstSav.Add(item_To);
                        //return FromAccount + ToAccount;
                    }
                    tpl = new Dictionary<string, object>();
                    tpl.Add("SAVINGS", lstSav);

                    Console.WriteLine("Amount transferred successfully.");
                }
                else
                {
                    var str = "Amount greater than " + MaxAmountPerDay + " cannot be transferred;";
                    throw new FundTransException(str);
                }
            }
            catch(FundTransException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tpl;
        }

        public void GetAccountDetails()
        {
            Console.Write("Account Number :");
            Console.WriteLine(this.Acct_Number);
            Console.Write("User Name :");
            Console.WriteLine(this.User_Name);
            Console.Write("Balance Amount :");
            Console.WriteLine(this.Balance);
        }
    } 
}
