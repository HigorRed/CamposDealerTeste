using CamposDealer.Models;
using CamposDealer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace CamposDealer.Controllers
{
    public class VendaController : Controller
    {
        private readonly VendaService _vendaService; 

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        public async Task<IActionResult> Index()
        {
            var vendas = await _vendaService.ObterTodasVendas();
            return View(vendas);
        }

     
        public async Task<IActionResult> Create()
        {
            var (clientes, produtos) = await _vendaService.ObterClientesEProdutos();

            if (!clientes.Any() || !produtos.Any())
            {
                TempData["ErrorMessage"] = "É necessário cadastrar pelo menos um Cliente e um Produto antes de criar uma Venda.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(clientes, "IdCliente", "NmCliente");
            ViewBag.Produtos = new SelectList(produtos, "IdProduto", "DscProduto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venda venda)
        {

            await _vendaService.CriarVenda(venda);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest(); 

            var venda = await _vendaService.ObterVendaPorId(id.Value);
            if (venda == null)
                return NotFound();

            var (clientes, produtos) = await _vendaService.ObterClientesEProdutos();
            ViewBag.Clientes = new SelectList(clientes, "IdCliente", "NmCliente", venda.IdCliente);
            ViewBag.Produtos = new SelectList(produtos, "IdProduto", "DscProduto", venda.IdProduto);
            return View(venda);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venda venda)
        {
            if (id != venda.IdVenda)
                return BadRequest();

            bool atualizado = await _vendaService.AtualizarVenda(venda);
            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var venda = await _vendaService.ObterVendaPorId(id.Value);
            if (venda == null)
                return NotFound();

            return View(venda);
        }

    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool excluido = await _vendaService.ExcluirVenda(id);
            if (!excluido)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
