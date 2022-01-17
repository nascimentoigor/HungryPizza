using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Domain.Entities
{
    public class Pedido : Entity
    {
        public int QtdPizza { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal VlTotal { get; set; }
        public int IdCliente { get; set; }
    }
}
