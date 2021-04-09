using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskEF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleStamp = table.Column<int>(type: "int", nullable: false),
                    NewStamp = table.Column<int>(type: "int", nullable: false),
                    OldStamp = table.Column<int>(type: "int", nullable: false),
                    AltNewStamp = table.Column<int>(type: "int", nullable: false),
                    AltOldStamp = table.Column<int>(type: "int", nullable: false),
                    NextDept = table.Column<int>(type: "int", nullable: false),
                    AltNextDept = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stamps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stamps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lists_Depts_DeptModelId",
                        column: x => x.DeptModelId,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListModelStampModel",
                columns: table => new
                {
                    ListsId = table.Column<int>(type: "int", nullable: false),
                    StampsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListModelStampModel", x => new { x.ListsId, x.StampsId });
                    table.ForeignKey(
                        name: "FK_ListModelStampModel_Lists_ListsId",
                        column: x => x.ListsId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListModelStampModel_Stamps_StampsId",
                        column: x => x.StampsId,
                        principalTable: "Stamps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListModelStampModel_StampsId",
                table: "ListModelStampModel",
                column: "StampsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_DeptModelId",
                table: "Lists",
                column: "DeptModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListModelStampModel");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "Stamps");

            migrationBuilder.DropTable(
                name: "Depts");
        }
    }
}
