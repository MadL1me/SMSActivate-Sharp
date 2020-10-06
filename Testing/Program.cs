using System;
using SMSActivate;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var activate = new SMSActivateClient("A30503A622ef26d89533867f1ee4df29");
            var b = activate.GetNumberAsync(Service.AnyOther);
            //var c = activate.SetActivationStatus(281216893, ActivationStatus.ReadyForSms);
            b.Wait();
            var c = activate.SetActivationStatus(b.Result.Phone.Id, ActivationStatus.ReadyForSms);
            c.Wait();
            //Console.WriteLine(a.Result);
            Console.WriteLine(b.Result.Phone.Number);
            Console.WriteLine(c.Result.Result);
        }
    }
}