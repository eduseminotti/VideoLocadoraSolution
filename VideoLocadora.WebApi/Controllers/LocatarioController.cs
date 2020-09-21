using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoLocadora.Dominio.Filmes;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatarioController : ControllerBase
    {
        private ILocatarioRepository _locatarioRepository;
        private LocatarioDomainService _locatarioDomainService;

        public LocatarioController(ILocatarioRepository locatarioRepository, LocatarioDomainService locatarioDomainService)
        {
            _locatarioRepository = locatarioRepository;
            _locatarioDomainService = locatarioDomainService;
        }

        [HttpGet]
        [Route("")]
        public List<Locatario> Get()
        {
            var locatario = _locatarioDomainService.ListarLocatarios();
            return locatario;
        }
        [HttpGet]
        [Route("{Id}")]
        public Locatario GetById(int d)
        {
            var locatario = new Locatario();
            return locatario;

        }

        [HttpPost]
        public IActionResult Post(LocatarioModel locatarioModel)
        {

            var sucesso = _locatarioDomainService.CadastrarLocatario(locatarioModel.Nome, locatarioModel.DataDeNascimento, locatarioModel.EnderecoCompleto);

            if (!sucesso)
                return BadRequest("Ocorreu um erro ao cadastrar o locatario");
            else
                return Created("/Locatario/{locatario.id}", "Locatario cadastrado com Sucesso.");

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Del(int id)
        {
            var locatario = _locatarioRepository.EncontrarPorId(id);

            if (locatario == null)
                return NotFound("Não encontrado o locatario especificado");

            List<Filme> filmes = _locatarioDomainService.VerificaSeOLocatarioPossuiFilmeLocado(locatario);

            if(filmes.Count != 0 )
            {
                string retorno = $"Não é possivel deletar o Locatario, o locatario possui o(s) seguinte(s) filme(s) locado(s): ";
                
                foreach (var filme in filmes)
                {

                    retorno = retorno + " Titulo: " + filme.Titulo + "; " ;
                }
                                
                return BadRequest(retorno);
            }


            var sucesso = _locatarioDomainService.DeletarLocatario(locatario);

            if (!sucesso)
                return BadRequest("Ocorreu um erro ao deletar o locatario");
            else
                return Ok("locatario deletado com suceso");

        }

    }
}
