using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using RestSharp;
using RestSharp.Authenticators;

namespace Dottle.Helpers
{
    public static class MailHelper
    {
        public static void SendEmail(string toEmail, string toName, string fromEmail, string title, string message)
        {
            string finalTitle = $"{toName} {fromEmail} from Dottle sent you an inquiry - {title}";
            RestClient client = new RestClient
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api",
                    "13b050763639802749c03f06a88bb541-c50a0e68-23342223")
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox94c50c2865a84124b5f09bf6d184f748.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox94c50c2865a84124b5f09bf6d184f748.mailgun.org>");
            request.AddParameter("to", $"{toName} <{toEmail}>");
            request.AddParameter("subject", finalTitle);
            request.AddParameter("text", message);
            request.Method = Method.POST;
            client.Execute(request);
        }
    }
}
