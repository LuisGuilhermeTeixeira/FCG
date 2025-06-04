using FCG.Api.Aplicação.Servicos;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.DTOs;
using FCG.Api.Infraestrutura.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioServico _usuarioServico;
        private readonly TokenServico _tokenServico;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUsuarioServico cadastroServico, TokenServico tokenServico, ILogger<LoginController> logger)
        {
            _usuarioServico = cadastroServico;
            _tokenServico = tokenServico;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO dto)
        {
            try
            {
                var token = await _usuarioServico.LoginAsync(dto);

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar cadastrar usuário: {Email}", dto.Email);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("obter-cookie")]
        [Authorize]
        public IActionResult ObterClaims()
        {
            var claims = User.Claims
                .Where(c => c.Type == "username" || c.Type == "id" || c.Type == "email")
                .Select(c => new
                {
                    Tipo = c.Type,
                    Valor = c.Value
                }).ToList();

            // Buscar a claim de expiração (exp)
            var expClaim = User.Claims.FirstOrDefault(c => c.Type == "exp");

            if (expClaim != null)
            {
                var expTimestamp = long.Parse(expClaim.Value);

                // Converter o timestamp para DateTime
                var expUtc = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;

                // Ajustar para horário de Brasília
                var fusoBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var expBrasil = TimeZoneInfo.ConvertTimeFromUtc(expUtc, fusoBrasil);

                claims.Add(new
                {
                    Tipo = "expiracao",
                    Valor = expBrasil.ToString("yyyy-MM-ddTHH:mm:ss")
                });
            }

            return Ok(claims);
        }

    }
}
