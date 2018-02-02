using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ByteBank.Forum.Identity
{
    public class ValidadorSenha : IIdentityValidator<string>
    {
        public int TamanhoRequerido { get; set; }
        public bool ObrigatorioCaractereEspecial { get; set; }
        public bool ObrigatorioLowercase { get; set; }
        public bool ObrigatorioUppercase { get; set; }
        public bool ObrigatorioDigito { get; set; }

        public Task<IdentityResult> ValidateAsync(string item)
        {
            var erros = new List<string>();

            if (!VerificaTamanhoRequerido(item))
                erros.Add($"A senha deve conter no mínimo {TamanhoRequerido} caracteres.");

            if (ObrigatorioCaractereEspecial && !VerificaCaractereEspecial(item))
                erros.Add("A senha deve conter no mínimo um caractere diferente de letra ou dígito.");

            if (ObrigatorioLowercase && !VerificaLowercase(item))
                erros.Add($"A senha deve conter no mínimo uma letra minúscula.");

            if (ObrigatorioUppercase && !VerificaUppercase(item))
                erros.Add($"A senha deve conter no mínimo uma letra maiúscula.");

            if (ObrigatorioDigito && !VerificaDigito(item))
                erros.Add($"A senha deve conter no mínimo um dígito.");

            if (erros.Any())
                return Task.FromResult(IdentityResult.Failed(erros.ToArray()));

            return Task.FromResult(IdentityResult.Success);
        }

        private bool VerificaTamanhoRequerido(string senha) =>
            senha?.Length >= TamanhoRequerido;

        private bool VerificaCaractereEspecial(string senha) =>
            Regex.IsMatch(senha, @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]");

        private bool VerificaLowercase(string senha) =>
            senha.Any(c => char.IsLower(c));

        private bool VerificaUppercase(string senha) =>
            senha.Any(c => char.IsUpper(c));

        private bool VerificaDigito(string senha) =>
            senha.Any(c => char.IsDigit(c));
    }
}