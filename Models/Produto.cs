using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CamposDealer.Models;

namespace CamposDealer.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required]
        [StringLength(200)]
        public string DscProduto { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal VlrUnitario { get; set; }

        public ICollection<Venda> Vendas { get; set; }
    }
}
