using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIAndroidApp
{
    public class AndroidHttpClientService
    {
        public HttpClient GetInsecureHttpClient()
        {
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            return new HttpClient(handler.GetPlatformMessageHandler());
        }

    }
}
