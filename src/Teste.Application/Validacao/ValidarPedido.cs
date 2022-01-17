using System;
using System.Collections.Generic;
using System.Text;
using Teste.Application.ViewModel;

namespace Teste.Application.Validacao
{
    public class ValidarPedido
    {
        public static ErrorMessage ValidarDados(PedidoVM obj)
        {
            var validacao = new ErrorMessage() { Valido = true};

            if (obj.QtdPizza <= 0 )
                validacao = new ErrorMessage() { Valido = false, Erro = "A quantidade de pizzas deve ser maior que 0!" };

            if (obj.QtdPizza > 10)
                validacao = new ErrorMessage() { Valido = false, Erro = "A quantidade de pizzas deve ser no máximo 10!" };

            return validacao;
        }
    }
}
