using System;

namespace SA.Service.ViewModels
{
    public class ProcessoViewModel
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Numero { get; set; }
        public DateTime DataCriacao { get; set; }
        public ClienteViewModel Cliente { get; set; }        
        public decimal Valor { get; set; }
        public bool IsAtivo { get; set; }
    }
}