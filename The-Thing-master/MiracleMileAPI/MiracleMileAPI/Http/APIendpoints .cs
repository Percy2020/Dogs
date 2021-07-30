using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Http
{
    public static class APIendpoints
    {
        public static string GetProfile { get { return "https://marty-dev.test-danicapension.se/api/selfservice/v1/customer/profile"; } }
        public static string PostProfile { get { return "https://marty-dev.test-danicapension.se/api/selfservice/v1/customer/profile"; } }
        public static string GetAccounts { get { return "https://marty-dev.test-danicapension.se/api/selfservice/v1/customer/accounts"; } }
        public static string PostAccounts { get { return "https://marty-dev.test-danicapension.se/api/selfservice/v1/customer/accounts"; } }
    }
}
