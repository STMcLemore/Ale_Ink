using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ale_Ink.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "ItemNote",
                columns: table => new
                {
                    ItemsItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    NotesNoteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNote", x => new { x.ItemsItemId, x.NotesNoteId });
                    table.ForeignKey(
                        name: "FK_ItemNote_Items_ItemsItemId",
                        column: x => x.ItemsItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemNote_Notes_NotesNoteId",
                        column: x => x.NotesNoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPerson",
                columns: table => new
                {
                    ItemsItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PeoplePersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPerson", x => new { x.ItemsItemId, x.PeoplePersonId });
                    table.ForeignKey(
                        name: "FK_ItemPerson_Items_ItemsItemId",
                        column: x => x.ItemsItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPerson_People_PeoplePersonId",
                        column: x => x.PeoplePersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotePerson",
                columns: table => new
                {
                    NotesNoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    PeoplePersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotePerson", x => new { x.NotesNoteId, x.PeoplePersonId });
                    table.ForeignKey(
                        name: "FK_NotePerson_Notes_NotesNoteId",
                        column: x => x.NotesNoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotePerson_People_PeoplePersonId",
                        column: x => x.PeoplePersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPlace",
                columns: table => new
                {
                    ItemsItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacesPlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPlace", x => new { x.ItemsItemId, x.PlacesPlaceId });
                    table.ForeignKey(
                        name: "FK_ItemPlace_Items_ItemsItemId",
                        column: x => x.ItemsItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPlace_Places_PlacesPlaceId",
                        column: x => x.PlacesPlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotePlace",
                columns: table => new
                {
                    NotesNoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacesPlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotePlace", x => new { x.NotesNoteId, x.PlacesPlaceId });
                    table.ForeignKey(
                        name: "FK_NotePlace_Notes_NotesNoteId",
                        column: x => x.NotesNoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotePlace_Places_PlacesPlaceId",
                        column: x => x.PlacesPlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPlace",
                columns: table => new
                {
                    PeoplePersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacesPlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPlace", x => new { x.PeoplePersonId, x.PlacesPlaceId });
                    table.ForeignKey(
                        name: "FK_PersonPlace_People_PeoplePersonId",
                        column: x => x.PeoplePersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPlace_Places_PlacesPlaceId",
                        column: x => x.PlacesPlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemNote_NotesNoteId",
                table: "ItemNote",
                column: "NotesNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPerson_PeoplePersonId",
                table: "ItemPerson",
                column: "PeoplePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPlace_PlacesPlaceId",
                table: "ItemPlace",
                column: "PlacesPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_NotePerson_PeoplePersonId",
                table: "NotePerson",
                column: "PeoplePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_NotePlace_PlacesPlaceId",
                table: "NotePlace",
                column: "PlacesPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPlace_PlacesPlaceId",
                table: "PersonPlace",
                column: "PlacesPlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemNote");

            migrationBuilder.DropTable(
                name: "ItemPerson");

            migrationBuilder.DropTable(
                name: "ItemPlace");

            migrationBuilder.DropTable(
                name: "NotePerson");

            migrationBuilder.DropTable(
                name: "NotePlace");

            migrationBuilder.DropTable(
                name: "PersonPlace");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
