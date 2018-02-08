using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(ByteBank.Forum.Startup))]
namespace ByteBank.Forum
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<AplicacaoDbContext>
                (() => new AplicacaoDbContext());

            app.CreatePerOwinContext<UserStore<UsuarioAplicacao>>((opt, cont) =>
                new UserStore<UsuarioAplicacao>(
                    cont.Get<AplicacaoDbContext>())
            );

            app.CreatePerOwinContext<UserManager<UsuarioAplicacao>>(
                AplicacaoUserManager.Criar);

            app.CreatePerOwinContext<SignInManager<UsuarioAplicacao, string>>(
                (opt, cont) =>
                {
                    var userManager = cont.Get<UserManager<UsuarioAplicacao>>();
                    return new SignInManager<UsuarioAplicacao, string>(userManager, cont.Authentication);
                });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}