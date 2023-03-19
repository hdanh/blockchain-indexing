using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlockchainIndexing.Application.Models
{
    public class BlockResult
    {
        public BlockDetail Result { get; set; }
    }

    public class BlockDetail
    {
        [JsonPropertyName("number")]
        public string BlockNumber { get; set; }

        public string Hash { get; set; }
        public string ParentHash { get; set; }
        public string Miner { get; set; }
        public decimal BlockReward { get; set; }
        public string GasLimit { get; set; }
        public string GasUsed { get; set; }
    }
}