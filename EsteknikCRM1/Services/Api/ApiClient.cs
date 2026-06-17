using System;
using System.Net.Http;

namespace EsteknikCRM1.Services.Api
{
    public static class ApiClient
    {
        public static HttpClient Client { get; } = new HttpClient
        {
            BaseAddress = new Uri("http://185.95.164.196:5000/") // API portunu yaz
        };
        }
}