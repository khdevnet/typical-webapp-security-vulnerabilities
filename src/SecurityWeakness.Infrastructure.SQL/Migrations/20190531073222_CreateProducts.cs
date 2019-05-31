using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SecurityWeakness.Infrastructure.SQL.Migrations
{
    public partial class CreateProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    sku = table.Column<string>(maxLength: 255, nullable: false),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "name", "price", "sku" },
                values: new object[,]
                {
                    { 1, "R2-D2", 200m, "p1" },
                    { 2, "Speeder", 300m, "p2" },
                    { 3, "Speeder2", 500m, "p3" },
                    { 4, "Speeder3", 600m, "p4" },
                    { 5, "BB-8", 400m, "p5" },
                    { 6, "Blaster", 700m, "p6" },
                    { 7, "Death star", 8000m, "p7" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
