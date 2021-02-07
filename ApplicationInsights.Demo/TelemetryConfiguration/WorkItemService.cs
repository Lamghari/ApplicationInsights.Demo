using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ApplicationInsights.Demo.TelemetryConfiguration
{
    public class WorkItemService
    {
        public void CreateWorkItem()
        {
            string organization = "lab-lamghari";
            string project = "lab-applicationInsights";
            string type = "Issue";

            string validateOnly = "";
            string bypassRules = "";
            string suppressNotifications = "";
            string expand = "";

            string body_op = "add";
            string body_path = "/fields/System.Title";
            string body_from = null;
            string body_value = "Authorization Errors";

            string token = "yhxkmy2twndxcak6wf5ps2gta3umuukspqyijyociegstoelwdna";
            string url = "https://dev.azure.com/" + organization + "/" + project + "/_apis/wit/workitems/$" + type + "?api-version=6.0";

            try
            {
                List<Object> body = new List<Object>
                {
                    new { op = body_op, path = body_path, value = body_value },
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
            }
            catch (Exception ex)
            {

            }
        }
    }
}