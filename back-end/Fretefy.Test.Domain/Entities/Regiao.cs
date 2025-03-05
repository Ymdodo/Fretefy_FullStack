using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;

        public List<RegiaoCidade> Cidades { get; set; }
    }
}
