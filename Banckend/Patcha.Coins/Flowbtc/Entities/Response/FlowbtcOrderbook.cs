using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class FlowbtcOrderbook
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("Data")]
        public FlowbtcOrderbookData Data { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }

}
