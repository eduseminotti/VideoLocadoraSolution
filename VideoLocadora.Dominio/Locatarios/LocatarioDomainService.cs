using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLocadora.Dominio.Locatarios
{
    public class LocatarioDomainService
    {
        private readonly ILocatarioRepository _locatarioRepository ;

        public LocatarioDomainService(ILocatarioRepository locatarioRepository)
        {
           _locatarioRepository = locatarioRepository;
            
        }

        public bool CadastrarLocatario(string nome, DateTime dataDeNascimento, string endereco)
        {
            var locatario = new Locatario();
            locatario.Nome = nome;
            locatario.DataDeNascimento = dataDeNascimento;
            locatario.EnderecoCompleto = endereco;

            return _locatarioRepository.CadastrarLocatario(locatario);
        }
        public List<Locatario> ListarLocatarios()
        {
            return _locatarioRepository.ListarLocatarios();
        }
        public bool DeletarLocatario(string nomeDoLocatario)
        {
            var locatario = _locatarioRepository.EncontrarPorNome(nomeDoLocatario);

            return _locatarioRepository.DeletarLocatario(locatario);            
        }
  
    }
}
