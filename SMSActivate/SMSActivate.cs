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
        public readonly string ReferalLink = "https://sms-activate.ru/?ref=421489";

        public SMSActivateClient(string APIKey)
        {
            this.APIKey = APIKey;
            HttpClient = new HttpClient();
        }

        public void ChangeAPIKey(string APIKey)
        {
            this.APIKey = APIKey;
        }

        public async Task<RequestResult> GetNumberAsync(string service, bool isForward = false)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("api_key", APIKey);
            content.Add("action", "getNumber");
            content.Add("isForward", BooleanToString(isForward));
            content.Add("ref", ReferalLink);
            content.Add("service", service);
            
            var response = await Request(content);
            return response;
        }

        public RequestResult GetNumber(string service, bool isForward = false)
        {
            var task = GetNumberAsync(service, isForward);
            task.Wait();
            return task.Result;
        }

        public void GetNumber(Service service, bool isForward = false) => GetNumber(ServiceInfo.GetServiceId(service), isForward);

        public Task<RequestResult> GetBalanceAsync()
        {
            return Request(new[] { new KeyValuePair<string, string>("api_key", APIKey), new KeyValuePair<string, string>("action", "getBalance") });
        }

        public RequestResult GetBalance()
        {
            var response = GetBalanceAsync();
            response.Wait();
            return response.Result;
        }

        private string BooleanToString(bool value) => (value) ? "0" : "1";

        #region Requests

        private async Task<RequestResult> Request(Dictionary<string, string> requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(requestContent);
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return new RequestResult(Response);
        }

        private async Task<RequestResult> Request(KeyValuePair<string, string> requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(new[] { requestContent });
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return new RequestResult(Response);
        }

        private async Task<RequestResult> Request(KeyValuePair<string, string>[] requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(requestContent);
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return new RequestResult(Response);
        }

        #endregion
    }
}
