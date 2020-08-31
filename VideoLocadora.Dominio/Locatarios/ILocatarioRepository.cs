using System;
using System.Collections.Generic;
using System.Text;

namespace VideoLocadora.Dominio.Locatarios
{
    public interface ILocatarioRepository
    {
        bool CadastrarLocatario(Locatario locatario);

        List<Locatario> ListarLocatarios();

        bool DeletarLocatario(Locatario locatario);

        Locatario EncontrarPorNome(string nome);

        Locatario EncontrarPorId(int id);
    }
}
