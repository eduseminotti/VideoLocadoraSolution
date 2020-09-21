using System;
using System.Text.Json.Serialization;
using VideoLocadora.Dominio.Base;
using VideoLocadora.Dominio.Enuns;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.Dominio.Filmes
{
    public class Filme : Entidade
    {

        public string Titulo { get; set; }


        public string Ano { get; set; }
 
        public string Categoria { get; set; }

        public FilmeLocado Locado { get; set; }

        public int ?LocatarioId { get; set; }

        public Locatario LocatarioDoFilme { get; set; }

        public Filme(string titulo, string ano, string categoria)
        {
            Titulo = titulo;
            Ano = ano;
            Categoria = categoria;
            Locado = FilmeLocado.Nao;
            LocatarioDoFilme = new Locatario();
        }

        public Filme()
        {
            LocatarioDoFilme = new Locatario();
        }

        public void AtualizarTitulo(string titulo)
        {
            if (titulo != string.Empty)
                this.Titulo = titulo;
        }

  
        public void LocarFilme(Locatario locatario)
        {
            if (Locado == FilmeLocado.Sim)
            {
                throw new FilmeLocadoException("O filme está indisponível.");
            }

            Locado = FilmeLocado.Sim;
            LocatarioId = locatario.Id;
            LocatarioDoFilme = locatario;
        }
        public void DevolverFilme()
        {
            Locado = FilmeLocado.Nao;
            LocatarioId = null;
            LocatarioDoFilme = null;
        }


        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Filme))
                return false;

            var filme = (Filme)obj;

            return filme.Titulo.Equals(this.Titulo, StringComparison.OrdinalIgnoreCase) &&
                filme.Categoria.Equals(this.Categoria, StringComparison.OrdinalIgnoreCase) &&
                filme.Ano.Equals(this.Ano, StringComparison.OrdinalIgnoreCase);
        }


    }
}
