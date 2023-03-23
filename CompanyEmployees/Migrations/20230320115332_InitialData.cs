using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployees.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Unity Estate, Command, Lagos", "Nigeria", "Darl Tech" });

            migrationBuilder.InsertData(
                table: "companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "1A, Ohiala, New Market Rd., Enugu", "Nigeria", "Mykroft Tech" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 29, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Darlington Obinna", "UI/UX Designer" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 26, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Israel Chidera", "Software Developer" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[] { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 27, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Nelson Ogechi", "Blogger" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
