using System;
using System.Collections.Generic;
using System.Text;

namespace SMSActivate.RequestResults
{
    public class Phone
    {
        public int Number { get; private set; }
        public int Id { get; private set; }
        public bool IsUsed { get; private set; }

        public Phone(int id, int number)
        {
            Number = number;
            Id = id;
        }

        public void UsePhone() => IsUsed = true;
    }
}
