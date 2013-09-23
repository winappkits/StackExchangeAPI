using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

/*
* LICENSE: https://raw.github.com/apimash/StarterKits/master/LicenseTerms-SampleApps%20.txt
*/

/*
 *
 *  A P I   M A S H 
 *
 * This class makes the HTTP call and deserialzies the stream to a supplied Type
*/

namespace APIMASHLib
{
    public class APIMASHMap 
    {
        public static T DeserializeObject<T>(string objString)
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore,DateFormatHandling = DateFormatHandling.IsoDateFormat};
            return (T) JsonConvert.DeserializeObject<T>(objString, settings);
        }

        public async Task<T> LoadObject<T>(string apiCall)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }
            var httpClient = new HttpClient(handler);
            var uriAPICall = new Uri(apiCall);
            var objString = await httpClient.GetStringAsync(uriAPICall);
            return DeserializeObject<T>(objString);
        }
    }
}
