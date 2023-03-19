using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Application.Services.Interfaces
{
    public interface IIndexingService
    {
        Task Process(int fromBlock, int toBlock);
    }
}