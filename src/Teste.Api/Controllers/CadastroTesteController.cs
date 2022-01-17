using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Teste.Application.Service;
using Teste.Application.ViewModel;

namespace Teste.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroTesteController : ControllerBase
    {
        private readonly ILogger<CadastroTesteController> _logger;
        private readonly ICadastroTesteService _cadastroTesteService;

        public CadastroTesteController(ILogger<CadastroTesteController> logger, ICadastroTesteService cadastroTesteService)
        {
            _logger = logger;
            _cadastroTesteService = cadastroTesteService;
        }

        [HttpGet]
        public async Task<IEnumerable<CadastroTesteVM>> GetAll()
        {
            return await _cadastroTesteService.GetAll();
        }
    }
}
