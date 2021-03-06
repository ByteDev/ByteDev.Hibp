﻿using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ByteDev.Hibp.Http
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage source)
        {
            var json = await source.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static bool IsRateLimitedExceeded(this HttpResponseMessage source)
        {
            return (int)source.StatusCode == 429;
        }
    }
}