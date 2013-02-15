using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/

        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            return View(new ErrorViewModel());
        }

        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            return View(new ErrorViewModel());
        }

    }
}
