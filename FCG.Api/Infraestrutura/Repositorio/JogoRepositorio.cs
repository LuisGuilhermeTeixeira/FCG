using FCG.Api.Dominio.Entidades;
using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;

namespace FCG.Api.Infraestrutura.Repositorio
{
    public class JogoRepositorio : BaseRepositorio<Jogo>, IJogoRepositorio
    {
        protected readonly DBContexto _context;

        public JogoRepositorio(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> JogoExistente(string titulo)
        {
            try
            {
                return await _context.Jogo.AnyAsync(u => u.Titulo == titulo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
