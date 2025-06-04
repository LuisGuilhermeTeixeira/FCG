using FCG.Api.Dominio.Entidades;
using FCG.Api.Dominio.Helpers;
using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Dominio.Interfaces.Servico;
using FCG.Api.DTOs;
using FCG.Api.Infraestrutura.Repositorio;

namespace FCG.Api.Aplicação.Servicos
{
    public class JogoServico : BaseServico<Jogo>, IJogoServico
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly ILogger<JogoServico> _logger;

        public JogoServico(IJogoRepositorio jogorepositorio, ILogger<JogoServico> logger)
            : base(jogorepositorio) // Passa o repositório para o construtor da classe base
        {
            _jogoRepositorio = jogorepositorio;
            _logger = logger;
        }

        public async Task<(bool sucesso, string? erro)> AdicionarJogo(JogoDTO jogoDTO)
        {
            if (jogoDTO is null)
                return (false, "Dados do Jogo não podem ser nulos.");

            jogoDTO.Titulo = jogoDTO.Titulo?.Trim();
            jogoDTO.Categoria = jogoDTO.Categoria?.Trim();
            jogoDTO.Descricao = jogoDTO.Descricao?.Trim();

            try
            {
                if (await _jogoRepositorio.JogoExistente(jogoDTO.Titulo))
                    return (false, "Título de jogo já está em uso.");

                var jogo = new Jogo
                {
                    Titulo = jogoDTO.Titulo,
                    Categoria = jogoDTO.Categoria,
                    Descricao = jogoDTO.Descricao,
                    Preco = jogoDTO.Preco,
                };

                _jogoRepositorio.Add(jogo);
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar o jogo com título: {titulo}", jogoDTO.Titulo);
                throw;
            }
        }

        public async Task<IList<JogoDTO>> buscarTodosJogos()
        {
            try
            {
                var listaJogos = _jogoRepositorio.BuscarTodos();
                if (listaJogos == null || !listaJogos.Any())
                {
                    _logger.LogWarning("Nenhum jogo encontrado.");
                    return new List<JogoDTO>();
                }

                var jogosDTO = listaJogos.Select(jogo => new JogoDTO
                {
                    Titulo = jogo.Titulo,
                    Categoria = jogo.Categoria,
                    Descricao = jogo.Descricao,
                    Preco = jogo.Preco
                }).ToList();

                return await Task.FromResult(jogosDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar jogos");
                throw;
            }
        }
    }
}
