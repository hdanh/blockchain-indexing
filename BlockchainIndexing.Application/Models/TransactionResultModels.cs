using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Application.Models
{
    public class TransactionCountResultModel
    {
        public string Result { get; set; }
    }

    public class TransactionDetailResult
    {
        public TransactionDetail Result { get; set; }
    };

    public class TransactionDetail
    {
        public string Hash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Value { get; set; }
        public string Gas { get; set; }
        public string GasPrice { get; set; }
        public string TransactionIndex { get; set; }
    }
}