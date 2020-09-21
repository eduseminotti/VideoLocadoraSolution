using System.Collections.Generic;
using VideoLocadora.Dominio.Enuns;
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
            if (locatario != null)
                filme.LocarFilme(locatario);
            else
                filme.DevolverFilme();

            _filmeRepository.LocarOuDevolverFilme(filme);

            return true;
        }

        public Filme CadastrarFilme(string titulo, string ano, string categoria)
        {
            var filme = new Filme(titulo, ano, categoria);
            _filmeRepository.CadastrarFilme(filme);

            return filme;
        }

        public bool AtualizarFilme(Filme filme, string novoTitulo)
        {

            filme.AtualizarTitulo(novoTitulo);

            var sucess = _filmeRepository.AtualizarFilme(filme);

            return sucess != null;
        }

        public bool DeletarFilme(Filme filme)
        {
            return _filmeRepository.DeletarFilme(filme);
        }
        public IList<Filme> ListarFilmes()
        {
            return _filmeRepository.RetornarTodos();
        }

        public bool VerificaSeOFilmeEstaLocado(Filme filme)
        {
            if (filme.Locado == FilmeLocado.Sim)
            {
                return true;
            }
            return false;
        }

    }
}
