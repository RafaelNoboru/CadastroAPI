using System.Collections.Generic;
using System.Data;
using Dapper;

namespace CadastroApi.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected readonly IDbConnection _connection;

        public RepositoryBase(IDbConnection connection)
        {
            _connection = connection;
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException("Método GetAll precisa ser implementado pelo repositório específico.");
        }

        public virtual T GetById(int id)
        {
            throw new System.NotImplementedException("Método GetById precisa ser implementado pelo repositório específico.");
        }

        public virtual void Add(T entity)
        {
            throw new System.NotImplementedException("Método Add precisa ser implementado pelo repositório específico.");
        }

        public virtual void Update(T entity)
        {
            throw new System.NotImplementedException("Método Update precisa ser implementado pelo repositório específico.");
        }

        public virtual void Delete(int id)
        {
            throw new System.NotImplementedException("Método Delete precisa ser implementado pelo repositório específico.");
        }
    }
}
