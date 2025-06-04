using FCG.Api.Dominio.Entidades;
using System.Diagnostics;

namespace FCG.Api.Dominio.Interfaces.Infraestrutura
{
    public interface IUsuarioRepositorio : IBaseRepositorio<Usuario>
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<bool> EmailExisteAsync(string email);

        Task<Usuario> LoginUsuário(string email, string senha);
    }
}
