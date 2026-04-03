using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Slogan = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Acronym = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    ReactiveStock = table.Column<bool>(type: "boolean", nullable: false),
                    PublicPrices = table.Column<bool>(type: "boolean", nullable: false),
                    PublicOrders = table.Column<bool>(type: "boolean", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Stock = table.Column<decimal>(type: "numeric", nullable: false),
                    Alert = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CompanyIds = table.Column<List<int>>(type: "integer[]", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    PasswordResetExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(48)", maxLength: 48, nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    PremiseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.PremiseId);
                    table.ForeignKey(
                        name: "FK_Premises_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Layouts",
                columns: table => new
                {
                    LayoutId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    PremiseId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layouts", x => x.LayoutId);
                    table.ForeignKey(
                        name: "FK_Layouts_Premises_PremiseId",
                        column: x => x.PremiseId,
                        principalTable: "Premises",
                        principalColumn: "PremiseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumptions",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumptions", x => new { x.ProductId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Consumptions_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consumptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    BLOB = table.Column<byte[]>(type: "bytea", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LayoutCoords",
                columns: table => new
                {
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    LayoutId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutCoords", x => new { x.X, x.Y, x.LayoutId });
                    table.ForeignKey(
                        name: "FK_LayoutCoords_Layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "Layouts",
                        principalColumn: "LayoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    SectorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LayoutId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    PositionX = table.Column<int>(type: "integer", nullable: false),
                    PositionY = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.SectorId);
                    table.ForeignKey(
                        name: "FK_Sectors_Layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "Layouts",
                        principalColumn: "LayoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LayoutId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    QrId = table.Column<string>(type: "text", nullable: false),
                    PositionX = table.Column<int>(type: "integer", nullable: false),
                    PositionY = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                    table.ForeignKey(
                        name: "FK_Tables_Layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "Layouts",
                        principalColumn: "LayoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectorCoords",
                columns: table => new
                {
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    SectorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorCoords", x => new { x.X, x.Y, x.SectorId });
                    table.ForeignKey(
                        name: "FK_SectorCoords_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "SectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.TableId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableCoords",
                columns: table => new
                {
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    TableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableCoords", x => new { x.X, x.Y, x.TableId });
                    table.ForeignKey(
                        name: "FK_TableCoords_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Acronym", "Enabled", "Name", "PublicOrders", "PublicPrices", "ReactiveStock", "Slogan" },
                values: new object[] { 1, "MA", true, "Maria Antonieta", true, true, true, "Universo Deli" });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Alert", "Enabled", "Name", "Stock", "Unit" },
                values: new object[,]
                {
                    { 1, 10, true, "Harina 000", 50m, 0 },
                    { 2, 5, true, "Tomate fresco", 30m, 0 },
                    { 3, 3, true, "Queso mozzarela", 20m, 0 },
                    { 4, 2, true, "Aceite de oliva", 15m, 4 },
                    { 5, 4, true, "Cebolla", 3m, 0 },
                    { 6, 2, true, "Pollo", 10m, 0 },
                    { 7, 8, true, "Pasta seca", 40m, 0 },
                    { 8, 2, true, "Salsa de tom ", 12m, 4 },
                    { 9, 50, true, "Albaca", 200m, 1 },
                    { 10, 1, true, "Vino tinto", 8m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Enabled", "ImageId", "Name", "ParentId", "Price" },
                values: new object[,]
                {
                    { 22, null, "", true, null, "Espresso", 1, 1000m },
                    { 23, null, "", true, null, "Espresso macchiato", 1, 1000m },
                    { 24, null, "", true, null, "Doppio", 1, 1000m },
                    { 25, null, "", true, null, "Lungo", 1, 1000m },
                    { 26, null, "", true, null, "Americano simple/doble", 1, 1000m },
                    { 27, null, "", true, null, "Cortado", 1, 1000m },
                    { 28, null, "", true, null, "Flat White", 1, 1000m },
                    { 29, null, "", true, null, "Capuchino", 1, 1000m },
                    { 30, null, "", true, null, "Latte", 1, 1000m },
                    { 31, null, "", true, null, "Moca", 1, 1000m },
                    { 32, null, "", true, null, "Caramel Latte", 1, 1000m },
                    { 33, null, "", true, null, "Vanilla latte", 1, 1000m },
                    { 34, null, "", true, null, "Avellana latte", 1, 1000m },
                    { 35, null, "Chocolate especial con pasta de avellanas", true, null, "Nutella latte", 1, 1000m },
                    { 36, null, "", true, null, "Espresso con hielo", 3, 1000m },
                    { 37, null, "", true, null, "Espresso tonic", 3, 1000m },
                    { 38, null, "", true, null, "Cold Flat", 3, 1000m },
                    { 39, null, "", true, null, "Iced Latte", 3, 1000m },
                    { 40, null, "", true, null, "Iced caramel latte", 3, 1000m },
                    { 41, null, "", true, null, "Iced vanilla latte", 3, 1000m },
                    { 42, null, "", true, null, "Iced avellana latte", 3, 1000m },
                    { 43, null, "", true, null, "Iced americano simple/doble", 3, 1000m },
                    { 44, null, "", true, null, "Con jamon cocido y queso tybo", 4, 1000m },
                    { 45, null, "", true, null, "Con rucula, tomates secos, queso parmesano y oliva", 4, 1000m },
                    { 46, null, "", true, null, "Con dulce de leche y azucar impalpable", 4, 1000m },
                    { 47, null, "", true, null, "Con nutella y azucar impalpable", 4, 1000m },
                    { 48, null, "", true, null, "Croissant", 4, 1000m },
                    { 49, null, "", true, null, "Pain au chocolat", 4, 1000m },
                    { 50, null, "", true, null, "Roll de canela", 4, 1000m },
                    { 51, null, "", true, null, "Zanahoria", 5, 1000m },
                    { 52, null, "", true, null, "Limon y amapolas", 5, 1000m },
                    { 53, null, "", true, null, "Avellanas y chocolate (vegano)", 5, 1000m },
                    { 54, null, "", true, null, "Banana y tahini", 5, 1000m },
                    { 55, null, "", true, null, "Almendras coco y chocolate negro.", 6, 1000m },
                    { 56, null, "", true, null, "Limón chocolate blanco y pistacho.", 6, 1000m },
                    { 57, null, "", true, null, "Chocolate con sal (vegana)", 6, 1000m },
                    { 58, null, "", true, null, "Alfajor de nuez.", 6, 1000m },
                    { 59, null, "", true, null, "Scones de queso y pimienta", 6, 1000m },
                    { 60, null, "", true, null, "Scones de queso y verdeo", 6, 1000m },
                    { 61, null, "", true, null, "Chipa", 6, 1000m },
                    { 62, null, "", true, null, "Alfajorcito de mani con dulce de leche y sésamo", 7, 1000m },
                    { 63, null, "", true, null, "Alfajorcito de mani con chocolate y dulce de leche", 7, 1000m },
                    { 64, null, "", true, null, "Alfajorcito de maicena", 7, 1000m },
                    { 65, null, "", true, null, "Pepas (2 unidades)", 7, 1000m },
                    { 66, null, "", true, null, "Conito de dulce de leche", 7, 1000m },
                    { 67, null, "Jamón cocido y queso tybo con pulpa de tomate en pan baguette.", true, null, "Catalan", 8, 1000m },
                    { 68, null, "Rucula, toates cherry confitados, queso tybo, pesto de albahaca, aceitunas negras en pan baguette.", true, null, "Mediterraneo", 8, 1000m },
                    { 69, null, "Mix de hojas verdes, cherry confitado, zucchini, queso tybo, pepino, mayo de zanahorias, en pan ciabatta.", true, null, "Vegetariano", 8, 1000m },
                    { 70, null, "Lomito ahumado, rúcula, pimientossalteados, cebolla caramelizada, parmesano y lactonesa de hierbas en pan ciabatta", true, null, "Pauza", 8, 1000m },
                    { 71, null, "Jamón cocido, queso tybo en pan de molde con salsa bechamel y queso gratinado.", true, null, "Sandwich Croque Monsieur", 8, 1000m },
                    { 72, null, "Jamón cocido, queso tybo en pan de molde con salsa bechamel,queso gratinado y huevo frito.", true, null, "Sandwich Croque Madame", 8, 1000m },
                    { 73, null, "Lomito ahumado, queso tybo y huevo.", true, null, "Bagel", 8, 1000m },
                    { 74, null, "", true, null, "De pan de molde con mermelada/miel y queso crema/ manteca.", 9, 1000m },
                    { 75, null, "", true, null, "De pan de campo con pulpa de tomate, queso parmesano y pesto de albahaca.", 9, 1000m },
                    { 76, null, "", true, null, "De pan de campo con queso crema, palta y huevo revuelto.", 9, 1000m },
                    { 77, null, "", true, null, "De hogaza maíz morado con queso crema, palta, brotes de lenteja y semillas.", 9, 1000m },
                    { 78, null, "", true, null, "Francesa con fruta de estación y granola con culis de naranja y menta.", 9, 1000m },
                    { 79, null, "", true, null, "Yogurt con granola, frutas de estación y miel", 9, 1000m },
                    { 80, null, "Espuma de espinaca concubos de calabacin y choclo salteado, tomate cherry,cebollacaramelizada en masa quebrada.", true, null, "Tarta veggie", 13, 1000m },
                    { 81, null, "Pollo, cebollas caramelizadas, espinaca, queso azul y nueces en masa quebrada.", true, null, "Tarta gourmet", 13, 1000m },
                    { 82, null, "Tortilla de trigo , mix de verdes, palta, zanahoria, pollo y pate de remolacha y semillas.", true, null, "Wrap frio", 14, 1000m },
                    { 83, null, "Tortilla de trigo calentita, pollo, queso tybo, cebolla caramelizada, zanahoria, pimiento rojo y verde, con mostaza Pauza (mostaza, curry y miel).", true, null, "Wrap calentito", 14, 1000m },
                    { 84, null, "Lechuga manteca, tomates cherry confitados, jamon cocido y queso tybo en tiras, crutones y pollo al horno con salsa césar", true, null, "Cesar", 15, 1000m },
                    { 85, null, "Mix de verdes, zanahoria, pepino, cherry confitados, crutones, brotes de lenteja, acompañado con vinagreta de yogurt natural", true, null, "Pauza", 15, 1000m },
                    { 86, null, "", true, null, "Limonada menta y jengibre 1/2L.", 16, 1000m },
                    { 87, null, "", true, null, "Jugo de naranja exprimido", 16, 1000m },
                    { 88, null, "", true, null, "Gaseosa lata 330 cc", 17, 1000m },
                    { 89, null, "", true, null, "Agua mineal 500 cc", 17, 1000m },
                    { 90, null, "", true, null, "Agua mineral con gas 500 cc", 17, 1000m },
                    { 91, null, "", true, null, "Stella Artois 330 cc", 17, 1000m },
                    { 92, null, "", true, null, "Sidra Peer", 17, 1000m },
                    { 93, null, "", true, null, "Cinzano Rosso/Bianco", 18, 0m },
                    { 94, null, "", true, null, "Lunfa", 18, 0m },
                    { 95, null, "", true, null, "Lunfa Verbena", 18, 0m },
                    { 96, null, "", true, null, "La Fuerza Rojo/Blanco", 18, 0m },
                    { 97, null, "", true, null, "Brokers", 19, 0m },
                    { 98, null, "", true, null, "Tanqueray", 19, 0m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CompanyIds", "Email", "Enabled", "Password", "PasswordResetExpiresAt", "PasswordResetToken", "Role", "Username" },
                values: new object[] { 1, null, "root@edimus.com", true, "$2a$11$RKOjo9jGUALtqDkaX52cj.7x8kcEj4mG4ESnP6i6Da9zc9DLE8wx.", null, null, 4, "DbSeeder" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CompanyId", "Enabled", "Name" },
                values: new object[,]
                {
                    { 1, 1, true, "Cafetería" },
                    { 2, 1, true, "Pastelería" },
                    { 3, 1, true, "Especialidades" },
                    { 4, 1, true, "Combos" },
                    { 5, 1, true, "Almuerzos" },
                    { 6, 1, true, "Bebidas" },
                    { 7, 1, true, "Coctelería" }
                });

            migrationBuilder.InsertData(
                table: "Premises",
                columns: new[] { "PremiseId", "CompanyId", "Enabled", "Name" },
                values: new object[] { 1, 1, true, "Barrio Jardin" });

            migrationBuilder.InsertData(
                table: "Layouts",
                columns: new[] { "LayoutId", "Enabled", "Height", "Name", "PremiseId", "Width" },
                values: new object[] { 1, true, 24, "Planta Baja", 1, 24 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Enabled", "ImageId", "Name", "ParentId", "Price" },
                values: new object[,]
                {
                    { 1, 1, "(de corte origen brasil)", true, null, "Café", null, 0m },
                    { 2, 1, "(consultar origen)", true, null, "Especial del día", null, 1000m },
                    { 3, 1, "", true, null, "Frío", null, 0m },
                    { 4, 2, "", true, null, "Croissant", null, 0m },
                    { 5, 2, "", true, null, "Budines", null, 0m },
                    { 6, 2, "", true, null, "Galletas", null, 0m },
                    { 7, 2, "", true, null, "Sin TACC", null, 0m },
                    { 8, 3, "", true, null, "Sandwiches", null, 0m },
                    { 9, 3, "", true, null, "Tostones", null, 0m },
                    { 10, 4, "Infusion, tostadas de pan de campo y pan de molde, jamon cocido, queso tybo, huevo revuelto con semillas, queso crema y un bowl con yogurt natural, miel y granola", true, null, "Completo", null, 1000m },
                    { 11, 4, "Infusión tostadas de pan de molde, pulpa de tomate y oliva y bowl con yogurt natural, miel, frutas de estación y granola.", true, null, "Lightweight", null, 1000m },
                    { 12, 4, "Infusión,panqueques de avena y banana con miel, frutos secos y frutas de estación", true, null, "Baby breakfast", null, 1000m },
                    { 13, 5, "", true, null, "Tartas", null, 0m },
                    { 14, 5, "", true, null, "Wrap", null, 0m },
                    { 15, 5, "", true, null, "Ensaladas", null, 0m },
                    { 16, 6, "", true, null, "Naturales", null, 0m },
                    { 17, 6, "", true, null, "Otras bebidas", null, 0m },
                    { 18, 7, "(Aperitivo a base de hierbas y especias acompañado con dash de soda, hielo y rodaja de naranja)", true, null, "Vermút", null, 1000m },
                    { 19, 7, "(Destilado inglés acompañado con tónica, hielo y rodaja de limón)", true, null, "Gin tonic", null, 1000m },
                    { 20, 7, "(Aperol, vino espumante extra brut, dash de soda y rodaja de naranja)", true, null, "Aperol Spritz", null, 1000m },
                    { 21, 7, "(Vermút, gin, campari y rodaja de naranja)", true, null, "Negroni", null, 1000m }
                });

            migrationBuilder.InsertData(
                table: "LayoutCoords",
                columns: new[] { "LayoutId", "X", "Y", "Type" },
                values: new object[,]
                {
                    { 1, 0, 3, 0 },
                    { 1, 0, 7, 0 },
                    { 1, 0, 15, 0 },
                    { 1, 1, 3, 0 },
                    { 1, 1, 7, 0 },
                    { 1, 1, 15, 0 },
                    { 1, 2, 3, 0 },
                    { 1, 2, 7, 0 },
                    { 1, 2, 15, 0 },
                    { 1, 3, 0, 0 },
                    { 1, 3, 2, 0 },
                    { 1, 3, 3, 0 },
                    { 1, 3, 7, 0 },
                    { 1, 3, 15, 1 },
                    { 1, 4, 3, 0 },
                    { 1, 4, 7, 0 },
                    { 1, 4, 15, 0 },
                    { 1, 5, 3, 1 },
                    { 1, 5, 7, 0 },
                    { 1, 5, 8, 0 },
                    { 1, 5, 9, 1 },
                    { 1, 5, 10, 0 },
                    { 1, 5, 11, 0 },
                    { 1, 5, 12, 0 },
                    { 1, 5, 13, 0 },
                    { 1, 5, 14, 0 },
                    { 1, 5, 15, 0 },
                    { 1, 6, 3, 1 },
                    { 1, 6, 15, 0 },
                    { 1, 7, 3, 0 },
                    { 1, 7, 15, 1 },
                    { 1, 8, 3, 0 },
                    { 1, 8, 15, 0 },
                    { 1, 9, 3, 0 },
                    { 1, 9, 15, 0 },
                    { 1, 10, 3, 0 },
                    { 1, 10, 15, 0 },
                    { 1, 11, 3, 0 },
                    { 1, 11, 4, 0 },
                    { 1, 11, 5, 1 },
                    { 1, 11, 6, 0 },
                    { 1, 11, 7, 0 },
                    { 1, 11, 8, 0 },
                    { 1, 11, 9, 0 },
                    { 1, 11, 10, 0 },
                    { 1, 11, 11, 0 },
                    { 1, 11, 12, 0 },
                    { 1, 11, 13, 0 },
                    { 1, 11, 14, 0 },
                    { 1, 11, 15, 0 },
                    { 1, 12, 3, 0 },
                    { 1, 12, 15, 0 },
                    { 1, 13, 3, 0 },
                    { 1, 13, 15, 0 },
                    { 1, 14, 3, 0 },
                    { 1, 14, 15, 0 },
                    { 1, 15, 3, 0 },
                    { 1, 15, 15, 0 }
                });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "SectorId", "Color", "Enabled", "LayoutId", "Name", "PositionX", "PositionY" },
                values: new object[,]
                {
                    { 1, "#EE82EE", true, 1, "Deck", 0, 0 },
                    { 2, "#FFA208", true, 1, "Salon", 0, 4 },
                    { 3, "#FAC8C0", true, 1, "Patio Interno", 0, 8 },
                    { 4, "#00FFFF", true, 1, "Garden", 12, 4 },
                    { 5, "#00FF00", true, 1, "Patio Externo", 0, 16 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Enabled", "LayoutId", "PositionX", "PositionY", "QrId", "Status" },
                values: new object[,]
                {
                    { 1, true, 1, 0, 0, "6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b", 2 },
                    { 2, true, 1, 2, 2, "d4735e3a265e16eee03f59718b9b5d03019c07d8b6c51f90da3a666eec13ab35", 0 },
                    { 3, true, 1, 7, 0, "4e07408562bedb8b60ce05c1decfe3ad16b72230967de01f640b7e4729b49fce", 0 },
                    { 4, true, 1, 9, 0, "4b227777d4dd1fc61c6f884f48641d02b4d121d3fd328cb08b5531fcacdabf8a", 2 },
                    { 5, true, 1, 11, 0, "ef2d127de37b942baad06145e54b0c619a1f22327b2ebbcfbec78f5564afe39d", 1 },
                    { 6, true, 1, 13, 0, "e7f6c011776e8db7cd330b54174fd76f7d0216b612387a5ffcfb81e6f0919683", 2 },
                    { 7, true, 1, 15, 1, "7902699be42c8a8e46fbbb4501726517e86b22c56a189f7625a6da49081b2451", 1 }
                });

            migrationBuilder.InsertData(
                table: "SectorCoords",
                columns: new[] { "SectorId", "X", "Y" },
                values: new object[,]
                {
                    { 1, 0, 0 },
                    { 2, 0, 0 },
                    { 3, 0, 0 },
                    { 4, 0, 0 },
                    { 5, 0, 0 },
                    { 1, 0, 1 },
                    { 2, 0, 1 },
                    { 3, 0, 1 },
                    { 4, 0, 1 },
                    { 5, 0, 1 },
                    { 1, 0, 2 },
                    { 2, 0, 2 },
                    { 3, 0, 2 },
                    { 4, 0, 2 },
                    { 5, 0, 2 },
                    { 3, 0, 3 },
                    { 4, 0, 3 },
                    { 5, 0, 3 },
                    { 3, 0, 4 },
                    { 4, 0, 4 },
                    { 3, 0, 5 },
                    { 4, 0, 5 },
                    { 3, 0, 6 },
                    { 4, 0, 6 },
                    { 4, 0, 7 },
                    { 4, 0, 8 },
                    { 4, 0, 9 },
                    { 4, 0, 10 },
                    { 1, 1, 0 },
                    { 2, 1, 0 },
                    { 3, 1, 0 },
                    { 4, 1, 0 },
                    { 5, 1, 0 },
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 1, 1 },
                    { 4, 1, 1 },
                    { 5, 1, 1 },
                    { 1, 1, 2 },
                    { 2, 1, 2 },
                    { 3, 1, 2 },
                    { 4, 1, 2 },
                    { 5, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 3 },
                    { 5, 1, 3 },
                    { 3, 1, 4 },
                    { 4, 1, 4 },
                    { 3, 1, 5 },
                    { 4, 1, 5 },
                    { 3, 1, 6 },
                    { 4, 1, 6 },
                    { 4, 1, 7 },
                    { 4, 1, 8 },
                    { 4, 1, 9 },
                    { 4, 1, 10 },
                    { 1, 2, 0 },
                    { 2, 2, 0 },
                    { 3, 2, 0 },
                    { 4, 2, 0 },
                    { 5, 2, 0 },
                    { 1, 2, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 1 },
                    { 4, 2, 1 },
                    { 5, 2, 1 },
                    { 1, 2, 2 },
                    { 2, 2, 2 },
                    { 3, 2, 2 },
                    { 4, 2, 2 },
                    { 5, 2, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 3 },
                    { 5, 2, 3 },
                    { 3, 2, 4 },
                    { 4, 2, 4 },
                    { 3, 2, 5 },
                    { 4, 2, 5 },
                    { 3, 2, 6 },
                    { 4, 2, 6 },
                    { 4, 2, 7 },
                    { 4, 2, 8 },
                    { 4, 2, 9 },
                    { 4, 2, 10 },
                    { 2, 3, 0 },
                    { 3, 3, 0 },
                    { 4, 3, 0 },
                    { 5, 3, 0 },
                    { 1, 3, 1 },
                    { 2, 3, 1 },
                    { 3, 3, 1 },
                    { 4, 3, 1 },
                    { 5, 3, 1 },
                    { 2, 3, 2 },
                    { 3, 3, 2 },
                    { 4, 3, 2 },
                    { 5, 3, 2 },
                    { 3, 3, 3 },
                    { 4, 3, 3 },
                    { 5, 3, 3 },
                    { 3, 3, 4 },
                    { 4, 3, 4 },
                    { 3, 3, 5 },
                    { 4, 3, 5 },
                    { 3, 3, 6 },
                    { 4, 3, 6 },
                    { 4, 3, 7 },
                    { 4, 3, 8 },
                    { 4, 3, 9 },
                    { 4, 3, 10 },
                    { 1, 4, 0 },
                    { 2, 4, 0 },
                    { 3, 4, 0 },
                    { 5, 4, 0 },
                    { 1, 4, 1 },
                    { 2, 4, 1 },
                    { 3, 4, 1 },
                    { 5, 4, 1 },
                    { 1, 4, 2 },
                    { 2, 4, 2 },
                    { 3, 4, 2 },
                    { 5, 4, 2 },
                    { 3, 4, 3 },
                    { 5, 4, 3 },
                    { 3, 4, 4 },
                    { 3, 4, 5 },
                    { 3, 4, 6 },
                    { 1, 5, 0 },
                    { 2, 5, 0 },
                    { 5, 5, 0 },
                    { 1, 5, 1 },
                    { 2, 5, 1 },
                    { 5, 5, 1 },
                    { 1, 5, 2 },
                    { 2, 5, 2 },
                    { 5, 5, 2 },
                    { 5, 5, 3 },
                    { 1, 6, 0 },
                    { 2, 6, 0 },
                    { 5, 6, 0 },
                    { 1, 6, 1 },
                    { 2, 6, 1 },
                    { 5, 6, 1 },
                    { 1, 6, 2 },
                    { 2, 6, 2 },
                    { 5, 6, 2 },
                    { 2, 6, 3 },
                    { 5, 6, 3 },
                    { 2, 6, 4 },
                    { 2, 6, 5 },
                    { 2, 6, 6 },
                    { 2, 6, 7 },
                    { 2, 6, 8 },
                    { 2, 6, 9 },
                    { 2, 6, 10 },
                    { 1, 7, 0 },
                    { 2, 7, 0 },
                    { 5, 7, 0 },
                    { 1, 7, 1 },
                    { 2, 7, 1 },
                    { 5, 7, 1 },
                    { 1, 7, 2 },
                    { 2, 7, 2 },
                    { 5, 7, 2 },
                    { 2, 7, 3 },
                    { 5, 7, 3 },
                    { 2, 7, 4 },
                    { 2, 7, 5 },
                    { 2, 7, 6 },
                    { 2, 7, 7 },
                    { 2, 7, 8 },
                    { 2, 7, 9 },
                    { 2, 7, 10 },
                    { 1, 8, 0 },
                    { 2, 8, 0 },
                    { 5, 8, 0 },
                    { 1, 8, 1 },
                    { 2, 8, 1 },
                    { 5, 8, 1 },
                    { 1, 8, 2 },
                    { 2, 8, 2 },
                    { 5, 8, 2 },
                    { 2, 8, 3 },
                    { 5, 8, 3 },
                    { 2, 8, 4 },
                    { 2, 8, 5 },
                    { 2, 8, 6 },
                    { 2, 8, 7 },
                    { 2, 8, 8 },
                    { 2, 8, 9 },
                    { 2, 8, 10 },
                    { 1, 9, 0 },
                    { 2, 9, 0 },
                    { 5, 9, 0 },
                    { 1, 9, 1 },
                    { 2, 9, 1 },
                    { 5, 9, 1 },
                    { 1, 9, 2 },
                    { 2, 9, 2 },
                    { 5, 9, 2 },
                    { 2, 9, 3 },
                    { 5, 9, 3 },
                    { 2, 9, 4 },
                    { 2, 9, 5 },
                    { 2, 9, 6 },
                    { 2, 9, 7 },
                    { 2, 9, 8 },
                    { 2, 9, 9 },
                    { 2, 9, 10 },
                    { 1, 10, 0 },
                    { 2, 10, 0 },
                    { 5, 10, 0 },
                    { 1, 10, 1 },
                    { 2, 10, 1 },
                    { 5, 10, 1 },
                    { 1, 10, 2 },
                    { 2, 10, 2 },
                    { 5, 10, 2 },
                    { 2, 10, 3 },
                    { 5, 10, 3 },
                    { 2, 10, 4 },
                    { 2, 10, 5 },
                    { 2, 10, 6 },
                    { 2, 10, 7 },
                    { 2, 10, 8 },
                    { 2, 10, 9 },
                    { 2, 10, 10 },
                    { 1, 11, 0 },
                    { 5, 11, 0 },
                    { 1, 11, 1 },
                    { 5, 11, 1 },
                    { 1, 11, 2 },
                    { 5, 11, 2 },
                    { 5, 11, 3 },
                    { 1, 12, 0 },
                    { 5, 12, 0 },
                    { 1, 12, 1 },
                    { 5, 12, 1 },
                    { 1, 12, 2 },
                    { 5, 12, 2 },
                    { 5, 12, 3 },
                    { 1, 13, 0 },
                    { 5, 13, 0 },
                    { 1, 13, 1 },
                    { 5, 13, 1 },
                    { 1, 13, 2 },
                    { 5, 13, 2 },
                    { 5, 13, 3 },
                    { 1, 14, 0 },
                    { 5, 14, 0 },
                    { 1, 14, 1 },
                    { 5, 14, 1 },
                    { 1, 14, 2 },
                    { 5, 14, 2 },
                    { 5, 14, 3 },
                    { 1, 15, 0 },
                    { 5, 15, 0 },
                    { 1, 15, 1 },
                    { 5, 15, 1 },
                    { 1, 15, 2 },
                    { 5, 15, 2 },
                    { 5, 15, 3 }
                });

            migrationBuilder.InsertData(
                table: "TableCoords",
                columns: new[] { "TableId", "X", "Y" },
                values: new object[,]
                {
                    { 1, 0, 0 },
                    { 2, 0, 0 },
                    { 3, 0, 0 },
                    { 4, 0, 0 },
                    { 5, 0, 0 },
                    { 6, 0, 0 },
                    { 7, 0, 0 },
                    { 3, 0, 1 },
                    { 4, 0, 1 },
                    { 6, 0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_IngredientId",
                table: "Consumptions",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LayoutCoords_LayoutId",
                table: "LayoutCoords",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_PremiseId",
                table: "Layouts",
                column: "PremiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Premises_CompanyId",
                table: "Premises",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagsId",
                table: "ProductTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_SectorCoords_SectorId",
                table: "SectorCoords",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_LayoutId",
                table: "Sectors",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_TableCoords_TableId",
                table: "TableCoords",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_LayoutId",
                table: "Tables",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumptions");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "LayoutCoords");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "SectorCoords");

            migrationBuilder.DropTable(
                name: "TableCoords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Layouts");

            migrationBuilder.DropTable(
                name: "Premises");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
