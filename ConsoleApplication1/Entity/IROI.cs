using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSoln.Entity
{
    public interface IROI
    {
        string TypeOfAccount { get; set; }
        void GetRateofInterest();
    }
}
