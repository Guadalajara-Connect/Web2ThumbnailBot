using System;
using System.Net;

namespace Web2ThumbnailBot.Helpers
{
    public static class UrlUtilities
    {
        public const string CStrHttp = "http://";
        public const string CStrHttps = "https://";
        public const string CStrThumbApi = "http://api.webthumbnail.org/?";
        public const string CStrApiParms = "width=1024&height=1024&screen=1024&url=";

        public static bool IsValidUri(string uriName, out string exMsg)
        {
            uriName = uriName.ToLower().Replace(CStrHttps, string.Empty);
            uriName = uriName.ToLower().Contains(CStrHttp) ? uriName : CStrHttp + uriName;
            return CheckUri(uriName, out exMsg);
        }

        public static bool CheckUri(string uri, out string exMsg)
        {
            exMsg = string.Empty;
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead(uri))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                exMsg = ex.Message;
                return false;
            }
        }
    }
}