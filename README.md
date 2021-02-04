# ApplicationInsights.Demo

## Trace logs in Azure Application Insights - log4Net

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
