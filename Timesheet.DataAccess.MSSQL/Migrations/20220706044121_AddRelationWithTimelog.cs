﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.DataAccess.MSSQL.Migrations
{
    public partial class AddRelationWithTimelog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "TimeLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeLogs_EmployeeId",
                table: "TimeLogs",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLogs_Employee_EmployeeId",
                table: "TimeLogs",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLogs_Employee_EmployeeId",
                table: "TimeLogs");

            migrationBuilder.DropIndex(
                name: "IX_TimeLogs_EmployeeId",
                table: "TimeLogs");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TimeLogs");
        }
    }
}
