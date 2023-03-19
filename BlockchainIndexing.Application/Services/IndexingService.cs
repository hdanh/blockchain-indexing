using BlockchainIndexing.Application.Models;
using BlockchainIndexing.Application.Services.Interfaces;
using BlockchainIndexing.Data.Context;
using BlockchainIndexing.Domain.Models;
using BlockchainIndexing.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlockchainIndexing.Application.Services
{
    public class IndexingService : IIndexingService
    {
        private IEtherscanService _etherscanService;
        private readonly ILogger<IndexingService> _logger;
        private readonly BlockchainContext _dbContext;

        public IndexingService(IEtherscanService etherscanService, ILogger<IndexingService> logger, BlockchainContext dbContext)
        {
            _etherscanService = etherscanService;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Process(int fromBlock, int toBlock)
        {
            for (int i = fromBlock; i <= toBlock; i++)
            {
                string blockNumberAsHex = i.ToHexString();
                var block = await _etherscanService.GetBlockByNumber(blockNumberAsHex);

                if (block == null)
                {
                    _logger.LogInformation($"Block #{i} not found!!!");
                    continue;
                }

                var transactionCount = await _etherscanService.GetBlockTransactionCountByNumber(blockNumberAsHex);

                await ProcessBlockAndTransaction(block, transactionCount);
            }
        }

        private async Task ProcessBlockAndTransaction(BlockDetail blockDetail, int transactionCount)
        {
            try
            {
                int blockNumber = blockDetail.BlockNumber.HexToInt32();

                Block model = await _dbContext.Blocks
                    .Include(x => x.Transactions)
                    .FirstOrDefaultAsync(x => x.BlockNumber == blockNumber);

                if (model == null)
                {
                    model = new Block()
                    {
                        BlockNumber = blockNumber,
                        BlockReward = blockDetail.BlockReward,
                        Hash = blockDetail.Hash,
                        Miner = blockDetail.Miner,
                        ParentHash = blockDetail.ParentHash,
                        GasUsed = blockDetail.GasUsed.HexToDecimal(),
                        GasLimit = blockDetail.GasLimit.HexToDecimal(),
                    };

                    if (model.Transactions == null)
                    {
                        model.Transactions = new List<Transaction>();
                    }

                    if (transactionCount > 0)
                    {
                        for (int i = 0; i < transactionCount; i++)
                        {
                            var detail = await _etherscanService.GetTransactionByBlockNumberAndIndex(blockDetail.BlockNumber, i.ToHexString());
                            var transaction = new Transaction()
                            {
                                From = detail.From,
                                To = detail.To,
                                Gas = detail.Gas.HexToDecimal(),
                                GasPrice = detail.GasPrice.HexToDecimal(),
                                TransactionIndex = i,
                                Hash = detail.Hash,
                                Value = detail.Value.HexToDecimal()
                            };

                            model.Transactions.Add(transaction);
                        }
                    }

                    _dbContext.Blocks.Add(model);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing block and transaction");
            }
        }
    }
}