﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viajes.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionContacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sentimiento",
                table: "t_contacto",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentimiento",
                table: "t_contacto");
        }
    }
}
