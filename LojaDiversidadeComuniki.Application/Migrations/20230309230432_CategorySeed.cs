using LojaDiversidadeComuniki.Domain.Model.Products;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaDiversidadeComuniki.Application.Migrations
{
    /// <inheritdoc />
    public partial class CategorySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"insert into Categories (id, name)
            values('{Guid.NewGuid()}', 'Vestuario')");

            migrationBuilder.Sql($@"Insert Into Categories(Id,Name)
            Values('{Guid.NewGuid()}', 'Casa')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categories");
        }
    }
}
