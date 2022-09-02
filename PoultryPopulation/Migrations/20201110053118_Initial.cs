using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoultryPopulation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    StreetAddress2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChickenBreeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PrimaryColor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChickenBreeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChickenCoops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChickenCoops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChickenCoops_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chickens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(nullable: true),
                    Modified = table.Column<DateTimeOffset>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    IsAdoptable = table.Column<bool>(nullable: false),
                    AdoptionFee = table.Column<decimal>(nullable: true),
                    ChickenBreedId = table.Column<int>(nullable: false),
                    ChickenCoopId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chickens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chickens_ChickenBreeds_ChickenBreedId",
                        column: x => x.ChickenBreedId,
                        principalTable: "ChickenBreeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chickens_ChickenCoops_ChickenCoopId",
                        column: x => x.ChickenCoopId,
                        principalTable: "ChickenCoops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chickens_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChickenCoops_OwnerId",
                table: "ChickenCoops",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_ChickenBreedId",
                table: "Chickens",
                column: "ChickenBreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_ChickenCoopId",
                table: "Chickens",
                column: "ChickenCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_OwnerId",
                table: "Chickens",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_AddressId",
                table: "Owners",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chickens");

            migrationBuilder.DropTable(
                name: "ChickenBreeds");

            migrationBuilder.DropTable(
                name: "ChickenCoops");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
