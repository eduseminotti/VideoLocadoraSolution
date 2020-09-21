using System;
using System.Collections.Generic;
using System.Text;
using VideoLocadora.Dominio.Filmes;

namespace VideoLocadora.Dominio.Locatarios
{
    public class LocatarioDomainService
    {
        private readonly ILocatarioRepository _locatarioRepository;
        private readonly IFilmeRepository _filmeRepository;

        public LocatarioDomainService(ILocatarioRepository locatarioRepository, IFilmeRepository filmeRepository)
        {
            _locatarioRepository = locatarioRepository;
            _filmeRepository = filmeRepository;
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
        public List<Filme> VerificaSeOLocatarioPossuiFilmeLocado(Locatario locatario)
        {
            return _filmeRepository.ListaFilmesDoLocatario(locatario.Id);
        }

    }
}
