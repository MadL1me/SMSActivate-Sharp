using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SMSActivate
{
    public class RequestResult
    {
        public string Result { get; protected set; }
        public HttpResponseMessage Response { get; protected set; }

        public RequestResult(HttpResponseMessage message)
        {
            Response = message;
            Result = message.Content.ReadAsStringAsync().Result;
        }
    }
}