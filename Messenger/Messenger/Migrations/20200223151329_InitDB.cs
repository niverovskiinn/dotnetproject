using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Messenger.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Dialogues",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserIdFirst = table.Column<int>(),
                    UserIdSecond = table.Column<int>(),
                    Created = table.Column<DateTime>()
                },
                constraints: table => { table.PrimaryKey("PK_Dialogues", x => x.Id); });

            migrationBuilder.CreateTable(
                "Messages",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DialogueId = table.Column<int>(),
                    UserIdFrom = table.Column<int>(),
                    Data = table.Column<string>(),
                    MsgTime = table.Column<DateTime>()
                },
                constraints: table => { table.PrimaryKey("PK_Messages", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(),
                    Login = table.Column<string>(),
                    Birthday = table.Column<DateTime>(),
                    Password = table.Column<string>(),
                    SignInTime = table.Column<DateTime>(),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Dialogues");

            migrationBuilder.DropTable(
                "Messages");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}