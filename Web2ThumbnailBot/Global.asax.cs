using System.Web;
using System.Web.Http;

namespace Web2ThumbnailBot
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
