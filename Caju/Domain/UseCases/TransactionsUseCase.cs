using Caju.Domain.Enums;
using Caju.Domain.Models;
using Caju.Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Caju.Domain.UseCases
{
    public class TransactionsUseCase(ICategoryPorts categoryPorts, IBalancePorts balancePorts, ITransactionPorts transactionPorts)
    {
        private readonly ICategoryPorts _categoryPorts = categoryPorts;
        private readonly IBalancePorts _balancePorts = balancePorts;
        private readonly ITransactionPorts _transactionPorts = transactionPorts;
        public IActionResult Execute(TransactionRequest transaction)
        {
            try
            {
                transaction.Id = Guid.NewGuid().ToString();

                var transactionCategory = _categoryPorts.GetCategory(transaction.Mcc, transaction.Merchant) ?? TransactionCategory.CASH;

                var accountBalance = _balancePorts.GetBalance(transaction.AccountId, transactionCategory);

                if (accountBalance.TotalAmount < transaction.Amount)
                {
                    transactionCategory = TransactionCategory.CASH;
                    accountBalance = _balancePorts.GetBalance(transaction.AccountId, transactionCategory);

                    if (accountBalance.TotalAmount < transaction.Amount)
                    {
                        return new OkObjectResult(new { code = "51", id = transaction.Id });
                    }
                }

                accountBalance.TotalAmount -= transaction.Amount;
                _transactionPorts.ProcessTransactionDebit(accountBalance, transaction.Amount);

                return new OkObjectResult(new { code = "00", id = transaction.Id });
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new { code = "07", id = transaction.Id, error = ex.Message });
            }
        }
    }
}

