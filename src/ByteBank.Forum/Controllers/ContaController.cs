using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        private UserManager<UsuarioAplicacao> _userManager;
        public UserManager<UsuarioAplicacao> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var contextoOwin = HttpContext.GetOwinContext();
                    _userManager = contextoOwin.GetUserManager<UserManager<UsuarioAplicacao>>();
                }

                return _userManager;
            }
        }

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
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await UserManager.FindByEmailAsync(modelo.Email);
                var emailJaCadastrado = usuarioExistente != null;

                if (emailJaCadastrado)
                    return RedirectToAction("Index", "Home");

                // Registramos o usuário
                var usuario = new UsuarioAplicacao
                {
                    Email = modelo.Email,
                    UserName = modelo.Email,
                    NomeCompleto = modelo.NomeCompleto
                };
                var resultado = await UserManager.CreateAsync(usuario, modelo.Senha);

                if (resultado.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    AdicionarErros(resultado);
            }

            // Algo de errado aconteceu. Mostraremos novamente esta view
            // com os erros de validação.
            return View(modelo);
        }

        private void AdicionarErros(IdentityResult resultado)
        {
            foreach (var erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro);
            }
        }
    }
}