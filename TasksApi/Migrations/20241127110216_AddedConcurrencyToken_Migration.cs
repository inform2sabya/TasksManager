﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedConcurrencyToken_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Version",
                table: "Tasks",
                type: "TEXT",
                rowVersion: true,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Tasks");
        }
    }
}
