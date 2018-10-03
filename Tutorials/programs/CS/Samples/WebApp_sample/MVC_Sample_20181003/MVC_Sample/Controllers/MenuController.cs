using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Touryo.Infrastructure.Business.Presentation;

namespace MVC_Sample.Controllers
{
    public class MenuController : MyBaseMVController
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        // GET: Menu/GotoList
        public ActionResult GotoList()
        {
            return RedirectToAction("Index", "Order");
        }
    }
}