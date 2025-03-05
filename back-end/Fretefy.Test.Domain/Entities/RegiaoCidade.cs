namespace Fretefy.Test.Domain.Entities
{
    public class RegiaoCidade
    {
        public int Id { get; set; }
        public int RegiaoId { get; set; }
        public int CidadeId { get; set; }

        public Regiao Regiao { get; set; }
        public Cidade Cidade { get; set; }
    }
}
