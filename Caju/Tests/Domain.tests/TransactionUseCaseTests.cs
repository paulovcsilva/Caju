using System.Diagnostics;
using Caju.Domain.Enums;
using Caju.Domain.Models;
using Caju.Domain.Ports;
using Caju.Domain.UseCases;
using Caju.Driven.Database.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Caju.Tests.Domain.tests
{
    public class TransactionUseCaseTests
    {
        private readonly ApplicationDbContext _context;
        private readonly TransactionsUseCase _transactionsUseCase;
        private readonly Mock<ICategoryPorts> _mockCategoryService;
        private readonly Mock<IBalancePorts> _mockBalanceService;
        private readonly Mock<ITransactionPorts> _mockTransactionService;

        public TransactionUseCaseTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CajuTestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Merchants.Add(new Merchant { Name = "Merchant1", Mcc = "5411" });
            _context.SaveChanges();

            _mockCategoryService = new Mock<ICategoryPorts>();
            _mockBalanceService = new Mock<IBalancePorts>();
            _mockTransactionService = new Mock<ITransactionPorts>();

            _transactionsUseCase = new TransactionsUseCase(
                _mockCategoryService.Object,
                _mockBalanceService.Object,
                _mockTransactionService.Object);
        }

        [Fact]
        public void Execute_ShouldProcessTransactionSuccessfully_WhenBalanceIsSufficient()
        {
            // Arrange
            var transaction = new TransactionRequest
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 100,
                AccountId = "account123",
                Mcc = "5411",
                Merchant = "Merchant1"
            };

            var expectedCategory = TransactionCategory.FOOD;

            _mockCategoryService.Setup(service => service.GetCategory(transaction.Mcc, transaction.Merchant))
                .Returns(expectedCategory);

            var balance = new Balance
            {
                UserId = transaction.AccountId,
                Category = TransactionCategory.FOOD,
                TotalAmount = 500
            };

            _context.Balances.Add(balance);
            _context.SaveChanges();

            _mockBalanceService.Setup(service => service.GetBalance(transaction.AccountId, expectedCategory))
                .Returns(balance);

            _mockTransactionService.Setup(service => service.ProcessTransactionDebit(balance, transaction.Amount));

            // Act
            var result = _transactionsUseCase.Execute(transaction);

            // Assert
            var updatedBalance = _context.Balances.First(b => b.UserId == transaction.AccountId);
            Assert.Equal(400, updatedBalance.TotalAmount);
        }

        [Fact]
        public void Execute_ShouldProcessTransactionWithCashCategory_WhenBalanceIsInsufficient()
        {
            // Arrange
            var transaction = new TransactionRequest
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 600,
                AccountId = "account1234",
                Mcc = "5411",
                Merchant = "Merchant1"
            };

            var expectedCategory = TransactionCategory.FOOD;
            _mockCategoryService.Setup(service => service.GetCategory(transaction.Mcc, transaction.Merchant))
                .Returns(expectedCategory);

            var balance = new Balance
            {
                UserId = transaction.AccountId,
                Category = TransactionCategory.FOOD,
                TotalAmount = 500
            };

            _context.Balances.Add(balance);
            _context.SaveChanges();


            _mockBalanceService.Setup(service => service.GetBalance(transaction.AccountId, expectedCategory))
                .Returns(balance);;

            var cashCategory = TransactionCategory.CASH;
            var cashBalance = new Balance { UserId = "account123", TotalAmount = 500, Category = TransactionCategory.CASH };
            _mockBalanceService.Setup(service => service.GetBalance(transaction.AccountId, cashCategory))
                .Returns(cashBalance);

            _mockTransactionService.Setup(service => service.ProcessTransactionDebit(balance, transaction.Amount));

            // Act
            var result = _transactionsUseCase.Execute(transaction);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = okResult.Value as dynamic;
            Assert.Equal("51", value.code);
        }
    }
}