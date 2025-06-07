using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StormSafe.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_RIOS",
                columns: table => new
                {
                    id_rio = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RIOS", x => x.id_rio);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SENSORES",
                columns: table => new
                {
                    id_sensor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    id_rio = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SENSORES", x => x.id_sensor);
                    table.ForeignKey(
                        name: "FK_TBL_SENSORES_TBL_RIOS_id_rio",
                        column: x => x.id_rio,
                        principalTable: "TBL_RIOS",
                        principalColumn: "id_rio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SENSORES_id_rio",
                table: "TBL_SENSORES",
                column: "id_rio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_SENSORES");

            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");

            migrationBuilder.DropTable(
                name: "TBL_RIOS");
        }
    }
}
