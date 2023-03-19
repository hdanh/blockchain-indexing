using BlockchainIndexing.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Application.Services.Interfaces
{
    public interface IEtherscanService
    {
        Task<BlockDetail> GetBlockByNumber(string tag);

        Task<int> GetBlockTransactionCountByNumber(string tag);

        Task<TransactionDetail> GetTransactionByBlockNumberAndIndex(string tag, string index);
    }
}