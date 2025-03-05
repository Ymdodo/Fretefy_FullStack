using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretefy.Test.Domain.Entities
{
    public class Cidade : IEntity<int>
    {
        public Cidade()
        {
        }

        public Cidade(string nome, string uf)
        {
            Nome = nome;
            UF = uf;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string UF { get; set; }
    }
}
