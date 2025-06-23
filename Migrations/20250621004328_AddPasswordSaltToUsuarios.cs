using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordSaltToUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassworSalt",
                table: "TB_USUARIOS",
                newName: "PasswordSalt");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 171, 135, 188, 94, 186, 133, 64, 219, 210, 22, 137, 143, 130, 159, 150, 151, 0, 187, 6, 74, 222, 241, 213, 42, 212, 180, 235, 172, 223, 74, 191, 154, 108, 225, 124, 144, 115, 233, 26, 102, 236, 123, 118, 127, 28, 70, 20, 235, 15, 185, 19, 131, 15, 53, 155, 69, 38, 54, 0, 119, 169, 252, 13, 236 }, new byte[] { 102, 3, 101, 61, 207, 8, 196, 224, 209, 59, 91, 208, 9, 225, 107, 139, 6, 40, 26, 182, 132, 27, 116, 164, 234, 18, 22, 140, 57, 229, 233, 16, 215, 103, 123, 117, 3, 228, 127, 131, 151, 7, 31, 126, 144, 54, 118, 79, 166, 178, 128, 144, 139, 238, 124, 209, 255, 49, 123, 192, 77, 29, 138, 216, 128, 36, 214, 99, 200, 17, 254, 39, 59, 83, 72, 86, 142, 138, 197, 192, 172, 108, 165, 124, 106, 70, 232, 171, 84, 77, 117, 228, 210, 249, 196, 110, 241, 250, 81, 153, 73, 212, 17, 153, 187, 97, 185, 13, 157, 55, 77, 197, 250, 84, 18, 221, 50, 139, 204, 192, 65, 137, 79, 231, 69, 35, 230, 38 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "TB_USUARIOS",
                newName: "PassworSalt");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PassworSalt", "PasswordHash" },
                values: new object[] { new byte[] { 201, 201, 154, 157, 43, 242, 133, 49, 154, 13, 200, 0, 57, 55, 235, 243, 141, 217, 175, 239, 252, 118, 156, 106, 131, 69, 206, 150, 141, 27, 142, 216, 21, 2, 55, 194, 31, 253, 242, 106, 84, 101, 131, 182, 248, 134, 143, 148, 10, 149, 161, 192, 83, 45, 234, 23, 127, 89, 88, 218, 49, 129, 185, 193, 221, 181, 238, 174, 32, 80, 5, 247, 8, 88, 15, 104, 159, 96, 110, 209, 94, 212, 203, 228, 84, 230, 103, 104, 116, 120, 63, 190, 4, 158, 163, 111, 146, 67, 41, 192, 192, 199, 173, 119, 66, 202, 29, 35, 226, 53, 183, 82, 30, 51, 174, 160, 212, 241, 29, 182, 202, 52, 69, 181, 51, 28, 40, 159 }, new byte[] { 11, 150, 158, 140, 47, 100, 88, 65, 27, 193, 9, 96, 135, 79, 92, 240, 123, 112, 161, 176, 133, 244, 168, 225, 210, 173, 168, 69, 9, 201, 131, 132, 18, 92, 29, 64, 201, 153, 64, 80, 70, 149, 206, 237, 73, 24, 72, 155, 142, 230, 210, 107, 218, 208, 50, 224, 21, 186, 110, 8, 50, 122, 146, 188 } });
        }
    }
}
