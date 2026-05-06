using System;
using System.Net.Http;

namespace EsteknikCRM1.Services.Api
{
    public static class ApiClient
    {
        public static HttpClient Client { get; } = new HttpClient
        {
            BaseAddress = new Uri("https://api.esbemuhendislik.com/") // API portunu yaz
        };
    }
}