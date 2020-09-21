using System;
using System.Collections.Generic;
using System.Text;
using VideoLocadora.Dominio.Locatarios;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace VideoLocadora.Repositorio.Sql
{
    public class LocatarioRepositorio : ILocatarioRepository
    {

        private string _connectionString;

        public LocatarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        public bool CadastrarLocatario(Locatario locatario)
        {

            SqlConnection conexao = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"insert into  dbo.Locatario ( nome, enderecocompleto , datadenascimento)" +
                    $" values ( '{locatario.Nome}', '{locatario.EnderecoCompleto}' , '{locatario.DataDeNascimento}') "
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

        public bool DeletarLocatario(Locatario locatario)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"delete from dbo.Locatario where id = {locatario.Id}"
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

        public Locatario EncontrarPorId(int id)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);
            var locatario = new Locatario();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"select id, nome,enderecocompleto,datadenascimento from dbo.Locatario where id = {id}"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();         

                while (query.Read())
                {
                    locatario.Id = int.Parse(query["id"].ToString());
                    locatario.Nome = query["nome"].ToString();
                    locatario.EnderecoCompleto = query["enderecocompleto"].ToString();
                    locatario.DataDeNascimento = DateTime.Parse(query["datadenascimento"].ToString());
                }          
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Close();
            }
            return locatario;

        }

        public Locatario EncontrarPorNome(string nome)
        {
            SqlConnection conexao = new SqlConnection(_connectionString);
            var locatario = new Locatario();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"select id, nome,enderecocompleto,datadenascimento from dbo.Locatario where nome = '{nome}'"
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                while (query.Read())
                {
                    locatario.Id = int.Parse(query["id"].ToString());
                    locatario.Nome = query["nome"].ToString();
                    locatario.EnderecoCompleto = query["enderecocompleto"].ToString();
                    locatario.DataDeNascimento = DateTime.Parse(query["datadenascimento"].ToString());
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conexao.Close();
            }
            return locatario;
        }

        public List<Locatario> ListarLocatarios()
        {
            SqlConnection conexao = new SqlConnection(_connectionString);
            var locatarios = new List<Locatario>();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conexao,
                    CommandText = $"select id, nome,enderecocompleto,datadenascimento from dbo.Locatario "
                };

                conexao.Open();

                SqlDataReader query = cmd.ExecuteReader();

                while (query.Read())
                {

                    var locatario = new Locatario();

                    locatario.Id = int.Parse(query["id"].ToString());
                    locatario.Nome = query["nome"].ToString();
                    locatario.EnderecoCompleto = query["enderecocompleto"].ToString();
                    locatario.DataDeNascimento = DateTime.Parse(query["datadenascimento"].ToString());

                    locatarios.Add(locatario);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conexao.Close();
            }
            return locatarios;

        }

        
    }
}
