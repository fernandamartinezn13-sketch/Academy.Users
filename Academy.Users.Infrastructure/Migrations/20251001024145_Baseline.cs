using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Baseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Baseline: no se crean tablas ni se alteran objetos.
            // Esta migración solo sirve para alinear EF con la BD existente.
            // migrationBuilder.Sql("-- baseline"); // opcional
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Sin cambios reversibles (no-op).
        }
    }
}
