using Microsoft.EntityFrameworkCore;
using Moq;

namespace Caju.Tests
{
    public class MockHelper
    {
        public static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockDbSet = new Mock<DbSet<T>>();

            // Configurar o comportamento do DbSet para simular consultas LINQ
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockDbSet;
        }
    }
}
