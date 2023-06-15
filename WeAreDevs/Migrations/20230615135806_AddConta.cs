using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeAreDevs.Migrations
{
    /// <inheritdoc />
    public partial class AddConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContaId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ContaId",
                table: "Usuarios",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_Id",
                table: "Contas",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Contas_ContaId",
                table: "Usuarios",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Contas_ContaId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ContaId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Usuarios");
        }
    }
}
