using System;
using System.Collections.Generic;
using System.Text;
using Teste.Domain.Entities;

namespace Teste.Application.ViewModel
{
    public class PedidoVM
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataPedido { get; set; }
        public int QtdPizza { get; set; }
        public decimal VlTotal { get; set; }
        public string MsgErro { get; set; }
        public string Endereco { get; set; }
        public List<Sabores> Sabores { get; set; }

    }
}
