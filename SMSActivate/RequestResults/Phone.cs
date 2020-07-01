using System;
using System.Collections.Generic;
using System.Text;

namespace SMSActivate.RequestResults
{
    public class Phone
    {
        public string Number { get; private set; }
        public string Id { get; private set; }
        public bool IsUsed { get; private set; }

        public Phone(string id, string number)
        {
            Number = number;
            Id = id;
        }

        public void UsePhone() => IsUsed = true;
    }
}
