using Caju.Domain.Enums;
using Caju.Domain.Models;
using Caju.Driven.Database;
using Caju.Driven.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Caju.Tests.Driven.Tests
{
    public class BalanceRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly BalanceRepository _balanceRepository;
        private readonly Mock<DbSet<Balance>> _mockBalanceSet;

        public BalanceRepositoryTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var balanceData = new List<Balance>
    {
        new Balance { UserId = "account123", TotalAmount = 500, Category = TransactionCategory.MEAL },
        new Balance { UserId = "account456", TotalAmount = 1000, Category = TransactionCategory.MEAL }
    }.AsQueryable();

            _mockBalanceSet = new Mock<DbSet<Balance>>();

            _mockBalanceSet.As<IQueryable<Balance>>().Setup(m => m.Provider).Returns(balanceData.Provider);
            _mockBalanceSet.As<IQueryable<Balance>>().Setup(m => m.Expression).Returns(balanceData.Expression);
            _mockBalanceSet.As<IQueryable<Balance>>().Setup(m => m.ElementType).Returns(balanceData.ElementType);
            _mockBalanceSet.As<IQueryable<Balance>>().Setup(m => m.GetEnumerator()).Returns(balanceData.GetEnumerator());

            _mockContext.Setup(m => m.Balances).Returns(_mockBalanceSet.Object);

            _balanceRepository = new BalanceRepository(_mockContext.Object);
        }

        [Fact]
        public void GetBalance_WhenBalanceExists_ReturnsBalance()
        {
            // Arrange
            var userId = "user123";
            var category = TransactionCategory.FOOD;
            var expectedBalance = new Balance { UserId = userId, Category = category, TotalAmount = 100 };

            var data = new List<Balance> { expectedBalance }.AsQueryable();
            var mockBalanceSet = MockHelper.CreateMockDbSet(data);

            _mockContext.Setup(m => m.Balances).Returns(mockBalanceSet.Object);

            // Act
            var result = _balanceRepository.GetBalance(userId, category);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBalance.UserId, result.UserId);
            Assert.Equal(expectedBalance.Category, result.Category);
            Assert.Equal(expectedBalance.TotalAmount, result.TotalAmount);
        }
    }
}
