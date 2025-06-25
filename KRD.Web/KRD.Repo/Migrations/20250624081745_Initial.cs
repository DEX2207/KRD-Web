using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KRD.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:public.color", "high_tech_two_tone,high_flash_two_tone,high_metal_two_tone,black,white,green,red,blue")
                .Annotation("Npgsql:Enum:public.option_type", "facelift,hps,swpa,abs,esp,heated_seats,electric_windows,alloy_wheels")
                .Annotation("Npgsql:Enum:public.status", "received,awaiting_confirmation,closed");

            migrationBuilder.CreateTable(
                name: "CarsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Generation = table.Column<string>(type: "text", nullable: false),
                    Config = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<int>(type: "color", nullable: false),
                    BasePrice = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OptionType = table.Column<int>(type: "option_type", nullable: false),
                    OptionStatus = table.Column<bool>(type: "boolean", nullable: false),
                    OptionPrice = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<int>(type: "status", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarOptionsDb",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    OptionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOptionsDb", x => new { x.CarId, x.OptionId });
                    table.ForeignKey(
                        name: "FK_CarOptionsDb_CarsDb_CarId",
                        column: x => x.CarId,
                        principalTable: "CarsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarOptionsDb_OptionsDb_OptionId",
                        column: x => x.OptionId,
                        principalTable: "OptionsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuyerFullName = table.Column<string>(type: "text", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    BuyerAddress = table.Column<string>(type: "text", nullable: false),
                    ContactId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatusId = table.Column<int>(type: "integer", nullable: false),
                    PaymentInvoce = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersDb_CarsDb_CarId",
                        column: x => x.CarId,
                        principalTable: "CarsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersDb_ContactsDb_ContactId",
                        column: x => x.ContactId,
                        principalTable: "ContactsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersDb_OrderStatusDb_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatusDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrdersId = table.Column<int>(type: "integer", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    ContactId = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WarrantyActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientsDb_CarsDb_CarId",
                        column: x => x.CarId,
                        principalTable: "CarsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsDb_ContactsDb_ContactId",
                        column: x => x.ContactId,
                        principalTable: "ContactsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsDb_OrdersDb_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "OrdersDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarOptionsDb_OptionId",
                table: "CarOptionsDb",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsDb_CarId",
                table: "ClientsDb",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsDb_ContactId",
                table: "ClientsDb",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsDb_OrdersId",
                table: "ClientsDb",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDb_CarId",
                table: "OrdersDb",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDb_ContactId",
                table: "OrdersDb",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDb_OrderStatusId",
                table: "OrdersDb",
                column: "OrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarOptionsDb");

            migrationBuilder.DropTable(
                name: "ClientsDb");

            migrationBuilder.DropTable(
                name: "OptionsDb");

            migrationBuilder.DropTable(
                name: "OrdersDb");

            migrationBuilder.DropTable(
                name: "CarsDb");

            migrationBuilder.DropTable(
                name: "ContactsDb");

            migrationBuilder.DropTable(
                name: "OrderStatusDb");
        }
    }
}
