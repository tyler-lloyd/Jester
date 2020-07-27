using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Model;

namespace Application
{
    public class MyService
    {
        private readonly HttpClient _client;
        public MyService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<IList<Comment>> GetItem()
        {
            var response = await _client.GetAsync("/comments");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IList<Comment>>(content);
        }

        public async Task<IList<Comment>> PostItem()
        {
            var response = await _client.PostAsync("/comments", new StringContent("{\"hello\": \"world\"}"));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IList<Comment>>(content);
        }
    }
}
