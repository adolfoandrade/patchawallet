using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatchaWallet.Stocks
{
    public class BestMatches
    {
        [JsonProperty("bestMatches")] 
        public List<SymbolSearch> SymbolsSearch { get; set; }
    }
}
