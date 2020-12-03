using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWEB.Utility
{
    public class Resource
    {
        public const string APIBaseUrl = "https://localhost:44350/";
        public const string ProductAPIUrl = APIBaseUrl + "api/product/";
        public const string RegisterAPIUrl = APIBaseUrl + "api/user/register";
        public const string LoginAPIUrl = APIBaseUrl + "api/user/login";
        public const string ContentType = "application/json";
    }
}
