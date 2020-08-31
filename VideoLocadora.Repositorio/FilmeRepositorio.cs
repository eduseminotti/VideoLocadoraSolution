using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VideoLocadora.Dominio;
using VideoLocadora.Dominio.Filmes;
using VideoLocadora.Repositorio.Settings;

namespace VideoLocadora.Repositorio
{
    public class FilmeRepositorio : Repositorio<Filme>, IFilmeRepository
    {
        public FilmeRepositorio(IOptions<DataSettings> caminhoDados)
            : base(caminhoDados.Value.PathToData, "filmes")
        {
            
        }

        public Filme AtualizarFilme(Filme filme)
        {
            var filmeEncontrado = RetornarPorId(filme.Id);

            Deletar(filmeEncontrado);

            if (!Salvar(filme))
                throw new InvalidOperationException("Ocorreu uma falha ao tentar salvar o filme");

            return filme;
        }

        public bool DeletarFilme(Filme filme)
        {
            return Deletar(filme);
        }

        public Filme RetornarFilmePorId(int id)
        {
            return RetornarPorId(id);
        }

        public Filme RetornarPorTitulo(string titulo)
        {
            var filmes = Retornar();
            return filmes.FirstOrDefault(y => y.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }

        public IList<Filme> RetornarTodos()
        {
            return Retornar();
        }

        public bool SalvarFilme(Filme filme)
        {
            var id = RetornarUltimoId() +1;
            filme.Id = id;
            return Salvar(filme);
        }
    }
}
