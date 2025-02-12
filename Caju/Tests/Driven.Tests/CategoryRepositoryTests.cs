using Caju.Domain.Enums;
using Caju.Domain.Models;
using Caju.Driven.Database;
using Caju.Driven.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Caju.Tests.Driven.Tests
{
    public class CategoryRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            _categoryRepository = new CategoryRepository(_mockContext.Object);
        }

        [Fact]
        public void GetCategory_ShouldReturnNull_WhenCategoryNotFound()
        {
            // Arrange
            var merchantName = "NonExistentMerchant";
            var merchants = new Merchant[] { }.AsQueryable();

            var mockDbSet = MockHelper.CreateMockDbSet(merchants);
            _mockContext.Setup(c => c.Merchants).Returns(mockDbSet.Object);

            var result = _categoryRepository.GetCategory("1234", merchantName);
            // Act & Assert
            Assert.Null(result);
        }
    }
}