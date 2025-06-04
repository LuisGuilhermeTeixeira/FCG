using FCG.Api.Dominio.Entidades;

namespace FCG.Api.Dominio.Interfaces.Infraestrutura
{
    public interface IJogoRepositorio : IBaseRepositorio<Jogo>
    {
        Task<bool> JogoExistente(string titulo);
    }
}
