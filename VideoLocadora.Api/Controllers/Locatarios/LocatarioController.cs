using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoLocadora.Dominio.Locatarios;

namespace VideoLocadora.Api.Controllers.Locatarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatarioController : ControllerBase
    {
        private LocatarioDomainService _locatarioDomain;
        private ILocatarioRepository _locatarioRepository;

        public LocatarioController(ILocatarioRepository locatarioRepository , LocatarioDomainService locatarioDomainService)
        {
            _locatarioDomain = locatarioDomainService;
            _locatarioRepository = locatarioRepository;
        }
        [HttpGet]
        public IEnumerable<Locatario> GetAll()
        {
            var locatarios = _locatarioDomain.ListarLocatarios();

            return locatarios;
        }
    }
}
