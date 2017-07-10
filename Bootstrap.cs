using System;
using System.Diagnostics;
using System.Linq;
using Autofac;
using log4net;
using System.Reflection;
using System.IO;

public interface IBootstrap {
    /// <summary> Run tasks </summary>
    void Run();
}

public class Bootstrap : IBootstrap {

    public ILifetimeScope container {get; set;}
    public ILog log {get; set;}

    public void Run() {
        try {

            //InitAutofac
            if (this.container==null) {
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyModules();
                //builder.RegisterType<Bootstrap>().AsSelf().PropertiesAutowired();
                builder.RegisterInstance<Bootstrap>(this).AsSelf().SingleInstance();
                this.container = builder.Build();
                //container.InjectProperties
                var nextBootstrap = this.container.Resolve<Bootstrap>();
                nextBootstrap.Run();
                return;
            }

            //InitLogging
            if (this.log == null) {
                //log4net.Config.XmlConfigurator.Configure();
                var logRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
                log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
                var scope = this.container.BeginLifetimeScope(c => c.RegisterModule(new LogInjectionModule<log4net.ILog>(log4net.LogManager.GetLogger)));
                var nextBootstrap = this.container.Resolve<Bootstrap>();
                nextBootstrap.Run();
                return;
            }
            
            //InitRemotingFromConfig
            //RemotingClientSettings configData = ConfigurationHelper.GetSection<RemotingClientSettings>();
            //var coreXHost = configData?.CoreXHosts?.Cast<CoreXHostConfigurationElement>().FirstOrDefault();
            //RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);


            //InitServiceSettings
            //ServiceSettings.LoadSettings();     
        }
        catch(Exception ex) {
            log?.Error(ex);
        }
    }


}
