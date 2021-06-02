using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sandbox.Migrations
{
  public partial class InitialCreate : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Authors",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            FirstName = table.Column<string>(nullable: true),
            LastName = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Authors", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Books",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Title = table.Column<string>(nullable: true),
            Published = table.Column<DateTime>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Books", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Reviwers",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            FirstName = table.Column<string>(nullable: true),
            LastName = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Reviwers", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "BookAuthors",
          columns: table => new
          {
            BookId = table.Column<int>(nullable: false),
            AuthorId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
            table.ForeignKey(
                      name: "FK_BookAuthors_Authors_AuthorId",
                      column: x => x.AuthorId,
                      principalTable: "Authors",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_BookAuthors_Books_BookId",
                      column: x => x.BookId,
                      principalTable: "Books",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Reviews",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Headline = table.Column<string>(nullable: true),
            ReviewText = table.Column<string>(nullable: true),
            Rating = table.Column<int>(nullable: false),
            ReviewerId = table.Column<int>(nullable: true),
            BookId = table.Column<int>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Reviews", x => x.Id);
            table.ForeignKey(
                      name: "FK_Reviews_Books_BookId",
                      column: x => x.BookId,
                      principalTable: "Books",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Reviews_Reviwers_ReviewerId",
                      column: x => x.ReviewerId,
                      principalTable: "Reviwers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_BookAuthors_AuthorId",
          table: "BookAuthors",
          column: "AuthorId");

      migrationBuilder.CreateIndex(
          name: "IX_Reviews_BookId",
          table: "Reviews",
          column: "BookId");

      migrationBuilder.CreateIndex(
          name: "IX_Reviews_ReviewerId",
          table: "Reviews",
          column: "ReviewerId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "BookAuthors");

      migrationBuilder.DropTable(
          name: "Reviews");

      migrationBuilder.DropTable(
          name: "Authors");

      migrationBuilder.DropTable(
          name: "Books");

      migrationBuilder.DropTable(
          name: "Reviwers");
    }
  }
}
