using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSoln.Entity
{
    class Current_Acct: Account, IROI, ITransaction
    {
        public string TypeOfAccount { get; set; }
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }

        public int MinBalanceRequired = 1000;
        public Current_Acct():base()
        {

        }

        public Current_Acct(int balc, int acct_num, string name) : base(balc, acct_num, name)
        {

        }

        public void GetRateofInterest()
        {
            Console.WriteLine("Rate of interest is 3%");
        }
        public Dictionary<string, object>
            TransferAmount(Dictionary<string, object> lst, int amt)
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
            var tpl = new Dictionary<string, object>();
            tpl.Add("CURRENT", lstSav);
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
