using Newtonsoft.Json;

namespace Patcha.InvestmentWallet.Core.IIIxbit.Entities.Response
{
    public class IIIxbitOrder
    {
        [JsonProperty("unit_price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }
    }
}
