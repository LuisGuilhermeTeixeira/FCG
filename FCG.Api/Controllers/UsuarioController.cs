using FCG.Api.Aplicação.Servicos;
using FCG.Api.Dominio.Entidades;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioServico _usuarioServico;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(IUsuarioServico usuarioServico, ILogger<UsuarioController> logger)
        {
            _usuarioServico = usuarioServico;
            _logger = logger;
        }

        [HttpPost("cadastro")]
        [Authorize(Policy = "PerfilAdmin")]

        public async Task<IActionResult> CadastrarUsuario(UsuarioDTO dto)
        {
            try
            {
                var (sucesso, erro) = await _usuarioServico.CriarUsuarioAsync(dto);

                if (!sucesso)
                {
                    _logger.LogWarning("Erro ao cadastrar usuário: {Erro}. Email: {Email}", erro, dto.Email);
                    return BadRequest(new { mensagem = erro });
                }

                _logger.LogInformation("Usuário cadastrado com sucesso: {Email}", dto.Email);
                return Ok("Usuário cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar cadastrar usuário: {Email}", dto.Email);
                return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
            }
        }


    }
}
