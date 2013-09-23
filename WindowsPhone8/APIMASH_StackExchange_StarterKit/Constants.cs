using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMASH_StackExchange_StarterKit
{
    static class Constants
    {
        public const string Stack = "stackoverflow";

        public static readonly string QuestionsApi = String.Format(ApiFormat, ApiServer, ApiVersion, "questions",Stack);

        public const string ApiFormat = "http://{0}/{1}/{2}?order=desc&sort=activity&site={3}&filter=withbody";
        public const string ApiServer = "api.stackexchange.com";
        public const string ApiVersion = "2.1";

    }
}
