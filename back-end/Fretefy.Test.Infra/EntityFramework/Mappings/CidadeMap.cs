using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class CidadeMap : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd(); 

            builder.Property(p => p.Nome)
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(p => p.UF)
                .HasMaxLength(2)
                .IsRequired();

            builder.HasData(
                new Cidade { Id = 1, Nome = "Rio Branco", UF = "AC" },
                new Cidade { Id = 2, Nome = "Maceió", UF = "AL" },
                new Cidade { Id = 3, Nome = "Macapá", UF = "AP" },
                new Cidade { Id = 4, Nome = "Manaus", UF = "AM" },
                new Cidade { Id = 5, Nome = "Salvador", UF = "BA" },
                new Cidade { Id = 6, Nome = "Fortaleza", UF = "CE" },
                new Cidade { Id = 7, Nome = "Brasília", UF = "DF" },
                new Cidade { Id = 8, Nome = "Vitória", UF = "ES" },
                new Cidade { Id = 9, Nome = "Goiânia", UF = "GO" },
                new Cidade { Id = 10, Nome = "São Luís", UF = "MA" },
                new Cidade { Id = 11, Nome = "Cuiabá", UF = "MT" },
                new Cidade { Id = 12, Nome = "Campo Grande", UF = "MS" },
                new Cidade { Id = 13, Nome = "Belo Horizonte", UF = "MG" },
                new Cidade { Id = 14, Nome = "Belém", UF = "PA" },
                new Cidade { Id = 15, Nome = "João Pessoa", UF = "PB" },
                new Cidade { Id = 16, Nome = "Curitiba", UF = "PR" },
                new Cidade { Id = 17, Nome = "Recife", UF = "PE" },
                new Cidade { Id = 18, Nome = "Teresina", UF = "PI" },
                new Cidade { Id = 19, Nome = "Rio de Janeiro", UF = "RJ" },
                new Cidade { Id = 20, Nome = "Natal", UF = "RN" },
                new Cidade { Id = 21, Nome = "Porto Alegre", UF = "RS" },
                new Cidade { Id = 22, Nome = "Porto Velho", UF = "RO" },
                new Cidade { Id = 23, Nome = "Boa Vista", UF = "RR" },
                new Cidade { Id = 24, Nome = "Florianópolis", UF = "SC" },
                new Cidade { Id = 25, Nome = "São Paulo", UF = "SP" },
                new Cidade { Id = 26, Nome = "Aracaju", UF = "SE" },
                new Cidade { Id = 27, Nome = "Palmas", UF = "TO" }
            );
        }
    }
}
