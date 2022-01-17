using System;
using System.Collections.Generic;
using System.Text;
using Teste.Domain.Entities;
using Teste.Infrastructure.Data.Contexts;

namespace Teste.Infrastructure.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(TesteContext context) : base(context) { }
    }
}
