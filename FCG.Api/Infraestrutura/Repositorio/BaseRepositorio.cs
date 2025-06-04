using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FCG.Api.Infraestrutura.Repositorio
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        protected readonly DBContexto _context;

        public BaseRepositorio(DBContexto context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(TEntity obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Atualizar(TEntity obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public TEntity BuscarPorId(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> BuscarTodos()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> BuscarTodosNoTracking()
        {
            return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public void Remover(TEntity obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }
    }
}
