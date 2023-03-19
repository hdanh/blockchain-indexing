using BlockchainIndexing.Application.Models;
using BlockchainIndexing.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace BlockchainIndexing.Application.Services
{
    public class EtherscanService : IEtherscanService
    {
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly HttpClient _httpClient;
        private readonly ILogger<EtherscanService> _logger;

        private const string GetBlockByNumberAction = "eth_getBlockByNumber";
        private const string GetBlockTransactionCountByNumberAction = "eth_getBlockTransactionCountByNumber";
        private const string GetTransactionByBlockNumberAndIndexAction = "eth_getTransactionByBlockNumberAndIndex";

        public EtherscanService(IConfiguration configuration, HttpClient httpClient, ILogger<EtherscanService> logger)
        {
            _configuration = configuration;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<BlockDetail> GetBlockByNumber(string tag)
        {
            BlockResult result = null;

            var uri = GetUri(GetBlockByNumberAction, tag);

            using (var response = await _httpClient.GetAsync(uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<BlockResult>(apiResponse, _serializerOptions);
            }

            return result?.Result;
        }

        public async Task<int> GetBlockTransactionCountByNumber(string tag)
        {
            TransactionCountResultModel apiResult = null;
            int result = 0;

            try
            {
                var uri = GetUri(GetBlockTransactionCountByNumberAction, tag);

                using (var response = await _httpClient.GetAsync(uri))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    apiResult = JsonSerializer.Deserialize<TransactionCountResultModel>(apiResponse, _serializerOptions);
                }

                if (apiResult != null)
                {
                    result = Convert.ToInt32(apiResult.Result, 16);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public async Task<TransactionDetail> GetTransactionByBlockNumberAndIndex(string tag, string index)
        {
            TransactionDetailResult result = null;
            var uri = GetUri(GetTransactionByBlockNumberAndIndexAction, tag, index);

            using (var response = await _httpClient.GetAsync(uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<TransactionDetailResult>(apiResponse, _serializerOptions);
            }

            return result?.Result;
        }

        private string GetUri(string action, string tag, string index = null)
        {
            var stringBuilder = new StringBuilder($"api?module=proxy&apikey={_configuration["ApiKey"]}&tag={tag}&action={action}");

            if (action.Equals("eth_getBlockByNumber"))
            {
                stringBuilder.Append("&boolean=true");
            }
            else if (action.Equals("eth_getTransactionByBlockNumberAndIndex"))
            {
                stringBuilder.Append($"&index={index}");
            }

            return stringBuilder.ToString();
        }
    }
}