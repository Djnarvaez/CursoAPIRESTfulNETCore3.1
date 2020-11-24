using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Util
{
    public class APITranslator
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public APITranslator(IHttpClientFactory httpClientFactory, 
                             IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<string> Translate(string text)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, configuration["Translator:url"]);

            Object[] objText = new Object[] { new { Text = text } };

            request.Content = new StringContent(JsonConvert.SerializeObject(objText), Encoding.UTF8, "application/json");

            request.Headers.Add(configuration["Translator:key"], configuration["Translator:value"]);

            var httpClient = httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<AzureTranslationModel>>(json);

            return model.First().translations.Select(x => x.text).FirstOrDefault();
        }
    }
}
