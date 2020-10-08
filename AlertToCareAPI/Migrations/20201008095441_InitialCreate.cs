using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlertToCareAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ICUs",
                columns: table => new
                {
                    IcuId = table.Column<Guid>(nullable: false),
                    Layout = table.Column<string>(nullable: true),
                    BedsCount = table.Column<int>(nullable: false),
                    BedIdPrefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICUs", x => x.IcuId);
                });

            migrationBuilder.CreateTable(
                name: "PatientAddress",
                columns: table => new
                {
                    Mrn = table.Column<Guid>(nullable: false),
                    HouseNo = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Pincode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAddress", x => x.Mrn);
                });

            migrationBuilder.CreateTable(
                name: "Vitals",
                columns: table => new
                {
                    Mrn = table.Column<Guid>(nullable: false),
                    Bpm = table.Column<float>(nullable: false),
                    Spo2 = table.Column<float>(nullable: false),
                    RespRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.Mrn);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Mrn = table.Column<string>(nullable: false),
                    PatientName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    ContactNo = table.Column<string>(nullable: true),
                    BedId = table.Column<string>(nullable: true),
                    ICUId = table.Column<Guid>(nullable: false),
                    VitalsMrn = table.Column<Guid>(nullable: true),
                    AddressMrn = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Mrn);
                    table.ForeignKey(
                        name: "FK_Patients_PatientAddress_AddressMrn",
                        column: x => x.AddressMrn,
                        principalTable: "PatientAddress",
                        principalColumn: "Mrn",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_ICUs_ICUId",
                        column: x => x.ICUId,
                        principalTable: "ICUs",
                        principalColumn: "IcuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Vitals_VitalsMrn",
                        column: x => x.VitalsMrn,
                        principalTable: "Vitals",
                        principalColumn: "Mrn",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AddressMrn",
                table: "Patients",
                column: "AddressMrn");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ICUId",
                table: "Patients",
                column: "ICUId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_VitalsMrn",
                table: "Patients",
                column: "VitalsMrn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "PatientAddress");

            migrationBuilder.DropTable(
                name: "ICUs");

            migrationBuilder.DropTable(
                name: "Vitals");
        }
    }
}
