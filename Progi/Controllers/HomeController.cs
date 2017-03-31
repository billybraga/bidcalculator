using Newtonsoft.Json;
using Progi.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Progi.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult Calculate(double total)
        {
            var result = BidCalculator
                .Instance
                .CalculateBidFromTotalCost(total, MvcApplication.Config ?? BidConfigModel.Default);
            return Content(
                JsonConvert.SerializeObject(
                    result == null
                    ? null
                    : result.ToRounded()),
                "application/json");
        }
    }
}