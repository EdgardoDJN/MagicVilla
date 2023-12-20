using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Amenidad 1", "Villa 1", new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5208), new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5197), "", 100.0, "Villa 1", 4, 100.0 },
                    { 2, "Amenidad 2", "Villa 2", new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5209), new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5209), "", 150.0, "Villa 2", 6, 150.0 },
                    { 3, "Amenidad 3", "Villa 3", new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5210), new DateTime(2023, 12, 19, 20, 0, 52, 820, DateTimeKind.Local).AddTicks(5210), "", 200.0, "Villa 3", 8, 200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
