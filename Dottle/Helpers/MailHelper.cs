using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dottle.Helpers
{
    public class MailHelper
    {
        public async void SendEmail()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://rapidprod-sendgrid-v1.p.rapidapi.com/mail/send"),
                Headers =
                {
                    { "x-rapidapi-key", "7d32c1757fmsh2d901c55708746dp176860jsn1b702f932d65" },
                    { "x-rapidapi-host", "rapidprod-sendgrid-v1.p.rapidapi.com" },
                },
                Content = new StringContent("{\n    \"personalizations\": [\n        {\n            \"to\": [\n                {\n                    \"email\": \"john@example.com\"\n                }\n            ],\n            \"subject\": \"Hello, World!\"\n        }\n    ],\n    \"from\": {\n        \"email\": \"from_address@example.com\"\n    },\n    \"content\": [\n        {\n            \"type\": \"text/plain\",\n            \"value\": \"Hello, World!\"\n        }\n    ]\n}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }            
        }
    }
}
