using CamposDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamposDealer.Services
{
    public sealed class ClienteService 
    {
        private readonly AppDbContext _context; 

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

    
        public async Task<List<Cliente>> ObterTodosClientes()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync(); 
        }

      
        public async Task<Cliente?> ObterClientePorId(int id)
        {
            return await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.IdCliente == id);
        }

        
        public async Task<bool> CriarCliente(Cliente cliente)
        {
            if (cliente == null || string.IsNullOrWhiteSpace(cliente.NmCliente) || string.IsNullOrWhiteSpace(cliente.Cidade))
                return false; 

            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

      
        public async Task<bool> AtualizarCliente(Cliente cliente)
        {
            if (cliente == null || !_context.Clientes.Any(c => c.IdCliente == cliente.IdCliente))
                return false;

            _context.Update(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

      
        public async Task<bool> ExcluirCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
