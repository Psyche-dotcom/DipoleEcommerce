using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class TransactionData
    {

        public string message { get; set; }
        public string status { get; set; }
        public DataTransaction data { get; set; }
    }
}
