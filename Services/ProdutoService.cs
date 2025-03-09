using CamposDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamposDealer.Services
{
    public sealed class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Produto>> ObterTodosProdutos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }


        public async Task<Produto?> ObterProdutoPorId(int id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.IdProduto == id);
        }


        public async Task<bool> CriarProduto(Produto produto)
        {
            if (produto == null || string.IsNullOrWhiteSpace(produto.DscProduto) || produto.VlrUnitario <= 0)
                return false;

            _context.Add(produto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarProduto(Produto produto)
        {
            if (produto == null || !_context.Produtos.Any(p => p.IdProduto == produto.IdProduto))
                return false;

            _context.Update(produto);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ExcluirProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
