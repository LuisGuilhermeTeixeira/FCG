using FCG.Api.Aplicação.Servicos;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private IJogoServico _jogoservico;
        private readonly ILogger<JogosController> _logger;
        public JogosController(IJogoServico jogoServico, ILogger<JogosController> logger)
        {
            _jogoservico = jogoServico;
            _logger = logger;
        }

        [HttpPost("cadastro")]
        [Authorize(Policy = "PerfilAdmin")]

        public async Task<IActionResult> CadastrarJogo(JogoDTO dto)
        {
            try
            {
                var (sucesso, erro) = await _jogoservico.AdicionarJogo(dto);

                if (!sucesso)
                {
                    _logger.LogWarning("Erro ao cadastrar jogo: {Erro}. Titulo: {Titulo}", erro, dto.Titulo);
                    return BadRequest(new { mensagem = erro });
                }

                _logger.LogInformation("Jogo cadastrado com sucesso: {Titulo}", dto.Titulo);
                return Ok("Usuário cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao tentar cadastrar Jogo: {Titulo}", dto.Titulo);
                return StatusCode(500, "Erro interno do servidor. Tente novamente mais tarde.");
            }
        }

        [HttpGet("lista-jogos")]
        [Authorize(Policy = "PerfilUsuario")]
        public async Task<IActionResult> BuscarTodosJogos()
        {
            try
            {
               var retornolistaDto = _jogoservico.buscarTodosJogos();

                return Ok(retornolistaDto);
            }
            catch (Exception e)
            {
                return BadRequest("Erro durante a Busca");
            }
        }
    }
}
