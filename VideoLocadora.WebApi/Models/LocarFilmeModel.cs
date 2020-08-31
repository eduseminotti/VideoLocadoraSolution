using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoLocadora.WebApi.Models
{
    public class LocarFilmeModel
    {
        public int LocatarioId { get; set; }
        public int FilmeId { get; set; }
    }
}
