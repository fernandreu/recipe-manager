using System;
using System.Collections.Specialized;
using System.Web;

namespace WebClient.Models
{
    public class ServerConfig
    {
#if DEBUG
        public string BaseUrl { get; } = "https://localhost";

        public int Port { get; } = 5001;
#else
        public string BaseUrl { get; } = "http://fernandreu.ddns.net";

        public int Port { get; } = 5000;
#endif

        public string UrlTo(string endpoint, NameValueCollection queryParameters = null)
        {
            if (endpoint.StartsWith("/"))
            {
                endpoint = endpoint.Substring(1);
            }

            var builder = new UriBuilder($"{BaseUrl}/{endpoint}");
            if (Port > 0)
            {
                builder.Port = Port;
            }
            
            var query = HttpUtility.ParseQueryString(builder.Query);
            if (queryParameters != null)
            {
                query.Add(queryParameters);
            }

            builder.Query = query.ToString();
            return builder.ToString();
        }
    }
}
