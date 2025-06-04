namespace FCG.Api.Dominio.Interfaces.Servico
{
    public interface IBaseServico<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity BuscarPorId(int id);
        IEnumerable<TEntity> BuscarTodos();
        IEnumerable<TEntity> BuscarTodosNoTracking();
        void Atualizar(TEntity obj);
        void Remover(TEntity obj);
    }
}
