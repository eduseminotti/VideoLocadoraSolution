using Moq;
using NUnit.Framework;
using System;
using VideoLocadora.Dominio.Filmes;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.Tests
{
    public class Tests
    {
        private Mock<IFilmeRepository> _fakeFilmeRepository;
        private FilmeDomainService _filmeDomainService;



        [SetUp]
        public void Setup()
        {
            _fakeFilmeRepository = new Mock<IFilmeRepository>();
            _filmeDomainService = new FilmeDomainService(_fakeFilmeRepository.Object);

        }

        [Test]
        public void Deve_retornar_que_o_filme_esta_locado()
        {
            //preparação
            var locatario = new Locatario();
            locatario.Nome = "locatario";
            locatario.DataDeNascimento = DateTime.Now;
            locatario.EnderecoCompleto = "Endereço completo";

            var filme = new Filme("titulo", "1010", "cat");
            filme.Locado = true;

            //ação

            Action action = () => _filmeDomainService.LocarFilme(filme, locatario);

            //verificação
            Assert.Throws(typeof(FilmeLocadoException), new TestDelegate(action));           
        }
        [Test]
        public void Deve_retornar_que_o_filme_esta_disponivel()
        {
            //preparação
            var locatario = new Locatario();
            locatario.Nome = "locatario";
            locatario.DataDeNascimento = DateTime.Now;
            locatario.EnderecoCompleto = "Endereço completo";

            var filme = new Filme("titulo", "1010", "cat");
            filme.Locado = false;

            //ação

            var result = _filmeDomainService.LocarFilme(filme, locatario);

            //verificação
            Assert.IsTrue(result);
        }
    }
}