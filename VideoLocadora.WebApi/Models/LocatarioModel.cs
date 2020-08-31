using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoLocadora.WebApi
{
    public class LocatarioModel
    {
        public String Nome{ get; set; }
        public String EnderecoCompleto { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}
