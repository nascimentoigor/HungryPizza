using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.Application.ViewModel;

namespace Teste.Application.Service
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoVM>> GetAll(int IdCliente);
        Task<PedidoVM> Incluir(PedidoVM obj);
    }

}
