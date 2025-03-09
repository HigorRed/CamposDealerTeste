using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CamposDealer.Models;

namespace CamposDealer.Models
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }


        [Required]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [Required]
        public int IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        public Produto Produto { get; set; }

        [Required]
        public int QtdVenda { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal VlrUnitarioVenda { get; set; }

        [Required]
        public DateTime DthVenda { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]

        public decimal VlrTotalVenda { get; set; }

    }
}



