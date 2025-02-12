using Caju.Domain.Enums;
using Caju.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Caju.Driven.Database.Configurations
{
    public class ApplicationDbContextModelBuilder
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Balance>()
                .HasKey(b => new { b.Category, b.UserId });

            modelBuilder.Entity<Balance>()
                .Property(b => b.Category)
                .HasConversion(
                    v => v.ToString(),  // Armazena o valor do enum como string
                    v => (TransactionCategory)Enum.Parse(typeof(TransactionCategory), v))  // Converte de volta para enum
                .IsRequired();


            modelBuilder.Entity<Balance>().HasData(
                new Balance { UserId = "123", Category = TransactionCategory.FOOD, TotalAmount = 480.00m },
                new Balance { UserId = "123", Category = TransactionCategory.MEAL, TotalAmount = 160.00m },
                new Balance { UserId = "123", Category = TransactionCategory.CASH, TotalAmount = 100.00m },
                new Balance { UserId = "456", Category = TransactionCategory.FOOD, TotalAmount = 300.00m },
                new Balance { UserId = "456", Category = TransactionCategory.MEAL, TotalAmount = 150.00m },
                new Balance { UserId = "456", Category = TransactionCategory.CASH, TotalAmount = 450.00m });

            modelBuilder.Entity<Merchant>().HasKey(t => t.Id);

            modelBuilder.Entity<Merchant>().HasData(
                new Merchant { Id = 1, Name = "UBER EATS SAO PAULO BR", Mcc = "5811" },
                new Merchant { Id = 2, Name = "UBER TRIP SAO PAULO BR", Mcc = "4444" },
                new Merchant { Id = 3, Name = "PICPAY*BILHETUNICO GOIANIA BR", Mcc = "5555" },
                new Merchant { Id = 4, Name = "PAG*JoseDaSilva RIO DE JANEIRO BR", Mcc = "5811" });
        }
    }
}
