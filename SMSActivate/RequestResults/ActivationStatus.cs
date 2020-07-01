using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SMSActivate.RequestResults
{
    public class ActivationStatusResult : RequestResult
    {
        public string ActivationCode { get; protected set; }

        public ActivationStatusResult(HttpResponseMessage message) : base(message)
        {
            var result = message.Content.ReadAsStringAsync().Result;

            if (result.Contains("OK"))
            {
                ActivationCode = result.Split(':')[1];
            }
        }
    }
}
