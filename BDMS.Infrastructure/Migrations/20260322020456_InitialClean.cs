using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BDMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "City", "ContactPhone", "Name" },
                values: new object[,]
                {
                    { 1, "Cheranallur, Kochi", "Kochi", "0484-6699999", "Aster Medcity" },
                    { 2, "Rajagiri Valley Rd, Aluva", "Aluva", "0484-2700600", "Rajagiri Hospital" },
                    { 3, "Ponekkara, Kochi", "Kochi", "0484-2802000", "Amrita Institute of Medical Sciences" },
                    { 4, "Pettah, Kochi", "Kochi", "0484-2662222", "Lisie Hospital" },
                    { 5, "MG Road, Kochi", "Kochi", "0484-2361400", "Medical Trust Hospital" }
                });
        }
    }
}
