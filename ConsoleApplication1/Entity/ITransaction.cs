using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSoln.Entity
{
    public interface ITransaction
    {
        int FromAccount { get; set; }
        int ToAccount { get; set; }

        Dictionary<string, object> TransferAmount(Dictionary<string, object> lst, int amt);
    }
}
