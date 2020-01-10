using System;

namespace PatchaWallet.Stocks
{
    public class BaseApiEndPointUrl
    {
        public static readonly Uri COIN_GECKO_API = new Uri("https://api.coingecko.com/api/v3/");
        public static readonly Uri ALPHA_VANTAGE_API = new Uri("https://www.alphavantage.co/query/");

        public static readonly Uri MERCADOBITCOIN_DATA_API = new Uri("https://www.mercadobitcoin.net/api/");
        public static readonly Uri MERCADOBITCOIN_TRADE_API = new Uri("https://www.mercadobitcoin.net/tapi/v3/");

        public static readonly Uri BRAZILIEX_API = new Uri("https://braziliex.com/api/v1/");
        public static readonly Uri TEMBTC_API = new Uri("https://broker.tembtc.com.br/api/v3/");
        public static readonly Uri BITCOINTOYOU_API = new Uri("https://www.bitcointoyou.com/API/");
        public static readonly Uri BITNUVEM_API = new Uri("https://bitnuvem.com/api/");
        public static readonly Uri BITRECIFE_API = new Uri("https://exchange.bitrecife.com.br/api/v3/");
        public static readonly Uri BITCOINTRADE_API = new Uri("https://api.bitcointrade.com.br/v2/");
        public static readonly Uri BITPRECO_API = new Uri("https://api.bitpreco.com/");
        public static readonly Uri IIIXBIT_API = new Uri("https://api.exchange.3xbit.com.br/");
        public static readonly Uri BITBLUE_API = new Uri("https://bitblue.com/api/");
        public static readonly Uri NEGOCIECOINS_API = new Uri("https://broker.negociecoins.com.br/api/v3/");
        public static readonly Uri FLOWBTC_API = new Uri("https://publicapi.flowbtc.com.br/v1/");
    }
}
