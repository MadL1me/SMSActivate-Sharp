using SMSActivate.RequestResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMSActivate
{
    public enum ActivationStatus
    {
        ReadyForSms = 1,
        RequestCodeAgain = 3,
        FinishActivation = 6,
        DenyActivation = 8
    }

    public class SMSActivateClient
    {
        public string APIKey { get; private set; }

        public readonly HttpClient HttpClient = new HttpClient();
        
        public readonly string DefaultRoute = "https://sms-activate.ru/stubs/handler_api.php";
        public readonly string ReferalLink = "https://sms-activate.ru/?ref=421489";


        public SMSActivateClient(string APIKey)
        {
            this.APIKey = APIKey;
        }

        public void ChangeAPIKey(string APIKey)
        {
            this.APIKey = APIKey;
        }

        public async Task<GetNumberResult> GetNumberAsync(string service, bool isForward = false)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("api_key", APIKey);
            content.Add("action", "getNumber");
            content.Add("isForward", isForward.BooleanToString());
            content.Add("ref", ReferalLink);
            content.Add("service", service);
            
            var response = await Request(content);
            return new GetNumberResult(response);
        }

        public async Task<GetNumberResult> GetNumberAsync(Service service, bool isForward = false)
        {
            return await GetNumberAsync(ServiceInfo.GetServiceId(service), isForward);
        }

        public async Task<RequestResult> SetActivationStatus(int activationId, ActivationStatus activationStatus)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("api_key", APIKey);
            content.Add("action", "setStatus");
            content.Add("status", ((int)activationStatus).ToString());
            content.Add("id", activationId.ToString());
            var result = await Request(content);
            return new RequestResult(result);
        }

        public async Task<ActivationStatusResult> GetActivationStatus(int activationId)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("api_key", APIKey);
            content.Add("action", "getStatus");
            content.Add("id", activationId.ToString());
            var result = await Request(content);
            return new ActivationStatusResult(result);
        }

        public async Task<RequestResult> GetBalanceAsync()
        {
            return new RequestResult(await Request(new[] { new KeyValuePair<string, string>("api_key", APIKey), new KeyValuePair<string, string>("action", "getBalance") }));
        }

        public async Task<RequestResult> GetBalanceAndCashBack()
        {
            return new RequestResult(await Request(new[] { new KeyValuePair<string, string>("api_key", APIKey), new KeyValuePair<string, string>("action", "getBalanceAndCashBack") }));
        }

        #region Requests

        private async Task<HttpResponseMessage> Request(Dictionary<string, string> requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(requestContent);
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return Response;
        }

        private async Task<HttpResponseMessage> Request(KeyValuePair<string, string> requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(new[] { requestContent });
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return Response;
        }

        private async Task<HttpResponseMessage> Request(KeyValuePair<string, string>[] requestContent)
        {
            HttpContent content = new FormUrlEncodedContent(requestContent);
            var Response = await HttpClient.PostAsync(DefaultRoute, content);
            return Response;
        }

        #endregion
    }
}
