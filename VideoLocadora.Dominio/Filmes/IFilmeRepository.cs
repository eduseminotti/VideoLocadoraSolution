using System.Collections.Generic;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.Dominio.Filmes
{
    public interface IFilmeRepository
    {
        bool CadastrarFilme(Filme filme);
        Filme RetornarPorTitulo(string titulo);

        bool AtualizarFilme(Filme filme);
        
        bool LocarOuDevolverFilme(Filme filme);

        Filme RetornarFilmePorId(int id);

        bool DeletarFilme(Filme filme);
        IList<Filme> RetornarTodos();

        List<Filme> ListaFilmesDoLocatario(int locatarioId);
    }
}
