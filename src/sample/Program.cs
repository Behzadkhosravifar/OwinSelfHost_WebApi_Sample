using System.Reflection;
using Topshelf;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                var version = Assembly.GetEntryAssembly().GetName().Version.ToString(3);
                configurator.UseNLog();
                configurator.SetDisplayName($"Sample Service v{version}");
                configurator.SetDescription($"Sample Service v{version}, desc... .");
                configurator.SetServiceName("Sample");

                configurator.Service<Service>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(settings => new Service(settings, Properties.Settings.Default.Port));
                    serviceConfigurator.WhenStarted((service, hostControl) => service.Start(hostControl));
                    serviceConfigurator.WhenStopped((service, hostControl) => service.Stop(hostControl));
                });
            });

            System.Console.ReadKey();
        }
    }
}