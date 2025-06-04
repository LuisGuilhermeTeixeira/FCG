using FCG.Api.Dominio.Interfaces.Infraestrutura;
using FCG.Api.Dominio.Interfaces.Servico;

namespace FCG.Api.Aplicação.Servicos
{
    public class BaseServico<TEntity> : IBaseServico<TEntity> where TEntity : class
    {
        private readonly IBaseRepositorio<TEntity> _repositorio;

        public BaseServico(IBaseRepositorio<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public void Add(TEntity obj)
        {
            _repositorio.Add(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _repositorio.Atualizar(obj);
        }

        public TEntity BuscarPorId(int id)
        {
            return _repositorio.BuscarPorId(id);
        }

        public IEnumerable<TEntity> BuscarTodos()
        {
            return _repositorio.BuscarTodos();
        }

        public IEnumerable<TEntity> BuscarTodosNoTracking()
        {
            return _repositorio.BuscarTodosNoTracking();
        }
        public void Remover(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
