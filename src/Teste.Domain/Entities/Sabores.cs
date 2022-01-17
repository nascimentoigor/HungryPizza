using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Domain.Entities
{
    public class Sabores : Entity
    {
        public string Sabor { get; set; }
        public decimal Valor { get; set; }
        public int QtdRestantes { get; set; }
    }
}
