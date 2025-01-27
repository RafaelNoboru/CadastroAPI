namespace CadastroAPI.Models.Models
{
    public class Pessoa
    {
        public int Id { get; set; } 
        public string Nome { get; set; } 
        public string Telefone { get; set; } 
        public string Cpf { get; set; }
        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    
    }
}
