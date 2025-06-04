using FCG.Api.Dominio.Entidades;
using FCG.Api.DTOs;

namespace FCG.Api.Dominio.Interfaces.Servico
{
    public interface IJogoServico : IBaseServico<Jogo>
    {
        Task<(bool sucesso, string? erro)> AdicionarJogo(JogoDTO usuarioDTO);
        Task<IList<JogoDTO>> buscarTodosJogos();
    }
}
