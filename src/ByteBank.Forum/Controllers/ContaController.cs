using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        // GET: Registrar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                // Registramos o usuário
                var dbContext = new AplicacaoDbContext();
                var userStore = new UserStore<IdentityUser>(dbContext);
                var userManager = new UserManager<IdentityUser>(userStore);

                var usuario = new IdentityUser
                {
                    Email = modelo.Email,
                    UserName = modelo.UserName
                };
                var resultado = userManager.Create(usuario, modelo.Senha);

                return RedirectToAction("Index", "Home");
            }

            // Algo de errado aconteceu. Mostraremos novamente esta view
            // com os erros de validação.
            return View(modelo);
        }

    }
}