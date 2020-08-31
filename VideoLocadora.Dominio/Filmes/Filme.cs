using System;
using System.Text.Json.Serialization;
using VideoLocadora.Dominio.Base;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.Dominio.Filmes
{
    public class Filme : Entidade
    {
        /// <summary>
        /// Indica o título do filme
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Ano de lançamento
        /// </summary>
        public string Ano { get; set; }

        /// <summary>
        /// Categoria do filme
        /// </summary>
        public string Categoria { get; set; }

        /// <summary>
        /// Indica se o filme encontra-se locado
        /// </summary>
        public bool Locado { get; set; }

        /// <summary>
        /// Id do locatário
        /// </summary>
        public int LocatarioId { get; set; }

        /// <summary>
        /// Locatário que está com o filme emprestado
        /// </summary>
        public Locatario Locatario { get; set; }

        public Filme(string titulo, string ano, string categoria)
        {
            Titulo = titulo;
            Ano = ano;
            Categoria = categoria;
            Locado = false;
        }

        /// <summary>
        /// Atualiza o título do filme, caso o título novo for vazio a atualização não acontecerá.
        /// </summary>
        /// <param name="titulo">Novo título</param>
        public void AtualizarTitulo(string titulo)
        {
            if (titulo != string.Empty)
                this.Titulo = titulo;
        }

        /// <summary>
        /// Retorna um valor booleano que indica se o filme está disponível
        /// </summary>
        /// <returns></returns>
        public bool EstaDisponivel()
        {
            return Locado == false;
        }

        /// <summary>
        /// Realiza a locação do filme para um locatário.
        /// Se o filme já estiver locado, uma exceção <see cref="FilmeLocadoException"/> será lançada.
        /// </summary>
        /// <param name="locatario"></param>
        public void LocarFilme(Locatario locatario)
        {
            if (Locado)
            {
                throw new FilmeLocadoException("O filme está indisponível.");
            }

            Locado = true;
            LocatarioId = locatario.Id;
            Locatario = locatario;
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

        public override string ToString()
        {
            string situacao = Locado ? "Locado" : "Disponível";

            return $"Título: {Titulo} \nCategoria: {Categoria} \nAno de Lançamento: {Ano} \nSituação do filme: {situacao}";
        }
    }
}
