using CamposDealer.Models;
using CamposDealer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CamposDealer.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService; 

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

      
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObterTodosClientes();
            return View(clientes);
        }

  
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest(); 

            var cliente = await _clienteService.ObterClientePorId(id.Value);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

      
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            

            await _clienteService.CriarCliente(cliente);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest(); 

            var cliente = await _clienteService.ObterClientePorId(id.Value);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
                return BadRequest();

         

            bool atualizado = await _clienteService.AtualizarCliente(cliente);
            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

     
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var cliente = await _clienteService.ObterClientePorId(id.Value);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool excluido = await _clienteService.ExcluirCliente(id);
            if (!excluido)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
