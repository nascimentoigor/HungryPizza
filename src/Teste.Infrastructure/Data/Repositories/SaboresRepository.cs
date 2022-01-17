using System;
using System.Collections.Generic;
using System.Text;
using Teste.Domain.Entities;
using Teste.Infrastructure.Data.Contexts;

namespace Teste.Infrastructure.Data.Repositories
{
    public class SaboresRepository : Repository<Sabores>, ISaboresRepository
    {
        public SaboresRepository(TesteContext context) : base(context) { }
    }
}
