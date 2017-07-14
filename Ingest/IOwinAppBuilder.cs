using Owin;

namespace Ingest
{
    interface IOwinAppBuilder
    {
        void Configuration(IAppBuilder appBuilder);
    }
}
