using System;
using Nancy.Hosting.Self;
using Topshelf;
using Serilog;
using Autofac;
using AutofacSerilogIntegration;
using Abstractions;

namespace WebServisNancy
{
    public class Program
    {
        static void Main(string[] args)
        {
           // SQLiteDataAccess.OpenDb();

            var uri = new Uri("http://localhost:1234");
            var config = new HostConfiguration
            {
                RewriteLocalhost = false
            };

            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateLogger();
           // Log.ForContext<SomeClass>();

            var builder = new ContainerBuilder();
            builder.RegisterType<LogMessages>().As<ILoggerMessages>();
            builder.RegisterLogger();
            var container = builder.Build();
            LogConteiner.mylog = container.Resolve<ILoggerMessages>();

            HostFactory.Run(x =>
            {
                x.Service<NancyHost>(s =>
                {
                    s.ConstructUsing(name => new NancyHost(config, uri));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Using WebServisNancy");
                x.SetDisplayName("WebServisNancy Demo");
                x.SetServiceName("WebServisNancyDemo");
            });

        }
    }
}
