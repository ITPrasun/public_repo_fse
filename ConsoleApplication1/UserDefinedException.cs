using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSoln
{
    public class MinBalcException : Exception
    {
        public MinBalcException(string message) : base(message)
        {
        }
    }

    public class FundTransException : Exception
    {
        public FundTransException(string message) : base(message)
        {
        }
    }
}
