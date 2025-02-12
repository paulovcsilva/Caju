using Caju.Domain.Models;
using Caju.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;


namespace Caju.Driver.HttpServer
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(TransactionsUseCase transactionsUseCase) : ControllerBase
    {

        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] TransactionRequest transaction)
        {
            try
            {
                var response = transactionsUseCase.Execute(transaction);
                return response;
            }
            catch (Exception ex)
            {
                return Ok(new { code = "07", id = transaction.Id, error = ex.Message });
            }
        }
    }
}
