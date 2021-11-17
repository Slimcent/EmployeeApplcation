using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccountNumber", "Age", "Name" },
                values: new object[] { new Guid("af428052-91cf-4f36-ac68-e929c7b57a6c"), "123-3452134543-32", 30, "Mark" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AccountNumber", "Age", "Name" },
                values: new object[] { new Guid("1f4638b7-3e45-4620-852c-a7cd2a1d7281"), "123-9384613085-55", 28, "Evelin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
