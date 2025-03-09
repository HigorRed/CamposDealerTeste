using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamposDealer.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string NmCliente { get; set; }

        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }

        public ICollection<Venda> Vendas { get; set; }
    }
}
