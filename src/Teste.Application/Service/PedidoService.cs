using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Application.ViewModel;
using Teste.Domain.Entities;
using Teste.Infrastructure.Data.Repositories;

namespace Teste.Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ISaboresRepository _saboresRepository;
        private readonly ILogger<IPedidoService> _logger;
        private readonly IMapper _mapper;

        public PedidoService(ILogger<IPedidoService> logger, IMapper mapper, IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, ISaboresRepository saboresRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _saboresRepository = saboresRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoVM>> GetAll(int IdCliente)
        {
            return _mapper.Map<IEnumerable<PedidoVM>>(_pedidoRepository.AsQueryable().Where(p => p.IdCliente == IdCliente));
        }

        public async Task<Cliente> RetornaClientePorId(int id)
        {
            var retorno = _clienteRepository.AsQueryable().Where(p => p.Id == id).FirstOrDefault();
            return _mapper.Map<Cliente>(retorno);
        }

        public async Task<decimal> CalculaSabores(List<Sabores> sabores)
        {
            var retorno = new Sabores();
            List<decimal> valor = new List<decimal>();
            decimal valorTotal = new decimal();
            decimal valorTotalpedido = new decimal();
            for (int i = 0; i < sabores.Count; i++)
            {
                var sabor = sabores[i].Sabor.Split('/');
                for(int j = 0; j < sabor.Length; j++)
                {
                    retorno = _saboresRepository.AsQueryable().Where(p => p.Sabor == sabor[j]).FirstOrDefault();
                    if(retorno.QtdRestantes <= 0)
                    {
                        return valorTotalpedido = 0;
                    }
                    else
                    {
                        valor.Add(retorno.Valor);
                        var objAlt = _mapper.Map<Sabores>(retorno);
                        objAlt.QtdRestantes--;
                        await _saboresRepository.Update(objAlt);
                        await _saboresRepository.Commit();
                    }
                }
                valorTotal = valor.Average();
                valorTotalpedido += valorTotal;
                valor.Clear();
            }
            return valorTotalpedido;
        }

        public async Task<PedidoVM> Incluir(PedidoVM obj)
        {
            var objRetorno = new PedidoVM();
            var cliente = new Cliente();
            
            var validacaoDados = Validacao.ValidarPedido.ValidarDados(obj);

            if (!validacaoDados.Valido)
                objRetorno.MsgErro = "Erro ao incluir o pedido! " + validacaoDados.Erro;
            else
            {
                if (String.IsNullOrEmpty(obj.Endereco))
                {
                    cliente = await RetornaClientePorId(obj.IdCliente);
                    obj.Endereco = cliente.Endereco;
                }
                else
                {
                    obj.IdCliente = 0;
                }
                obj.VlTotal = await CalculaSabores(obj.Sabores);
                if(obj.VlTotal == 0)
                {
                    objRetorno.MsgErro = "Erro ao incluir o pedido! Sabor em falta";
                    return objRetorno;
                }
                try
                {
                    var objInc = _mapper.Map<Pedido>(obj);
                    var testeInc = await _pedidoRepository.Insert(objInc);
                    objRetorno = _mapper.Map<PedidoVM>(objInc);
                }
                catch (Exception ex)
                {
                    objRetorno.MsgErro = "Erro ao incluir dados! " + ex.Message;
                }
            }
            
            return objRetorno;
        }
    }
}
