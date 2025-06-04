using FCG.Api.Dominio.Entidades;
using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;

namespace FCG.Api.Infraestrutura.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio<Usuario>, IUsuarioRepositorio
    {
        protected readonly DBContexto _context;

        public UsuarioRepositorio(DBContexto context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            try
            {
                return await _context.Usuario.AnyAsync(u => u.Email == email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            try
            {
                return await _context.Usuario
                    .Include(u => u.Jogo)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Usuario> LoginUsuário(string email, string senha)
        {
            try
            {
                return await _context.Usuario
                    .Include(u => u.Jogo)
                    .FirstOrDefaultAsync(u => u.Email == email && u.SenhaHash == senha);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
