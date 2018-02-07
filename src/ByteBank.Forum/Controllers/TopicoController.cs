using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ByteBank.Forum.Controllers
{
    public class TopicoController : Controller
    {
        [Authorize]
        public ActionResult Criar()
        {
            return View();
        }
    }
}