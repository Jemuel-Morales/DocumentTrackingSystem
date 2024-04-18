using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocumentTrackingSystem.Web.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearLevel = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
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
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
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

            migrationBuilder.CreateTable(
                name: "TrackingStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingStatus_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackingStatus_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Registrar" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Under Review" },
                    { 2, "Pending Approval" },
                    { 3, "Approved" },
                    { 4, "Rejected" },
                    { 5, "On Hold" },
                    { 6, "In Progress" },
                    { 7, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Course", "DateOfBirth", "FirstName", "Gender", "LastName", "MiddleName", "Semester", "StudentNumber", "YearLevel" },
                values: new object[,]
                {
                    { 1, "BSCS", new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Male", "Doe", "Smith", 2, "OCC202012020001", 2 },
                    { 2, "BSIT", new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Female", "Mantis", "Yor", 2, "OCC202012020002", 2 },
                    { 3, "BSIT", new DateTime(2000, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kate", "Female", "Bernal", "Bernal", 2, "OCC202012020003", 4 }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Letter" },
                    { 2, "Form" },
                    { 3, "Report" },
                    { 4, "Compliance" },
                    { 5, "Support" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Description", "DocumentNumber", "StudentId", "Subject", "TrackingNumber", "TypeId" },
                values: new object[] { 1, new DateTime(2024, 4, 19, 2, 44, 13, 816, DateTimeKind.Local).AddTicks(638), new DateTime(2024, 4, 19, 2, 44, 13, 816, DateTimeKind.Local).AddTicks(639), "Requesting for grades", "2024-0419-0244-8160", 1, "Summary of Grades", "2404190244138160", 3 });

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

            migrationBuilder.InsertData(
                table: "TrackingStatus",
                columns: new[] { "Id", "Comments", "CreatedBy", "DateCreated", "DocumentId", "StatusId" },
                values: new object[] { 1, "this is autogenerated", "computer", new DateTime(2024, 4, 19, 2, 44, 13, 816, DateTimeKind.Local).AddTicks(724), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StudentId",
                table: "Documents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TypeId",
                table: "Documents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_People_UserId",
                table: "People",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackingStatus_DocumentId",
                table: "TrackingStatus",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingStatus_StatusId",
                table: "TrackingStatus",
                column: "StatusId");

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
                name: "TrackingStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
