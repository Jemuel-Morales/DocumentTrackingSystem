using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebBasedOneCaintaCollegeODTS.Web.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TrackingStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                table: "Status",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Under Review" },
                    { 3, "Pending Approval" },
                    { 4, "Approved" },
                    { 5, "Rejected" },
                    { 6, "On Hold" },
                    { 7, "In Progress" },
                    { 8, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Course", "DateOfBirth", "FirstName", "Gender", "LastName", "MiddleName", "Semester", "StudentNumber", "YearLevel" },
                values: new object[,]
                {
                    { 1, "BSCS", new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Male", "Doe", "Smith", 2, "OCC202012020001", 2 },
                    { 2, "BSIT", new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Female", "Mantis", "Yor", 2, "OCC202012020002", 2 }
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
                values: new object[] { 1, new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(782), new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(783), "requesting for grades", "2024-0414-0131-5080", 1, "Summary of Grades", "2404140131445080", 1 });

            migrationBuilder.InsertData(
                table: "TrackingStatus",
                columns: new[] { "Id", "Comments", "DateCreated", "DateModified", "DocumentId", "ModifiedBy", "StatusId" },
                values: new object[,]
                {
                    { 1, "this is autogenerated", new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(864), new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(865), 1, "computer", 3 },
                    { 2, "checking empty grades", new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(869), new DateTime(2024, 4, 14, 1, 31, 44, 508, DateTimeKind.Local).AddTicks(869), 1, "Ms. Angela Reyes", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StudentId",
                table: "Documents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TypeId",
                table: "Documents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingStatus_DocumentId",
                table: "TrackingStatus",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingStatus_StatusId",
                table: "TrackingStatus",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingStatus");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
