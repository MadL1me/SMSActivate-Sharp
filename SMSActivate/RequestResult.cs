using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SMSActivate
{
    public class RequestResult
    {
        public string value;
        public HttpResponseMessage Response;

        public RequestResult(HttpResponseMessage httpResponseMessage)
        {
            Response = httpResponseMessage;
            value = httpResponseMessage.Content.ReadAsStringAsync().Result;
        }
    }
}
