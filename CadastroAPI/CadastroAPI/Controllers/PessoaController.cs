using System.Text.Json;
using CadastroApi.Repository;
using CadastroAPI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CadastroAPI.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Endereco> _enderecoRepository;
        private readonly ILogger<PessoaController> _logger;

        public PessoaController(IRepository<Pessoa> pessoaRepository, IRepository<Endereco> enderecoRepository, ILogger<PessoaController> logger)
        {
            _pessoaRepository = pessoaRepository;
            _enderecoRepository = enderecoRepository;
            _logger = logger;
        }

        // Exhibition 
        public IActionResult Index()
        {
            var pessoas = _pessoaRepository.GetAll();
            return View(pessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _pessoaRepository.Add(pessoa);
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var pessoa = _pessoaRepository.GetById(id);
                if (pessoa == null)
                {
                    return RedirectToAction("Index");
                }

                _pessoaRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _pessoaRepository.Update(pessoa);
                return RedirectToAction("Edit", new { id = pessoa.Id });
            }
            return View(pessoa);
        }
        public IActionResult Edit(int id)
        {
            var pessoa = _pessoaRepository.GetById(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            var enderecos = _enderecoRepository.GetAll().Where(e => e.PessoaId == id);
            ViewBag.Enderecos = enderecos;

            return View(pessoa);
        }

        [HttpPost]
        public IActionResult AddEndereco(int pessoaId, Endereco endereco)
        {
           
            if (ModelState.IsValid)
            {
             
                endereco.PessoaId = pessoaId;
                _enderecoRepository.Add(endereco);

                return RedirectToAction("Edit", new { id = pessoaId });
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError($"Erro de validação: {error.ErrorMessage}");
            }

            return RedirectToAction("Edit", new { id = pessoaId });
        }

        [HttpPost]
        public IActionResult DeleteEndereco(int id)
        {
            var endereco = _enderecoRepository.GetById(id);
            if (endereco != null)
            {
                _enderecoRepository.Delete(id);
                return RedirectToAction("Edit", new { id = endereco.PessoaId });
            }

            return NotFound();
        }

        //ViaCep

        [HttpGet]
        public async Task<IActionResult> ConsultarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return BadRequest("O CEP não pode estar vazio.");
            }

            try
            {
                string url = $"https://viacep.com.br/ws/{cep}/json/";

                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Erro ao consultar o CEP. Verifique o CEP informado.");
                }

                var conteudo = await response.Content.ReadAsStringAsync();
                var endereco = JsonSerializer.Deserialize<ViaCep>(conteudo, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (endereco == null || !string.IsNullOrWhiteSpace(endereco.Erro))
                {
                    return BadRequest("CEP inválido ou não encontrado.");
                }

                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
