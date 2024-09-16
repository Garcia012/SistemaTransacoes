using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UmblerCielo.Server.Services;
using UmblerCielo.Server.Models;

namespace UmblerCielo.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly CieloService _cieloService;

        public TransactionController(CieloService cieloService)
        {
            _cieloService = cieloService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTransaction([FromBody] CieloRequestModel request)
        {
        
            
            var response = await _cieloService.CriarTransacao(request);
            return Ok(response);
            
        }
        [HttpPost("capture/{transactionId}")]
        public async Task<IActionResult> CaptureTransaction(string transactionId, [FromBody] double amount)
        {
            var response = await _cieloService.CapturarTransacao(transactionId, amount);
            return Ok(response);
        }

        [HttpPost("cancel/{transactionId}")]
        public async Task<IActionResult> CancelTransaction(string transactionId)
        {
            var response = await _cieloService.CancelarTransacao(transactionId);
            return Ok(response);
        }
        // Método GET para teste
        [HttpGet("TestConnection")]
        public IActionResult TestConnection()
        {
            return Ok("Conexão com o backend está funcionando!");
        }
    }
}
