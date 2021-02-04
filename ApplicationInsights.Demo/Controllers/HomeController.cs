using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationInsights.Demo.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            log.Info("[INDEX] Message object with the log4net.Core.Level.Info Info");

            return View();
        }

        public ActionResult About()
        {
            log.Debug("[ABOUT] Message object with the log4net.Core.Level.Debug Debug");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            log.Error("[CONTACT] Message object with the log4net.Core.Level.Error Error");

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}