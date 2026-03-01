using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BDMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hospitals");
        }
    }
}
