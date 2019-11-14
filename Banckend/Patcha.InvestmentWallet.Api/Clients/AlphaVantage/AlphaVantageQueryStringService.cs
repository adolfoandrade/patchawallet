using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Services.AlphaVantage
{
    public class AlphaVantageQueryStringService
    {
        public static Uri AppendQueryString(string path, Dictionary<string, object> parameter)
        {
            return CreateUrl(path, parameter);
        }

        public static Uri AppendQueryString(string path)
        {
            return CreateUrl(path, new Dictionary<string, object>());
        }

        private static Uri CreateUrl(string path, Dictionary<string, object> parameter)
        {
            parameter.Add("apikey", "T0COGNVH2VBFPHH9");
            var urlParameters = new List<string>();
            foreach (var par in parameter)
            {
                urlParameters.Add(par.Value == null || string.IsNullOrWhiteSpace(par.Value.ToString()) ? null : $"{par.Key}={par.Value}");
            }

            var encodedParams = urlParameters
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(WebUtility.HtmlEncode)
                .Select((x, i) => i > 0 ? $"&{x.ToLower()}" : $"?{x.ToLower()}")
                .ToArray();
            var url = encodedParams.Length > 0 ? $"{path}{string.Join(string.Empty, encodedParams)}" : path;

            return new Uri(BaseApiEndPointUrl.ALPHA_VANTAGE_API, url);
        }
    }
}
