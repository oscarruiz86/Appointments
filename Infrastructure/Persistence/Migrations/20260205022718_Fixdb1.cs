using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixdb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Appointment_status_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_working_hours_Employees_EmployeeId",
                table: "working_hours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_working_hours",
                table: "working_hours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment_status",
                table: "Appointment_status");

            migrationBuilder.RenameTable(
                name: "working_hours",
                newName: "WorkingHours");

            migrationBuilder.RenameTable(
                name: "Appointment_status",
                newName: "AppointmentStatus");

            migrationBuilder.RenameIndex(
                name: "IX_working_hours_EmployeeId_Weekday",
                table: "WorkingHours",
                newName: "IX_WorkingHours_EmployeeId_Weekday");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_status_TenantId_Name",
                table: "AppointmentStatus",
                newName: "IX_AppointmentStatus_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_status_TenantId",
                table: "AppointmentStatus",
                newName: "IX_AppointmentStatus_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingHours",
                table: "WorkingHours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentStatus",
                table: "AppointmentStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentStatus_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "AppointmentStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_Employees_EmployeeId",
                table: "WorkingHours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentStatus_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_Employees_EmployeeId",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingHours",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentStatus",
                table: "AppointmentStatus");

            migrationBuilder.RenameTable(
                name: "WorkingHours",
                newName: "working_hours");

            migrationBuilder.RenameTable(
                name: "AppointmentStatus",
                newName: "Appointment_status");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingHours_EmployeeId_Weekday",
                table: "working_hours",
                newName: "IX_working_hours_EmployeeId_Weekday");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentStatus_TenantId_Name",
                table: "Appointment_status",
                newName: "IX_Appointment_status_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentStatus_TenantId",
                table: "Appointment_status",
                newName: "IX_Appointment_status_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_working_hours",
                table: "working_hours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment_status",
                table: "Appointment_status",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Appointment_status_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "Appointment_status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_working_hours_Employees_EmployeeId",
                table: "working_hours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
