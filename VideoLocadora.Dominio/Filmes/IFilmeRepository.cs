using System.Collections.Generic;

namespace VideoLocadora.Dominio.Filmes
{
    public interface IFilmeRepository
    {
        bool SalvarFilme(Filme filme);
        Filme RetornarPorTitulo(string titulo);

        Filme AtualizarFilme(Filme filme);

        Filme RetornarFilmePorId(int id);

        bool DeletarFilme(Filme filme);
        IList<Filme> RetornarTodos();
    }
}
