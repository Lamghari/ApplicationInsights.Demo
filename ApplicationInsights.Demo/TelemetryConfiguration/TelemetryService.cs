using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;

namespace ApplicationInsights.Demo.TelemetryConfiguration
{
    public class TelemetryService
    {
        public void simpleTelemetry()
        {
            Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration configuration = Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.CreateDefault();
            var telemetryClient = new TelemetryClient(configuration);
            telemetryClient.TrackTrace("CashIn - Error");
        }

        public void advancedTelemetry()
        {
            Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration configuration = Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration.CreateDefault();
            var telemetryClient = new TelemetryClient(configuration);
            telemetryClient.TrackTrace("Login - Critical issue",
                                        Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Critical,
                                        new Dictionary<string, string> {
                                            { "username", "0610691306" },
                                            { "tier", "TR23435023403408" },
                                            { "channel", "M"}
                                        });
        }

    }
}