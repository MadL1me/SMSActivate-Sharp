using System;
using System.Collections.Generic;
using System.Text;

namespace SMSActivate
{
    public enum Country
    {
        Russia = 0,
        Ukraine,
        Kazahstan,
        China,
        Philipines,
    }

    public enum Service
    {
        Vkontakte,
        Telegram,
        Google,
        MailRu,
        AnyOther
    }

    public enum Operators
    {
        Megafon,
        Mts,
        BeeLine
    }

    public static class ServiceInfo
    {
        private static Dictionary<Service, string> _servicesId = new Dictionary<Service, string>()
        {
            {Service.Vkontakte, "vk"}, {Service.Telegram, "tg"}, {Service.AnyOther, "ot"}, {Service.Google, "go"},
            {Service.MailRu, "ma" }
        };

        public static string GetServiceId(Service serviceName) => _servicesId[serviceName];
    }
}
