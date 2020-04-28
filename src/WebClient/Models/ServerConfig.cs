﻿using System;
using System.Collections.Specialized;
using System.Web;

namespace WebClient.Models
{
    public class ServerConfig
    {
        public ServerConfig()
        {
#if DEBUG
            BaseUrl  = new Uri("https://localhost");

            Port = 5001;
#else
            BaseUrl = new Uri("https://fernandreu.ddns.net");

            Port = 5001;
#endif
        }

        public ServerConfig(Uri baseUrl, int port)
        {
            BaseUrl = baseUrl;
            Port = port;
        }
        
        public Uri BaseUrl { get; }

        public int Port { get; }

        public Uri UrlTo(string endpoint, NameValueCollection queryParameters = null)
        {
            endpoint ??= string.Empty;
            
            if (endpoint.StartsWith("/", StringComparison.InvariantCulture))
            {
                endpoint = endpoint.Substring(1);
            }

            var builder = new UriBuilder(BaseUrl)
            {
                Path = endpoint,
            };
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
