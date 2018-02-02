using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ByteBank.Forum.Models
{
    public class AplicacaoDbContext : IdentityDbContext<IdentityUser>
    {
        public AplicacaoDbContext()
            : this("DefaultConnection")
        {

        }

        public AplicacaoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}