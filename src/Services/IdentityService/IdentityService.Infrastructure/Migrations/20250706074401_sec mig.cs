using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class secmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateOfBirth", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 7, 6, 7, 44, 0, 734, DateTimeKind.Utc).AddTicks(2550), new byte[] { 115, 199, 219, 13, 254, 77, 50, 185, 169, 69, 29, 79, 236, 63, 43, 228, 137, 14, 17, 16, 19, 254, 206, 120, 187, 1, 230, 134, 153, 93, 117, 235, 139, 103, 175, 159, 226, 178, 32, 48, 247, 15, 12, 147, 2, 217, 68, 208, 112, 167, 205, 148, 7, 55, 131, 191, 116, 80, 239, 46, 241, 34, 72, 161 }, new byte[] { 253, 243, 90, 158, 26, 201, 245, 234, 141, 202, 71, 85, 8, 110, 186, 217, 142, 9, 129, 116, 196, 231, 125, 98, 205, 153, 152, 249, 24, 121, 193, 174, 245, 184, 105, 6, 218, 120, 216, 67, 246, 200, 46, 110, 216, 94, 231, 98, 105, 36, 91, 152, 119, 245, 110, 76, 63, 33, 81, 67, 73, 223, 220, 202, 198, 164, 7, 48, 14, 149, 13, 166, 35, 19, 82, 143, 85, 51, 97, 136, 118, 53, 22, 144, 10, 3, 157, 72, 118, 57, 47, 244, 142, 32, 101, 40, 44, 159, 114, 105, 116, 40, 221, 208, 4, 88, 160, 121, 0, 117, 244, 242, 254, 160, 42, 157, 58, 167, 76, 6, 152, 141, 241, 213, 74, 200, 56, 44 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CapacityMB = table.Column<long>(type: "bigint", nullable: false),
                    ContentData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateOfBirth", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 6, 8, 20, 36, 26, 758, DateTimeKind.Utc).AddTicks(7180), new byte[] { 21, 188, 86, 71, 223, 175, 126, 6, 176, 20, 96, 11, 186, 238, 191, 199, 38, 174, 168, 25, 241, 166, 141, 72, 106, 147, 112, 190, 251, 14, 4, 149, 127, 137, 101, 115, 219, 249, 97, 216, 185, 216, 93, 171, 163, 140, 233, 70, 68, 0, 96, 182, 84, 167, 195, 120, 115, 132, 222, 6, 96, 245, 21, 111 }, new byte[] { 226, 17, 215, 84, 128, 114, 218, 151, 171, 52, 114, 32, 217, 111, 181, 18, 32, 23, 107, 216, 24, 73, 149, 123, 199, 195, 192, 83, 23, 58, 190, 219, 99, 222, 144, 138, 233, 222, 213, 70, 212, 197, 157, 207, 138, 16, 237, 151, 154, 230, 35, 14, 60, 203, 221, 15, 6, 28, 75, 225, 209, 46, 243, 17, 28, 48, 92, 103, 228, 130, 82, 94, 108, 13, 35, 89, 79, 61, 206, 112, 57, 254, 54, 206, 77, 39, 87, 175, 105, 78, 76, 254, 55, 212, 151, 32, 69, 161, 250, 129, 149, 197, 114, 58, 161, 132, 152, 137, 176, 28, 200, 231, 216, 95, 196, 150, 233, 218, 193, 150, 90, 196, 250, 173, 3, 129, 150, 194 } });
        }
    }
}
