using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VideoLocadora.Dominio;
using VideoLocadora.Dominio.Locatarios;
using VideoLocadora.Repositorio.Settings;

namespace VideoLocadora.Repositorio
{
    public class LocatarioRepositorio : Repositorio<Locatario>, ILocatarioRepository
    {
        
        public LocatarioRepositorio(IOptions<DataSettings> caminhoDados) 
            : base(caminhoDados.Value.PathToData , "locatarios") 
        {
           
        }

        public bool CadastrarLocatario(Locatario locatario)
        {         
            var id = RetornarUltimoId() +1;

            locatario.Id = id;

            return Salvar(locatario);
        }

        public bool DeletarLocatario(Locatario locatario)
        {
            return Deletar(locatario);
        }

        public List<Locatario> ListarLocatarios()
        {
            var locatarios = Retornar();
            return locatarios;
        }

        public Locatario EncontrarPorNome(string nome)
        {
            var locatario = Retornar();
            return locatario.FirstOrDefault(y => y.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public Locatario EncontrarPorId(int id)
        {
            return RetornarPorId(id);           
        }

    }
}
