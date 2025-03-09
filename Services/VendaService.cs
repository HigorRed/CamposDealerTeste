using CamposDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamposDealer.Services
{
    public sealed class VendaService 
    {
        private readonly AppDbContext _context;

        public VendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> ObterTodasVendas()
        {
            return await _context.Vendas
                .AsNoTracking()
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .ToListAsync();
        }

    
        public async Task<Venda?> ObterVendaPorId(int id)
        {
            return await _context.Vendas
                .AsNoTracking()
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .FirstOrDefaultAsync(v => v.IdVenda == id);
        }

       
        public async Task<bool> CriarVenda(Venda venda)
        {
            if (venda == null || venda.QtdVenda <= 0 || venda.VlrUnitarioVenda <= 0)
                return false; 

            venda.VlrTotalVenda = venda.QtdVenda * venda.VlrUnitarioVenda;

            _context.Add(venda);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarVenda(Venda venda)
        {
            if (venda == null || !_context.Vendas.Any(v => v.IdVenda == venda.IdVenda))
                return false;

            venda.VlrTotalVenda = venda.QtdVenda * venda.VlrUnitarioVenda;

            _context.Update(venda);
            await _context.SaveChangesAsync();
            return true;
        }

  
        public async Task<bool> ExcluirVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
                return false;

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(List<Cliente>, List<Produto>)> ObterClientesEProdutos()
        {
            var clientes = await _context.Clientes.AsNoTracking().ToListAsync();
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            return (clientes, produtos);
        }
    }
}
