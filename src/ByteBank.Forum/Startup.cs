using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
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
            app.CreatePerOwinContext(() => new AplicacaoDbContext());

            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) =>
                new UserStore<IdentityUser>(
                    cont.Get<AplicacaoDbContext>())
            );

            app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, cont) =>
                new UserManager<IdentityUser>(
                    cont.Get<UserStore<IdentityUser>>())
            );
        }
    }
}