using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    public partial class Populate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("4b405a5b-9652-4213-8068-85639e99cfe1"), "Suzanne", "Collins" },
                    { new Guid("69c7fb18-f99d-4f2d-b015-62c91120c31c"), "Andy", "Weir" },
                    { new Guid("d35450ed-c943-48c5-88ed-ab8a9f5eb064"), "Joanne", "Rowling" },
                    { new Guid("e29fcc24-13ad-4fb0-a782-6edd85f2fa3e"), "Cassandra", "Clare" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("45dfbab4-60df-4037-ba97-2f6088719cca"), "Sience fiction", "SCI-FI" },
                    { new Guid("79df8d8a-6956-4682-9361-78a654c1320c"), "Dragons, mythical quests.", "Fantasy" },
                    { new Guid("93c5439e-64b7-48e6-aa4a-90ef6e06bf5a"), null, "Dystopian" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { new Guid("16dd018e-b199-4355-9ae1-18ab5556ad4f"), "Dustin", "Joosen", 1 },
                    { new Guid("98b4cfd2-303e-4364-a9dc-c5c58ccc7237"), "Ky", "Coven", 0 }
                });
            }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("16dd018e-b199-4355-9ae1-18ab5556ad4f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98b4cfd2-303e-4364-a9dc-c5c58ccc7237"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4b405a5b-9652-4213-8068-85639e99cfe1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("69c7fb18-f99d-4f2d-b015-62c91120c31c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d35450ed-c943-48c5-88ed-ab8a9f5eb064"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("e29fcc24-13ad-4fb0-a782-6edd85f2fa3e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("45dfbab4-60df-4037-ba97-2f6088719cca"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("79df8d8a-6956-4682-9361-78a654c1320c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("93c5439e-64b7-48e6-aa4a-90ef6e06bf5a"));
        }
    }
}
