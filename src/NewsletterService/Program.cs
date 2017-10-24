using System.Reflection;
using Topshelf;

namespace NewsletterService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                var version = Assembly.GetEntryAssembly().GetName().Version.ToString(3);
                configurator.UseNLog();
                configurator.SetDisplayName($"Newsletter Service v{version}");
                configurator.SetDescription($"Newsletter Service v{version}, create newsletter content and manage user and data.");
                configurator.SetServiceName("NewsletterService");

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