using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlquilerCanchas.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoReserva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true),
                    ClaseCss = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoReserva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCancha",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCancha", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: false),
                    horaInicio = table.Column<int>(nullable: false),
                    horaFin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rol = table.Column<int>(nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: false),
                    Contrasenia = table.Column<byte[]>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Dni = table.Column<string>(maxLength: 100, nullable: false),
                    FechaDeNacimineto = table.Column<DateTime>(nullable: false),
                    Telefono = table.Column<string>(maxLength: 100, nullable: false),
                    FechaUltimoAcceso = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cancha",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false),
                    TipoCanchaId = table.Column<int>(nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    ClubId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cancha_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cancha_TipoCancha_TipoCanchaId",
                        column: x => x.TipoCanchaId,
                        principalTable: "TipoCancha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CanchaId = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    FechaReserva = table.Column<DateTime>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    Comentarios = table.Column<string>(maxLength: 50, nullable: true),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserva_Cancha_CanchaId",
                        column: x => x.CanchaId,
                        principalTable: "Cancha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_EstadoReserva_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadoReserva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurnoCancha",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CanchaId = table.Column<int>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoCancha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurnoCancha_Cancha_CanchaId",
                        column: x => x.CanchaId,
                        principalTable: "Cancha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnoCancha_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cancha_ClubId",
                table: "Cancha",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Cancha_TipoCanchaId",
                table: "Cancha",
                column: "TipoCanchaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_CanchaId",
                table: "Reserva",
                column: "CanchaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_EstadoId",
                table: "Reserva",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_TurnoId",
                table: "Reserva",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoCancha_CanchaId",
                table: "TurnoCancha",
                column: "CanchaId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnoCancha_TurnoId",
                table: "TurnoCancha",
                column: "TurnoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "TurnoCancha");

            migrationBuilder.DropTable(
                name: "EstadoReserva");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cancha");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "TipoCancha");
        }
    }
}
