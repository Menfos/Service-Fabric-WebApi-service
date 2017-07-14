using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System.Globalization;
using Microsoft.Owin.Hosting;

namespace Ingest
{
    internal class OwinCommunicationListener : ICommunicationListener
    {
        private readonly IOwinAppBuilder _startup;
        private readonly string _appRoot;
        private readonly StatelessServiceContext _serviceContext;

        private string _listeningAdress;

        private IDisposable _serverHandle;

        public OwinCommunicationListener(string appRoot, IOwinAppBuilder startup, StatelessServiceContext serviceContext)
        {
            this._appRoot = appRoot;
            this._startup = startup;
            this._serviceContext = serviceContext;
        }

        public void Abort()
        {
            StopWebServer();
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            StopWebServer();
            return Task.FromResult(true);
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var serviceEndpoint = _serviceContext.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            var port = serviceEndpoint.Port;
            var root = String.IsNullOrWhiteSpace(_appRoot) ? String.Empty : _appRoot.TrimEnd('/') + '/';

            _listeningAdress = String.Format(CultureInfo.InvariantCulture, "http://+:{0}/{1}", port, root);

            _serverHandle = WebApp.Start(_listeningAdress, appBuilder => _startup.Configuration(appBuilder));

            var publishAddress = _listeningAdress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);

            ServiceEventSource.Current.Message("Listening on {0}", publishAddress);
            return Task.FromResult(publishAddress);
        }

        private void StopWebServer()
        {
            if(_serverHandle != null)
            {
                try
                {
                    _serverHandle.Dispose();
                }
                catch(ObjectDisposedException)
                {

                }
            }
        }

    }
}