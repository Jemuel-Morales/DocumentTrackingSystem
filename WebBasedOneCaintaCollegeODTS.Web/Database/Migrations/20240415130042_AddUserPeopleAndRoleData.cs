using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBasedOneCaintaCollegeODTS.Web.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPeopleAndRoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated", "DocumentNumber", "TrackingNumber" },
                values: new object[] { new DateTime(2024, 4, 15, 21, 0, 38, 73, DateTimeKind.Local).AddTicks(5449), new DateTime(2024, 4, 15, 21, 0, 38, 73, DateTimeKind.Local).AddTicks(5450), "2024-0415-2100-0735", "2404152100380734" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Registrar" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Course", "DateOfBirth", "FirstName", "Gender", "LastName", "MiddleName", "Semester", "StudentNumber", "YearLevel" },
                values: new object[] { 3, "BSIT", new DateTime(2000, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kate", "Female", "Bernal", "Bernal", 2, "OCC202012020003", 4 });

            migrationBuilder.UpdateData(
                table: "TrackingStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 4, 15, 21, 0, 38, 73, DateTimeKind.Local).AddTicks(5566), new DateTime(2024, 4, 15, 21, 0, 38, 73, DateTimeKind.Local).AddTicks(5569) });

            migrationBuilder.UpdateData(
                table: "TrackingStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 4, 15, 21, 0, 38, 73, DateTimeKind.Local).AddTicks(5573), new DateTime(2024, 4, 15, 21, 0, 38, 73, DateTimeKind.Local).AddTicks(5575) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "Abraham24@occ-registrar.edu.ph", "Abraham24", 2, "Abraham24" },
                    { 2, "Atkinson6@occ-registrar.edu.ph", "Atkinson6", 2, "Atkinson6" },
                    { 3, "CelineV10@occ-registrar.edu.ph", "CelineV10", 2, "CelineV10" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "MiddleName", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1993, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abraham", "Male", "De Leon", "Tapel", 1 },
                    { 2, new DateTime(1993, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jessica", "Female", "Atkinson", "Cy", 2 },
                    { 3, new DateTime(1993, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celine", "Female", "Vargas", "Bernal", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_UserId",
                table: "People",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated", "DocumentNumber", "TrackingNumber" },
                values: new object[] { new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(782), new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(783), "2024-0414-0131-5080", "2404140131445080" });

            migrationBuilder.UpdateData(
                table: "TrackingStatus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(864), new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(865) });

            migrationBuilder.UpdateData(
                table: "TrackingStatus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(869), new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(869) });
        }
    }
}
