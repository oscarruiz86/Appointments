using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_Customers_CustomerId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_appointment_status_StatusId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_employees_EmployeeId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_services_ServiceId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_blocks_employees_EmployeeId",
                table: "blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_Users_UserId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_tenants_TenantId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_tenants_TenantId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_working_hours_employees_EmployeeId",
                table: "working_hours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tenants",
                table: "tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_services",
                table: "services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blocks",
                table: "blocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appointments",
                table: "appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appointment_status",
                table: "appointment_status");

            migrationBuilder.RenameTable(
                name: "tenants",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "services",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "blocks",
                newName: "Blocks");

            migrationBuilder.RenameTable(
                name: "appointments",
                newName: "Appointments");

            migrationBuilder.RenameTable(
                name: "appointment_status",
                newName: "Appointment_status");

            migrationBuilder.RenameIndex(
                name: "IX_tenants_IsActive",
                table: "Tenants",
                newName: "IX_Tenants_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_services_TenantId",
                table: "Services",
                newName: "IX_Services_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_UserId",
                table: "Employees",
                newName: "IX_Employees_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_TenantId",
                table: "Employees",
                newName: "IX_Employees_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_blocks_EmployeeId_StartAt",
                table: "Blocks",
                newName: "IX_Blocks_EmployeeId_StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_StatusId",
                table: "Appointments",
                newName: "IX_Appointments_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_ServiceId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_EmployeeId_StartAt",
                table: "Appointments",
                newName: "IX_Appointments_EmployeeId_StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_CustomerId",
                table: "Appointments",
                newName: "IX_Appointments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_appointment_status_TenantId_Name",
                table: "Appointment_status",
                newName: "IX_Appointment_status_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_appointment_status_TenantId",
                table: "Appointment_status",
                newName: "IX_Appointment_status_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blocks",
                table: "Blocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
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
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Employees_EmployeeId",
                table: "Blocks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Tenants_TenantId",
                table: "Employees",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_working_hours_Employees_EmployeeId",
                table: "working_hours",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Appointment_status_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Employees_EmployeeId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Tenants_TenantId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_working_hours_Employees_EmployeeId",
                table: "working_hours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blocks",
                table: "Blocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment_status",
                table: "Appointment_status");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "tenants");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "services");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employees");

            migrationBuilder.RenameTable(
                name: "Blocks",
                newName: "blocks");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "appointments");

            migrationBuilder.RenameTable(
                name: "Appointment_status",
                newName: "appointment_status");

            migrationBuilder.RenameIndex(
                name: "IX_Tenants_IsActive",
                table: "tenants",
                newName: "IX_tenants_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Services_TenantId",
                table: "services",
                newName: "IX_services_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UserId",
                table: "employees",
                newName: "IX_employees_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_TenantId",
                table: "employees",
                newName: "IX_employees_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Blocks_EmployeeId_StartAt",
                table: "blocks",
                newName: "IX_blocks_EmployeeId_StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_StatusId",
                table: "appointments",
                newName: "IX_appointments_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "appointments",
                newName: "IX_appointments_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_EmployeeId_StartAt",
                table: "appointments",
                newName: "IX_appointments_EmployeeId_StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CustomerId",
                table: "appointments",
                newName: "IX_appointments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_status_TenantId_Name",
                table: "appointment_status",
                newName: "IX_appointment_status_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_status_TenantId",
                table: "appointment_status",
                newName: "IX_appointment_status_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tenants",
                table: "tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_services",
                table: "services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blocks",
                table: "blocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appointments",
                table: "appointments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appointment_status",
                table: "appointment_status",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_Customers_CustomerId",
                table: "appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_appointment_status_StatusId",
                table: "appointments",
                column: "StatusId",
                principalTable: "appointment_status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_employees_EmployeeId",
                table: "appointments",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_services_ServiceId",
                table: "appointments",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_blocks_employees_EmployeeId",
                table: "blocks",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Users_UserId",
                table: "employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_tenants_TenantId",
                table: "employees",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_tenants_TenantId",
                table: "Users",
                column: "TenantId",
                principalTable: "tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_working_hours_employees_EmployeeId",
                table: "working_hours",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
