using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerAndMatterData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lawyers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Firm = table.Column<string>(type: "text", nullable: false),
                    LoginEmail = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lawyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    LawyerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    LawyerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matters_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matters_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Lawyers",
                columns: new[] { "Id", "Firm", "FirstName", "IsAdmin", "LastName", "LoginEmail", "Password" },
                values: new object[,]
                {
                    { 1L, "Firm1", "Harvey", true, "Dent", "harvey.dent@cityofgotham.com", "two-faced" },
                    { 2L, "Firm2", "Saul", false, "Goodman", "agoodman@goodlaw.com", "callMe" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "LawyerId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1L, "customer1@customer1.com", 1L, "Customer1", "1-111-111-1111" },
                    { 2L, "customer12@customer12.com", 2L, "Customer2", "2-222-222-2222" }
                });

            migrationBuilder.InsertData(
                table: "Matters",
                columns: new[] { "Id", "CustomerId", "Description", "IsClosed", "LawyerId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "Case 1 description", false, 1L, "Name of Case 1" },
                    { 2L, 2L, "Case 2 description", false, 2L, "Name of Case 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LawyerId",
                table: "Customers",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_CustomerId",
                table: "Matters",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_LawyerId",
                table: "Matters",
                column: "LawyerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matters");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Lawyers");
        }
    }
}
