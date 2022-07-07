using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.DataAccess.MSSQL.Migrations
{
    public partial class AddEmployeeBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLogs_Employee_EmployeeId",
                table: "TimeLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Position",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Bonus", "LastName", "Position", "Salary" },
                values: new object[] { 1, 20000m, "Иванов", 0, 200000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Bonus", "LastName", "Position", "Salary" },
                values: new object[] { 2, null, "Сидоров", 2, 120000m });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Bonus", "LastName", "Position", "Salary" },
                values: new object[] { 3, null, "Петров", 1, 1000m });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLogs_Employees_EmployeeId",
                table: "TimeLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLogs_Employees_EmployeeId",
                table: "TimeLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLogs_Employee_EmployeeId",
                table: "TimeLogs",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
