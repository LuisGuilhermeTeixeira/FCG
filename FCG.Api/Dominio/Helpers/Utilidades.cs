using FCG.Api.Dominio.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCG.Api.Dominio.Helpers
{
    public static class Utilidades
    {
        public static bool ValidarEmail(string email) =>
        System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static bool ValidarSenha(string senha) =>
            senha.Length >= 8 &&
            senha.Any(char.IsDigit) &&
            senha.Any(char.IsLetter) &&
            senha.Any(c => !char.IsLetterOrDigit(c));


    }
}
