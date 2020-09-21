using System;
using VideoLocadora.Dominio.Base;

namespace VideoLocadora.Dominio.Locatarios
{
    public class Locatario : Entidade
    {
        public string Nome { get; set; }
        public string EnderecoCompleto { get; set; }
        public DateTime? DataDeNascimento { get; set; }

        public Locatario()
        {
            
        }



        public override bool Equals(object obj)
        {
            var locatario = (Locatario)obj;

            return this.Nome == locatario.Nome;
        }
    }
}