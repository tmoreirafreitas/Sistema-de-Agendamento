using System.Collections.Generic;

namespace SA.Domain.Entities
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public virtual IList<Processo> Processos { get; set; }
    }
}
