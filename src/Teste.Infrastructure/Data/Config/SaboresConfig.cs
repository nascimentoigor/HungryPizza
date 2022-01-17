using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Teste.Domain.Entities;

namespace Teste.Infrastructure.Data.Config
{
    public class SaboresConfig : IEntityTypeConfiguration<Sabores>
    {
        public void Configure(EntityTypeBuilder<Sabores> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Sabor);
            builder.Property(p => p.Valor);
            builder.Property(p => p.QtdRestantes);
        }
    }
}
