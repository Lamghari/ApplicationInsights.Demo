# ApplicationInsights.Demo

## Trace logs in Azure Application Insights - log4Net

### Configure Log4Net

- Install log4net NuGet package.

- Add a log4net section inside the tag configSections of your web.config

```xml
<configSections>
	<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
</configSections>
```

- In the same web.config add the configuration for the log inside the tag log4net

```xml
<log4net>
  <appender name="RollingFile" type="log4net.Appender.RollingFileAppender">
    <file value="C:\Temp\ApplicationInsights.Demo.log" />
    <appendToFile value="true" />
    <maximumFileSize value="500KB" />
    <maxSizeRollBackups value="2" />
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %level %logger - %message%newline" />
    </layout>
  </appender>
  <root>
    <level value="All" />
    <appender-ref ref="RollingFile" />
    <appender-ref ref="aiAppender" />
  </root>
  <appender name="aiAppender" type="Microsoft.ApplicationInsights.Log4NetAppender.ApplicationInsightsAppender, Microsoft.ApplicationInsights.Log4NetAppender">
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%message%newline" />
    </layout>
  </appender>
</log4net>
```

- In the Application_Start() method of the global.asax file add this call, so that log4net reads the configuration in the web.config file

```xml
log4net.Config.XmlConfigurator.Configure();
```

```xml
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);
    log4net.Config.XmlConfigurator.Configure();
}
```

- Define a logger variable and add logs

```xml
public class HomeController : Controller
    {
        private static readonly ILog log =  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	
        public ActionResult Index()
        {
            log.Info("[INDEX] Message object with the log4net.Core.Level.Info Info");

            return View();
        }
    }
```

### Configure Application Insights to collect logs

#### Add Application Insights automatically

- Select Add Application Insights Telemetry > Application Insights Sdk (local) > Next > Finish > Close.

![](https://raw.githubusercontent.com/Lamghari/ApplicationInsights.Demo/main/ApplicationInsights.Demo/Content/images/appInsightsSteps.png)
> Steps to follow : Select Add Application Insights Telemetry > Application Insights Sdk (local) > Next > Finish > Close.

- Open the ApplicationInsights.config file.

- Before the closing </ApplicationInsights> tag add a line containing the instrumentation key for your Application Insights resource.

```html
	<InstrumentationKey>instrumentation-key</InstrumentationKey>
</ApplicationInsights>
```

- Manage NuGet Packages > Install the latest stable release `Microsoft.ApplicationInsights.Log4NetAppender`
The NuGet package installs the necessary assemblies and modifies web.config.

- Run your application. As you navigate the pages on the site telemetry will be sent to Application Insights.


## Trace logs using Azure Application Insights trace API

Steps to add
