using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Migrations
{
    public partial class UpdatedToDoItemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ToDoItems",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ToDoItems",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ToDoItems",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
