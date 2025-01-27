using System.Data;
using CadastroAPI.Models.Models;
using Dapper;

namespace CadastroApi.Repository
{
    public class EnderecoRepository : RepositoryBase<Endereco>
    {
        public EnderecoRepository(IDbConnection connection) : base(connection) { }

        public override IEnumerable<Endereco> GetAll()
        {
            string query = "SELECT * FROM Endereco";
            return _connection.Query<Endereco>(query);
        }

        public override Endereco GetById(int id)
        {
            string query = "SELECT * FROM Endereco WHERE Id = @Id";
            return _connection.QueryFirstOrDefault<Endereco>(query, new { Id = id });
        }

        public override void Add(Endereco endereco)
        {
            string query = @"
                INSERT INTO Endereco (Logradouro, Cep, Cidade, Estado, PessoaId) 
                VALUES (@Logradouro, @Cep, @Cidade, @Estado, @PessoaId)";
            _connection.Execute(query, endereco);
        }

        public override void Update(Endereco endereco)
        {
            string query = @"
                UPDATE Endereco
                SET Logradouro = @Logradouro, 
                    Cep = @Cep, 
                    Cidade = @Cidade, 
                    Estado = @Estado
                WHERE Id = @Id";
            _connection.Execute(query, endereco);
        }

        public override void Delete(int id)
        {
            string query = "DELETE FROM Endereco WHERE Id = @Id";
            _connection.Execute(query, new { Id = id });
        }
    }
}
