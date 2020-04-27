using System;
using System.Collections.Specialized;
using System.Web;

namespace WebClient.Models
{
    public class ServerConfig
    {
#if DEBUG
        public Uri BaseUrl { get; } = new Uri("https://localhost");

        public int Port { get; } = 5001;
#else
        public Uri BaseUrl { get; } = new Uri("https://fernandreu.ddns.net");

        public int Port { get; } = 5001;
#endif

        public Uri UrlTo(string endpoint, NameValueCollection queryParameters = null)
        {
            endpoint ??= string.Empty;
            
            if (endpoint.StartsWith("/", StringComparison.InvariantCulture))
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
            return builder.Uri;
        }
    }
}
