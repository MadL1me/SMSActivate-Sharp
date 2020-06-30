using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMSActivate
{
    public class SMSActivateClient
    {
        public string APIKey { get; private set; }
        public readonly HttpClient HttpClient;
        public readonly string DefaultRoute = "https://sms-activate.ru/stubs/handler_api.php";

        public SMSActivateClient(string APIKey)
        {
            this.APIKey = APIKey;
            HttpClient = new HttpClient();
        }

        public void ChangeAPIKey(string APIKey)
        {
            this.APIKey = APIKey;
        }

        public async Task<RequestResult> Request(Dictionary<string, string> requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(requestContent);
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return new RequestResult(Response);
        }

        public async Task<RequestResult> Request(KeyValuePair<string, string> requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(new[] { requestContent });
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return new RequestResult(Response);
        }

        public async Task<RequestResult> Request(KeyValuePair<string, string>[] requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(requestContent);
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return new RequestResult(Response);
        }

        public RequestResult GetBalance()
        {
            var response = Request(new[] { new KeyValuePair<string, string>("api_key", APIKey), new KeyValuePair<string, string>("action", "getBalance") });
            response.Wait();
            return response.Result;
        }
    }
}
