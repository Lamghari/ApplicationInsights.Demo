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

```c#
log4net.Config.XmlConfigurator.Configure();
```

```c#
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

```c#
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

```xml
	<InstrumentationKey>instrumentation-key</InstrumentationKey>
</ApplicationInsights>
```

- Manage NuGet Packages > Install the latest stable release `Microsoft.ApplicationInsights.Log4NetAppender`
The NuGet package installs the necessary assemblies and modifies web.config.

- Run your application. As you navigate the pages on the site telemetry will be sent to Application Insights.


## Trace logs using Azure Application Insights trace API

### Code snippet

```c#
Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration configuration = Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.CreateDefault();
var telemetryClient = new TelemetryClient(configuration);
telemetryClient.TrackTrace("Login - Critical issue",
                            Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Critical,
                            new Dictionary<string, string> {
                                { "username", "0610691306" },
                                { "tier", "TR23435023403408" },
                                { "channel", "M"}
                            });
```

## Search in Azure portal

Steps to add

## Search in Visual Studio

steps to add

## Create work item

### Create PAT : Personnal Access Token

- Sign in to your organization in Azure DevOps

- From your home page, open your user settings, and then select Personal access tokens.

- Select + New Token.

- Name your token, select the organization where you want to use the token, and then choose a lifespan for your token.

- When you're done, make sure to copy the token. For your security, it won't be shown again. Use this token as your password.

### Code snippet

```c#
string token = "PAT - Personnal Access Token";
string url = "https://dev.azure.com/" + {organization} + "/" + {project} + "/_apis/wit/workitems/$" + {type} + "?api-version=6.0";

List<Object> body = new List<Object>
{
    new { op = "add", path = "/fields/System.Title", value = "Connection Error" },
    new { op = "add", path = "/fields/System.AssignedTo", value = "Mohamed Lamghari" },
    new { op = "add", path = "/fields/Microsoft.VSTS.Common.Priority", value = "1" },
    new { op = "add", path = "/fields/Microsoft.VSTS.Common.Severity", value = "2 - High" }
};                

string json = JsonConvert.SerializeObject(body);

HttpClientHandler httpClientHandler = new HttpClientHandler();

using (HttpClient client = new HttpClient(httpClientHandler))
{
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
        System.Text.ASCIIEncoding.ASCII.GetBytes(":"+ token)));

    var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json-patch+json")
    };

    HttpResponseMessage responseMessage = client.SendAsync(request).Result;
}
```

