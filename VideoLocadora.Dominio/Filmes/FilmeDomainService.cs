using System.Collections.Generic;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.Dominio.Filmes
{
    public class FilmeDomainService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeDomainService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public bool LocarFilme(Filme filme, Locatario locatario)
        {
            filme.LocarFilme(locatario);

            _filmeRepository.AtualizarFilme(filme);

            return true;
        }

        public Filme CadastrarFilme(string titulo, string ano, string categoria)
        {
            var filme = new Filme(titulo, ano, categoria);
            _filmeRepository.SalvarFilme(filme);

            return filme;
        }

        public bool AtualizarFilme(int id, string titulo)
        {
            var filme = _filmeRepository.RetornarFilmePorId(id);

            filme.AtualizarTitulo(titulo);

            var sucess = _filmeRepository.AtualizarFilme(filme);

            return sucess != null;
        }

        public bool DeletarFilme(string tituloDoFilme)
        {
            var filmeEscolhido = _filmeRepository.RetornarPorTitulo(tituloDoFilme);

            return _filmeRepository.DeletarFilme(filmeEscolhido);
        }
        public IList<Filme> ListarFilmes()
        {
            return _filmeRepository.RetornarTodos();
        }

    }
}
