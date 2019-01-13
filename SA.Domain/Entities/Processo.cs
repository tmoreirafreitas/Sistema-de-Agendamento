using System;

namespace SA.Domain.Entities
{
    public class Processo : Entity
    {
        public string Numero { get; set; }
        public DateTime DataCriacao { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public decimal Valor { get; set; }
        public bool IsAtivo { get; set; }
    }
}
