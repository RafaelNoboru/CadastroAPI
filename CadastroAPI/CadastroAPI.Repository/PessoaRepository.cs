using System.Collections.Generic;
using System.Data;
using CadastroAPI.Models.Models;
using Dapper;

namespace CadastroApi.Repository
{
    public class PessoaRepository : RepositoryBase<Pessoa>
    {
        public PessoaRepository(IDbConnection connection) : base(connection) { }

        public override IEnumerable<Pessoa> GetAll()
        {
            string query = "SELECT * FROM Pessoa";
            return _connection.Query<Pessoa>(query);
        }

        public override Pessoa GetById(int id)
        {
            string query = "SELECT * FROM Pessoa WHERE Id = @Id";
            return _connection.QueryFirstOrDefault<Pessoa>(query, new { Id = id });
        }

        public override void Add(Pessoa pessoa)
        {
            string query = "INSERT INTO Pessoa (Nome, Telefone, CPF) VALUES (@Nome, @Telefone, @CPF)";
            _connection.Execute(query, pessoa);
        }

        public override void Update(Pessoa pessoa)
        {
            string query = "UPDATE Pessoa SET Nome = @Nome, Telefone = @Telefone, CPF = @CPF WHERE Id = @Id";
            _connection.Execute(query, pessoa);
        }

        public override void Delete(int id)
        {
            string query = "DELETE FROM Pessoa WHERE Id = @Id";
            _connection.Execute(query, new { Id = id });
        }
    }
}
