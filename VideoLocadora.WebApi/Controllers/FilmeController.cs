using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VideoLocadora.Dominio.Filmes;
using VideoLocadora.Dominio.Locatarios;
using VideoLocadora.WebApi.Models;

namespace VideoLocadora.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {

        private IFilmeRepository _filmeRepository;
        private FilmeDomainService _filmeDomainService;
        private ILocatarioRepository _locatarioRepository;

        public FilmeController(IFilmeRepository filmeRepository, FilmeDomainService filmeDomainService, ILocatarioRepository locatarioRepository)
        {
            _filmeRepository = filmeRepository;
            _filmeDomainService = filmeDomainService;
            _locatarioRepository = locatarioRepository;
        }

        [HttpGet]
        [Route("")]
        public IList<Filme> Get()
        {
            var filmes = _filmeDomainService.ListarFilmes();
            return filmes;
        }

        [HttpPost]
        public IActionResult Post(FilmeModel filme)
        {
            var sucesso = _filmeDomainService.CadastrarFilme(filme.Titulo, filme.Ano, filme.Categoria);

            if (sucesso == null)
                return BadRequest("Ocorreu um erro ao cadastrar o filme");
            else
                return Ok("Filme cadastrado com suceso");
        }
        [HttpPut]
        [Route("AtualizaFilme")]
        public IActionResult AtualizarFilme(AtualizaFilmeModel filme)
        {
            var filmeEncontrado = _filmeRepository.RetornarFilmePorId(filme.Id);

            if (filmeEncontrado == null)
                return NotFound("Não encontrado o filme especificado");

            var sucesso = _filmeDomainService.AtualizarFilme(filmeEncontrado.Id, filme.novoTitulo);

            if (!sucesso)
                return BadRequest("Ocorreu um erro ao atualizar o filme");
            else
                return Ok("Filme atualizado com suceso");
        }

        [HttpPut]
        [Route("LocarFilme")]
        public IActionResult LocarFilme(LocarFilmeModel locarFilme)
        {
            var filme = _filmeRepository.RetornarFilmePorId(locarFilme.FilmeId);

            if (filme == null)
                return NotFound("Não encontrado o filme especificado");
;
            var locatario = _locatarioRepository.EncontrarPorId(locarFilme.LocatarioId);

            if (locatario == null)
                return NotFound("Não encontrado o locatario especificado");

            var sucesso =  _filmeDomainService.LocarFilme(filme, locatario);

            if (!sucesso)
                return Conflict("Ocorreu um erro ao locar o filme, o filme especificado ja esta locado.");
            else
                return Ok("Filme locado com suceso");


        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _filmeRepository.RetornarFilmePorId(id);

            if (filme == null)
                return NotFound("Não encontrado o filme especificado");

            var sucesso = _filmeDomainService.DeletarFilme(filme.Titulo);

            if (!sucesso)
                return BadRequest("Ocorreu um erro ao locar o filme");
            else
                return Ok("Filme locado com suceso");
        }
    }
}
