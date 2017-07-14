using System.Web.Http;
using System.Net.Http.Formatting;
using Owin;


namespace Ingest
{
    class Startup : IOwinAppBuilder
    {
        public static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            ConfigureFormatters(config.Formatters);

            appBuilder.UseWebApi(config);
        }
    }
}
