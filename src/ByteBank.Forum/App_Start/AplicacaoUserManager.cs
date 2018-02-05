using ByteBank.Forum.Identity;
using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ByteBank.Forum
{
    public class AplicacaoUserManager : UserManager<UsuarioAplicacao>
    {
        public AplicacaoUserManager(IUserStore<UsuarioAplicacao> userStore)
            : base(userStore)
        {
        }

        public static AplicacaoUserManager Criar(IdentityFactoryOptions<UserManager<UsuarioAplicacao>> opcoes, IOwinContext contexto)
        {
            var userStore = contexto.Get<UserStore<UsuarioAplicacao>>();
            var userManager = new AplicacaoUserManager(userStore);

            userManager.PasswordValidator = new ValidadorSenha
            {
                TamanhoRequerido = 6,
                ObrigatorioCaractereEspecial = true,
                ObrigatorioDigito = true,
                ObrigatorioLowercase = true,
                ObrigatorioUppercase = true
            };

            userManager.EmailService = new EmailServico();

            //var provider = new DpapiDataProtectionProvider("Sample");
            var dataProtectionProvider = opcoes.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider =
                    new DataProtectorTokenProvider<UsuarioAplicacao>(dataProtectionProvider.Create("ForumByteBank"));
            }
            return userManager;
        }
    }
}