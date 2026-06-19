using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Shared.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Migration001_InitialSchema : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AuditLogs",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                EntityId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                EntityType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Operation = table.Column<int>(type: "integer", nullable: false),
                UserId = table.Column<int>(type: "integer", nullable: false),
                Username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditLogs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Companies",
            columns: table => new
            {
                CompanyId = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                Slogan = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                Slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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
            name: "Images",
            columns: table => new
            {
                ImageId = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                BLOB = table.Column<byte[]>(type: "bytea", nullable: false),
                Enabled = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Images", x => x.ImageId);
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
            name: "AuditLogChange",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                AuditLogId = table.Column<long>(type: "bigint", nullable: false),
                PropertyName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                OldValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                NewValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditLogChange", x => x.Id);
                table.ForeignKey(
                    name: "FK_AuditLogChange_AuditLogs_AuditLogId",
                    column: x => x.AuditLogId,
                    principalTable: "AuditLogs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
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
            name: "Ingredients",
            columns: table => new
            {
                IngredientId = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CompanyId = table.Column<int>(type: "integer", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Stock = table.Column<decimal>(type: "numeric", nullable: false),
                Alert = table.Column<int>(type: "integer", nullable: false),
                Unit = table.Column<int>(type: "integer", nullable: false),
                Enabled = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                table.ForeignKey(
                    name: "FK_Ingredients_Companies_CompanyId",
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
            name: "Tags",
            columns: table => new
            {
                TagId = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CompanyId = table.Column<int>(type: "integer", nullable: false),
                Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                Enabled = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.TagId);
                table.ForeignKey(
                    name: "FK_Tags_Companies_CompanyId",
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
                CompanyId = table.Column<int>(type: "integer", nullable: false),
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
                table.ForeignKey(
                    name: "FK_Products_Companies_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "Companies",
                    principalColumn: "CompanyId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Products_Images_ImageId",
                    column: x => x.ImageId,
                    principalTable: "Images",
                    principalColumn: "ImageId");
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
                ArrivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                CalledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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

        migrationBuilder.CreateTable(
            name: "TableSessions",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                TableId = table.Column<int>(type: "integer", nullable: false),
                OpenedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ClosedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                ArrivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                ArrivalAttentionSeconds = table.Column<int>(type: "integer", nullable: true),
                TotalCallingSeconds = table.Column<int>(type: "integer", nullable: false),
                CallingCount = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TableSessions", x => x.Id);
                table.ForeignKey(
                    name: "FK_TableSessions_Tables_TableId",
                    column: x => x.TableId,
                    principalTable: "Tables",
                    principalColumn: "TableId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SessionOrders",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SessionId = table.Column<long>(type: "bigint", nullable: false),
                ProductId = table.Column<int>(type: "integer", nullable: false),
                ProductName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                Amount = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SessionOrders", x => x.Id);
                table.ForeignKey(
                    name: "FK_SessionOrders_TableSessions_SessionId",
                    column: x => x.SessionId,
                    principalTable: "TableSessions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AuditLogChange_AuditLogId",
            table: "AuditLogChange",
            column: "AuditLogId");

        migrationBuilder.CreateIndex(
            name: "IX_Categories_CompanyId",
            table: "Categories",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_Companies_Slug",
            table: "Companies",
            column: "Slug",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Consumptions_IngredientId",
            table: "Consumptions",
            column: "IngredientId");

        migrationBuilder.CreateIndex(
            name: "IX_Ingredients_CompanyId",
            table: "Ingredients",
            column: "CompanyId");

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
            name: "IX_Products_CompanyId",
            table: "Products",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_ImageId",
            table: "Products",
            column: "ImageId",
            unique: true);

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
            name: "IX_SessionOrders_SessionId",
            table: "SessionOrders",
            column: "SessionId");

        migrationBuilder.CreateIndex(
            name: "IX_TableCoords_TableId",
            table: "TableCoords",
            column: "TableId");

        migrationBuilder.CreateIndex(
            name: "IX_Tables_LayoutId",
            table: "Tables",
            column: "LayoutId");

        migrationBuilder.CreateIndex(
            name: "IX_TableSessions_TableId",
            table: "TableSessions",
            column: "TableId");

        migrationBuilder.CreateIndex(
            name: "IX_Tags_CompanyId",
            table: "Tags",
            column: "CompanyId");

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
            name: "AuditLogChange");

        migrationBuilder.DropTable(
            name: "Consumptions");

        migrationBuilder.DropTable(
            name: "LayoutCoords");

        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "ProductTag");

        migrationBuilder.DropTable(
            name: "SectorCoords");

        migrationBuilder.DropTable(
            name: "SessionOrders");

        migrationBuilder.DropTable(
            name: "TableCoords");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "AuditLogs");

        migrationBuilder.DropTable(
            name: "Ingredients");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "Tags");

        migrationBuilder.DropTable(
            name: "Sectors");

        migrationBuilder.DropTable(
            name: "TableSessions");

        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropTable(
            name: "Images");

        migrationBuilder.DropTable(
            name: "Tables");

        migrationBuilder.DropTable(
            name: "Layouts");

        migrationBuilder.DropTable(
            name: "Premises");

        migrationBuilder.DropTable(
            name: "Companies");
    }
}
