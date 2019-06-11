using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSoln.Entity;

namespace BankingSoln
{
    class Banking
    {

        public static List<Savings_Acct> s_Acct_List = new List<Savings_Acct>();
        public static List<Current_Acct> c_Acct_List = new List<Current_Acct>();
        
        static void Main(string[] args)
        {
            string isCont = "Y";
            do
            {
                AccountProcessing();
                Console.WriteLine("Press Y to continue another transaction");

                isCont = Console.ReadLine();

            } while (isCont == "Y");
            CheckForMinBalance();
            Console.ReadLine();

        }
        public static void CheckForMinBalance()
        {
            if (c_Acct_List != null && c_Acct_List.Count > 0)
            {
                var cB = new Current_Acct();
                foreach (var acct in c_Acct_List)
                {
                    try
                    {
                        if (acct.Balance < cB.MinBalanceRequired)
                        {
                            var str ="Account has low balance : "+ acct.Acct_Number;
                            throw new MinBalcException(str);
                        }
                    }
                    catch(MinBalcException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public static void AccountProcessing()
        {
            Console.WriteLine("---- Press O for New Account ----");
            Console.WriteLine("---- Press E for Edit Account ----");
            Console.WriteLine("---- Press R for Remove Account ----");
            Console.WriteLine("---- Press D for Deposit Fund Account ----");
            Console.WriteLine("---- Press V for View Account ----");
            Console.WriteLine("---- Press W for Withdraw from Account ----");
            Console.WriteLine("---- Press I for Get rate of interest ----");
            Console.WriteLine("---- Press T for Transfer of Funds ----");

            string inp = Console.ReadLine();


            switch (inp)
            {
                case "O":
                    OpenNewAcct();
                    break;
                case "E":
                    EditExistingAccount();
                    break;
                case "R":
                    CloseAccount();
                    break;
                case "D":
                    DepositIntoAcct();
                    break;
                case "V":
                    ViewAccount();
                    break;
                case "W":
                    WithdrawFromAcct();
                    break;
                case "I":
                    GetRateOfInterest();
                    break;
                case "T":
                    TransferAmount();
                    break;

            }

        }
        public static void TransferAmount()
        {
            Console.WriteLine("Enter the account number, from where the amount will be deducted:");
            string acctF = Console.ReadLine();
            Console.WriteLine("Enter the account number, from where the amount will be added:");
            string acctT = Console.ReadLine();
            Console.WriteLine("Enter the amount to be transferred to the account:");
            string amt = Console.ReadLine();
            var item_S = s_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acctF)).FirstOrDefault();
            if (item_S != null)
            {
                Savings_Acct sc = new Savings_Acct();
                sc.FromAccount = Convert.ToInt32(acctF);
                sc.ToAccount = Convert.ToInt32(acctT);
                if(sc.FromAccount >0 && sc.ToAccount >0)
                {
                    var tpl = new Dictionary<string, object>();
                    tpl.Add("SAVINGS", s_Acct_List);
                    var data =sc.TransferAmount(tpl, Convert.ToInt32(amt));
                    s_Acct_List = data.Where(x => x.Key == "SAVINGS") as List<Savings_Acct>;
                }
            }
            else
            {
                var item_C = c_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acctF)).FirstOrDefault();
                if (item_C != null)
                {
                    Current_Acct sc = new Current_Acct();
                    sc.FromAccount = Convert.ToInt32(acctF);
                    sc.ToAccount = Convert.ToInt32(acctT);
                    if (sc.FromAccount > 0 && sc.ToAccount > 0)
                    {
                        var tpl = new Dictionary<string, object>();
                        tpl.Add("CURRENT", s_Acct_List);
                        var data= sc.TransferAmount(tpl, Convert.ToInt32(amt));
                        c_Acct_List = data.Where(x => x.Key == "CURRENT") as List<Current_Acct>;
                    }
                    Console.WriteLine("Amount transferred successfully!");
                }
            }
        }
        public static void GetRateOfInterest()
        {
            Console.WriteLine("Press S for Savings Account");
            Console.WriteLine("Press C for Current Account");

            string inp = Console.ReadLine();

            if (string.Equals(inp, "S", StringComparison.OrdinalIgnoreCase))
            {
                var sv = new Savings_Acct();
                sv.GetRateofInterest();
            }
            else if (string.Equals(inp, "C", StringComparison.OrdinalIgnoreCase))
            {
                var sv = new Current_Acct();
                sv.GetRateofInterest();
            }
        }
        public static void WithdrawFromAcct()
        {
            Console.WriteLine("Enter the Account Number");
            string acct = Console.ReadLine();
            Console.WriteLine("Enter the Amount to be withdrawn:");
            string amt = Console.ReadLine();
            var item_S = s_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
            if (item_S != null)
            {
                Savings_Acct sc = new Savings_Acct();
                sc.Withdrawal(Convert.ToInt32(amt));

                s_Acct_List.Remove(item_S);
                item_S.Balance = Convert.ToInt32(amt);
                s_Acct_List.Add(item_S);
                Console.WriteLine("Amount withdrawn successfully!");
            }
            else
            {
                var item_C = c_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
                if (item_C != null)
                {
                    Current_Acct sc = new Current_Acct();
                    sc.Withdrawal(Convert.ToInt32(amt));

                    c_Acct_List.Remove(item_C);
                    item_C.Balance = Convert.ToInt32(amt);
                    c_Acct_List.Add(item_C);
                    Console.WriteLine("Amount withdrawn successfully!");
                }
            }
        }
        public static void DepositIntoAcct()
        {
            Console.WriteLine("Enter the Account Number");
            string acct = Console.ReadLine();
            Console.WriteLine("Enter the Amount to be deposited:");
            string amt = Console.ReadLine();
            var item_S = s_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
            if (item_S != null)
            {
                Savings_Acct sc = new Savings_Acct();
                sc.Deposit(Convert.ToInt32(amt));

                s_Acct_List.Remove(item_S);
                item_S.Balance = Convert.ToInt32(amt);
                s_Acct_List.Add(item_S);
                Console.WriteLine("Amount deposited successfully!");
            }
            else
            {
                var item_C = c_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
                if (item_C != null)
                {
                    Current_Acct sc = new Current_Acct();
                    sc.Deposit(Convert.ToInt32(amt));

                    c_Acct_List.Remove(item_C);
                    item_S.Balance = Convert.ToInt32(amt);
                    c_Acct_List.Add(item_C);
                    Console.WriteLine("Amount deposited successfully!");
                }
            }
        }
        public static void EditExistingAccount()
        {
            Console.WriteLine("Enter the Account Number");
            string acct = Console.ReadLine();
            Console.WriteLine("Enter the new Name");
            string name = Console.ReadLine();
            var item_S = s_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
            if (item_S != null)
            {
                Savings_Acct sc = new Savings_Acct();
                if (sc.EditAccount(name))
                {
                    s_Acct_List.Remove(item_S);
                    item_S.User_Name = name;
                    s_Acct_List.Add(item_S);
                    Console.WriteLine("Account edited successfully!");
                }
                else
                {
                    Console.WriteLine("Error processing account!!");

                }
            }
            else
            {
                var item_C = c_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
                if (item_C != null)
                {
                    Current_Acct sc = new Current_Acct();
                    if (sc.EditAccount(name))
                    {
                        c_Acct_List.Remove(item_C);
                        item_C.User_Name = name;
                        c_Acct_List.Add(item_C);
                        Console.WriteLine("Account edited successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Error processing account!!");

                    }
                }
            }
        }
        public static void CloseAccount()
        {
            Console.WriteLine("Enter the Account Number");
            string acct = Console.ReadLine();
            var item_S = s_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
            if (item_S != null)
            {
                Savings_Acct sc = new Savings_Acct();
                if (sc.CloseAccount(item_S.Balance))
                {
                    s_Acct_List.Remove(item_S);
                    Console.WriteLine("Account closed successfully!");
                }
                else
                {
                    Console.WriteLine("Error processing account!!");

                }
            }
            else
            {
                var item_C = c_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
                if (item_C != null)
                {
                    Current_Acct sc = new Current_Acct();
                    if (sc.CloseAccount(item_C.Balance))
                    {
                        c_Acct_List.Remove(item_C);
                        Console.WriteLine("Account closed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Error processing account!!");

                    }
                }
            }
        }
        public static void ViewAccount()
        {
            Console.WriteLine("Enter the Account Number");
            string acct = Console.ReadLine();
            var item_S = s_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
            if (item_S != null)
            {
                var sv = new Savings_Acct(item_S.Balance, item_S.Acct_Number, item_S.User_Name);
                sv.GetAccountDetails();
            }
            var item_C = c_Acct_List.Where(x => x.Acct_Number == Convert.ToInt64(acct)).FirstOrDefault();
            if (item_C != null)
            {
                var sv = new Current_Acct(item_C.Balance, item_C.Acct_Number, item_C.User_Name);
                sv.GetAccountDetails();
            }

        }
        public static void OpenNewAcct()
        {
            Console.WriteLine("Press S for Savings Account");
            Console.WriteLine("Press C for Current Account");

            string inp = Console.ReadLine();

            Console.WriteLine("-- Enter user name: --");

            string name = Console.ReadLine();

            Console.WriteLine("Enter balance to be deposited:");

            string bal = Console.ReadLine();

            if (string.Equals(inp, "S", StringComparison.OrdinalIgnoreCase))
            {
                Savings_Acct acct = new Savings_Acct();
                bool isVlaid = acct.OpenAccount(name, Convert.ToInt32(bal));
                if (isVlaid)
                {
                    s_Acct_List.Add(acct);
                }
                else
                {
                    Console.WriteLine("Error processing Account!!");
                }
            }
            else if (string.Equals(inp, "C", StringComparison.OrdinalIgnoreCase))
            {
                Current_Acct acct = new Current_Acct();
                bool isVlaid = acct.OpenAccount(name, Convert.ToInt32(bal));
                if (isVlaid)
                {
                    c_Acct_List.Add(acct);
                }
                else
                {
                    Console.WriteLine("Error processing account!!");
                }
            }
        }

        ~Banking()
        {
            s_Acct_List = null;
            c_Acct_List = null;
        }
    }
}
