using System.ComponentModel.DataAnnotations;
namespace CadastroAPI.Models.Models;

public class Endereco
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O Logradouro é obrigatório.")]
    [StringLength(200, ErrorMessage = "O Logradouro não pode ter mais de 200 caracteres.")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "A Cidade é obrigatória.")]
    [StringLength(100, ErrorMessage = "A Cidade não pode ter mais de 100 caracteres.")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O Estado é obrigatório.")]
    [StringLength(2, ErrorMessage = "O Estado deve ter apenas 2 caracteres.")]
    public string Estado { get; set; }
    public int PessoaId { get; set; }

}
