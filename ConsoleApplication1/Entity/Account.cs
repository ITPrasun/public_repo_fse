using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSoln.Entity
{
    public abstract class Account
    {
        public int Acct_Number { get; set; }
        public string User_Name { get; set; }
        public int Balance { get; set; }

        protected Account()
        {

        }

         protected Account(int balc, int acct_num, string name)
        {
            Balance = balc;
            Acct_Number = acct_num;
            User_Name = name;
        }

       
        public bool OpenAccount(string userName,int balance)
        {
            if (!Check_Balance(balance))
            {
                return false;
            }
            else
            {
                Random random = new Random();
                Acct_Number = (int) ((random.Next(1,100) * 900000000000) + 100000000000);
                User_Name = userName;
                Balance = balance;

                Console.WriteLine("Your account number is: ");
                Console.WriteLine(Acct_Number);
                return true;
            }

            
        }

        public bool CloseAccount(int balance)
        {
            bool isValid = true;
            if(balance>0)
            {
                isValid = false;
            }
            return isValid;
        }

        public bool EditAccount(string userName)
        {
            bool isValid = false;
            if (!string.IsNullOrWhiteSpace(userName) &&
                userName.Length>2)
            {
                isValid = true;
            }
            return isValid;
        }

        public void Deposit( int amt)
        {
            this.Balance = this.Balance + amt;
        }

        public void Withdrawal(int amt)
        {
            if (this.Balance > amt)
            {
                this.Balance = this.Balance - amt;
            }
        }
        public bool Check_Balance(int balance)
        {
            bool isValid = true;
            if(balance<1000)
            {
                isValid = false;
            }
            return isValid;
        }

        ~Account()
        {
            Acct_Number = 0;
            User_Name = null;
            Balance = 0;
        }

    }
}
