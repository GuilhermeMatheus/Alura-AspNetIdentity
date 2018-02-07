using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ByteBank.Forum.ViewModels
{
    public class TopicoCriarViewModel
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Conteudo { get; set; }
    }
}