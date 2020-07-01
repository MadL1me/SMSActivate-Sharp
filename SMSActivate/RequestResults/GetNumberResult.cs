using SMSActivate.RequestResults;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SMSActivate
{
    public class GetNumberResult : RequestResult
    {
        public readonly Phone Phone;

        public GetNumberResult(HttpResponseMessage message) : base(message) 
        {
            var result = message.Content.ReadAsStringAsync().Result;
            
            if (result.Contains("ACCESS"))
            {
                var splited = result.Split(':');
                Phone = new Phone(result[1], result[2]);
            }
        }
    }
}
