using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VideoLocadora.Dominio.Enuns;
using VideoLocadora.Dominio.Filmes;

namespace VideoLocadora.Repositorio.Sql
{
    public class FilmeRepositorio : IFilmeRepository
    {
        private string _connectionString;

        public FilmeRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }
        public bool AtualizarFilme(Filme filme)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"update dbo.Filme set titulo = '{filme.Titulo}' where id = {filme.Id}"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }

        public bool DeletarFilme(Filme filme)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"delete from dbo.filme where id = {filme.Id}"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }

        public Filme RetornarFilmePorId(int id)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            Filme filme = new Filme();

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"select id, titulo,ano,categoria , locado, locatarioId from dbo.filme where id = {id}"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                while (query.Read())
                {
                    filme.Id = int.Parse(query["id"].ToString());
                    filme.Titulo = query["Titulo"].ToString();
                    filme.Categoria = query["Categoria"].ToString();
                    filme.Ano = query["Ano"].ToString();
                    filme.Locado = (FilmeLocado)int.Parse(query["locado"].ToString());
                    filme.LocatarioId = int.Parse(query["LocatarioId"].ToString());

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Close();
            }
            return filme;
        }

        public Filme RetornarPorTitulo(string titulo)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            Filme filme = new Filme();

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"select id, titulo,ano,categoria , locado, locatarioId from dbo.filme where titulo = '{titulo}'"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                while (query.Read())
                {
                    filme.Id = int.Parse(query["id"].ToString());
                    filme.Titulo = query["Titulo"].ToString();
                    filme.Categoria = query["Categoria"].ToString();
                    filme.Ano = query["Ano"].ToString();
                    filme.Locado = (FilmeLocado)int.Parse(query["locado"].ToString());
                    filme.LocatarioId = int.Parse(query["LocatarioId"].ToString());

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Close();
            }
            return filme;
        }

        public IList<Filme> RetornarTodos()
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            var filmes = new List<Filme>();

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"select id, titulo,ano,categoria , locado, locatarioId from dbo.filme"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                while (query.Read())
                {
                    Filme filme = new Filme();

                    filme.Id = int.Parse(query["id"].ToString());
                    filme.Titulo = query["Titulo"].ToString();
                    filme.Categoria = query["Categoria"].ToString();
                    filme.Ano = query["Ano"].ToString();
                    filme.Locado = (FilmeLocado)int.Parse(query["locado"].ToString());
                    var locatarioid = query["LocatarioId"].ToString();

                    if (locatarioid != null && locatarioid != "")
                        filme.LocatarioId = int.Parse(locatarioid);

                    filmes.Add(filme);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Close();
            }
            return filmes;
        }

        public bool CadastrarFilme(Filme filme)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"insert into  dbo.Filme ( titulo, categoria , ano , locado)" +
                    $" values ( '{filme.Titulo}', '{filme.Categoria}' , '{filme.Ano}' , 0) "
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }

        public bool LocarOuDevolverFilme(Filme filme)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            try
            {

                var locado = filme.Locado == FilmeLocado.Sim ? 1 : 0;

                string update;
                if (filme.LocatarioId == null)
                    update = $"update dbo.Filme set locado = {locado} , locatarioId = null where id = {filme.Id}";
                else
                    update = $"update dbo.Filme set locado = {locado} , locatarioId = {filme.LocatarioId} where id = {filme.Id}";


                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = update
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
