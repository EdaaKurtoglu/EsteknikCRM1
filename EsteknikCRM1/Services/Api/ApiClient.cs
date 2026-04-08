using System;
using System.Net.Http;

namespace EsteknikCRM1.Services.Api
{
    public static class ApiClient
    {
        public static HttpClient Client { get; } = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7256/") // API portunu yaz
        };
    }
}