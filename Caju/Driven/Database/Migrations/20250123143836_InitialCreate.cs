using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Caju.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => new { x.Category, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mcc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Balances",
                columns: new[] { "Category", "UserId", "TotalAmount" },
                values: new object[,]
                {
                    { "CASH", "123", 100.00m },
                    { "CASH", "456", 450.00m },
                    { "FOOD", "123", 480.00m },
                    { "FOOD", "456", 300.00m },
                    { "MEAL", "123", 160.00m },
                    { "MEAL", "456", 150.00m }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "Mcc", "Name" },
                values: new object[,]
                {
                    { 1, "5811", "UBER EATS SAO PAULO BR" },
                    { 2, "4444", "UBER TRIP SAO PAULO BR" },
                    { 3, "5555", "PICPAY*BILHETUNICO GOIANIA BR" },
                    { 4, "5811", "PAG*JoseDaSilva RIO DE JANEIRO BR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Merchants");
        }
    }
}
