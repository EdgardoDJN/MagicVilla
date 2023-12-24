using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreación = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualización = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 22, 17, 4, 8, 854, DateTimeKind.Local).AddTicks(5975), new DateTime(2023, 12, 22, 17, 4, 8, 854, DateTimeKind.Local).AddTicks(5961) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 22, 17, 4, 8, 854, DateTimeKind.Local).AddTicks(5976), new DateTime(2023, 12, 22, 17, 4, 8, 854, DateTimeKind.Local).AddTicks(5976) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 22, 17, 4, 8, 854, DateTimeKind.Local).AddTicks(5978), new DateTime(2023, 12, 22, 17, 4, 8, 854, DateTimeKind.Local).AddTicks(5977) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5208), new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5197) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5209), new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5209) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5210), new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5210) });
        }
    }
}
