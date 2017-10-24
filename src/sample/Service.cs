using System;
using System.Net;
using System.Threading;
using Microsoft.Owin.Hosting;
using NLog;
using Topshelf;
using Topshelf.Runtime;

namespace Sample
{
    internal class Service
    {
        private readonly ILogger _logger;
        private IDisposable _webServer;
        private HostSettings _settings;
        private readonly int _port;
        private Thread _service;

        public Service(HostSettings settings, int port)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _settings = settings;
            _port = port;
        }

        public bool Start(HostControl hostControl)
        {
            // src: http://www.ascii-art-generator.org/
            var header = @"
    
      #####                                     
     #     #   ##   #    # #####  #      ###### 
     #        #  #  ##  ## #    # #      #      
      #####  #    # # ## # #    # #      #####  
           # ###### #    # #####  #      #      
     #     # #    # #    # #      #      #      
      #####  #    # #    # #      ###### ######                                                                 


            ";

            _logger.Info(header);

            try
            {
                _service = new Thread(() =>
                {
                    // Start OWIN host 
                    _webServer = WebApp.Start<Startup>(new StartOptions($"http://+:{_port}"));

                    _logger.Info($"Server running on port {_port}");
                });
                _service.Start();

                return true;
            }
            catch (Exception exp)
            {
                if (exp.InnerException is HttpListenerException)
                {
                    throw new FieldAccessException("Access to listen this port is denied, please run as administrator!", exp.InnerException);
                }

                _logger.Error(exp);
                return false; 
            }
        }

        public bool Stop(HostControl hostControl)
        {
            _webServer.Dispose();
            _service.Join();
            hostControl.Stop();
            return true;
        }
    }
}
