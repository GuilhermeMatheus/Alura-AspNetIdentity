using Microsoft.AspNet.Identity.EntityFramework;

namespace ByteBank.Forum.Models
{
    public class UsuarioAplicacao : IdentityUser
    {
        public virtual string NomeCompleto { get; set; }
    }
}