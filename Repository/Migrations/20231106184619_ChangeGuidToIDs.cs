using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGuidToIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("ALTER TABLE Employees DROP CONSTRAINT FK_Employees_Companies_CompanyId");

            migrationBuilder.DropPrimaryKey(
                  name: "PK_Employees",
                  table: "Employees");
            migrationBuilder.Sql("ALTER TABLE Employees DROP COLUMN EmployeeId");
            migrationBuilder.Sql("ALTER TABLE Employees ADD EmployeeId BIGINT IDENTITY(1,1)");

            migrationBuilder.DropPrimaryKey(
                   name: "PK_Companies",
                   table: "Companies");
            migrationBuilder.Sql("ALTER TABLE Companies DROP COLUMN CompanyId");
            migrationBuilder.Sql("ALTER TABLE Companies ADD CompanyId BIGINT IDENTITY(1,1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Companies DROP CONSTRAINT FK_Employees_Companies_CompanyId");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "CompanyId");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.Sql("ALTER TABLE Employees ADD CONSTRAINT FK_Employees_Companies_CompanyId FOREIGN KEY (CompanyId) REFERENCES Companies(CompanyId)");
        }
    }
}
