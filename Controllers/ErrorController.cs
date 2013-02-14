using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error404()
        {
            return View(new ErrorViewModel());
        }

        public ActionResult Error500()
        {
            return View(new ErrorViewModel());
        }

    }
}
