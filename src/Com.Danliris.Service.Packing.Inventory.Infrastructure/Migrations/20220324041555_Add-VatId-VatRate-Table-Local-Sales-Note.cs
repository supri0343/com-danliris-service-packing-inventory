﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Migrations
{
    public partial class AddVatIdVatRateTableLocalSalesNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VatId",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VatRate",
                table: "GarmentShippingLocalSalesNotes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatId",
                table: "GarmentShippingLocalSalesNotes");

            migrationBuilder.DropColumn(
                name: "VatRate",
                table: "GarmentShippingLocalSalesNotes");
        }
    }
}