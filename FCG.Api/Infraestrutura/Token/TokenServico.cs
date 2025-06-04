using FCG.Api.Dominio.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCG.Api.Infraestrutura.Token
{
    public class TokenServico
    {
        private readonly SymmetricSecurityKey chave;
        int Expiracao = 10;

        public TokenServico()
        {
            this.chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("u7!3$9Lq2@aSdF9*1sT!B2nX#4pP&8Km"));
        }

        public string GerarToken(Usuario usuario)
        {
         
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.Nome),
                new Claim("Id", usuario.Id.ToString()),
                new Claim("email", usuario.Email)
            };

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(Expiracao), // Corrigi DateTime.Now para UtcNow aqui
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // NOVO MÉTODO para renovação
        public string RenovarToken(IEnumerable<Claim> claimsExistentes)
        {
            var claims = claimsExistentes.Where(c =>
                c.Type == "username" ||
                c.Type == "id" ||
                c.Type == "email" 
            ).ToList();

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(Expiracao),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
