using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Teste.Application.Service;
using Teste.Application.ViewModel;

namespace Teste.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoService _pedidoService;

        public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService)
        {
            _logger = logger;
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PedidoVM>> GetAll(int IdCliente)
        {
            return await _pedidoService.GetAll(IdCliente);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] PedidoVM viewModel)
        {
            var model = _pedidoService.Incluir(viewModel).Result;

            if(model != null && String.IsNullOrEmpty(model.MsgErro))
                return Ok(model);
            else
                return BadRequest(model.MsgErro);
        }
    }
}