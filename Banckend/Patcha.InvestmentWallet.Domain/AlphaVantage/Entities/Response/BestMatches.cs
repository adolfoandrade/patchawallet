using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response
{
    public class BestMatches
    {
        [JsonProperty("bestMatches")] 
        public List<SymbolSearch> SymbolsSearch { get; set; }
    }
}
