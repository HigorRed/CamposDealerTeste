using CamposDealer.Models;
using CamposDealer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CamposDealer.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService; 

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.ObterTodosProdutos();
            return View(produtos);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest(); 

            var produto = await _produtoService.ObterProdutoPorId(id.Value);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

     
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            

            await _produtoService.CriarProduto(produto);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest(); 

            var produto = await _produtoService.ObterProdutoPorId(id.Value);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.IdProduto)
                return BadRequest();


            bool atualizado = await _produtoService.AtualizarProduto(produto);
            if (!atualizado)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var produto = await _produtoService.ObterProdutoPorId(id.Value);
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool excluido = await _produtoService.ExcluirProduto(id);
            if (!excluido)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
