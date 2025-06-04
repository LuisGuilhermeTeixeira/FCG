using FCG.Api.Dominio.Entidades;
using FCG.Api.DTOs;

namespace FCG.Api.Dominio.Interfaces.Servico
{
    public interface IUsuarioServico : IBaseServico<Usuario>
    {
        Task<(bool sucesso, string? erro)> CriarUsuarioAsync(UsuarioDTO usuarioDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
