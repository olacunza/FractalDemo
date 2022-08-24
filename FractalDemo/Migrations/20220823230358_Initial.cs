using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FractalDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Categoria_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Categoria_Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Orden_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOrden = table.Column<int>(type: "int", nullable: false),
                    EstadoPedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    MontoInicial = table.Column<double>(type: "float", nullable: false),
                    IGV = table.Column<double>(type: "float", nullable: false),
                    MontoFinal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Orden_Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Producto_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioUnitario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoProducto = table.Column<bool>(type: "bit", nullable: false),
                    Categoria_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Producto_Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_Categoria_Id",
                        column: x => x.Categoria_Id,
                        principalTable: "Categorias",
                        principalColumn: "Categoria_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenProducto",
                columns: table => new
                {
                    Orden_Id = table.Column<int>(type: "int", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenProducto", x => new { x.Orden_Id, x.Producto_Id });
                    table.ForeignKey(
                        name: "FK_OrdenProducto_Ordenes_Orden_Id",
                        column: x => x.Orden_Id,
                        principalTable: "Ordenes",
                        principalColumn: "Orden_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenProducto_Productos_Producto_Id",
                        column: x => x.Producto_Id,
                        principalTable: "Productos",
                        principalColumn: "Producto_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenProducto_Producto_Id",
                table: "OrdenProducto",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Categoria_Id",
                table: "Productos",
                column: "Categoria_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenProducto");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
