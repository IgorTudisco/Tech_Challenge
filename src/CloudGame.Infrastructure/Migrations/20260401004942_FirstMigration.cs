using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(120)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(120)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
