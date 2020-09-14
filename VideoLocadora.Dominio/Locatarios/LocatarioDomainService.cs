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
        public bool DeletarLocatario(Locatario locatario)
        {
            //todo implementar verificação se locatario possuir algum filme locado antes de excluir
            return _locatarioRepository.DeletarLocatario(locatario);            
        }
  
    }
}
