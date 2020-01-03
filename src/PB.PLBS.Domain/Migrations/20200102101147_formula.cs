using Microsoft.EntityFrameworkCore.Migrations;

namespace PB.PLBS.Domain.Migrations
{
    public partial class formula : Migration  
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formulas",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    ConcreteToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FormulaItems",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Enable = table.Column<bool>(nullable: false),
                    FormulaID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulaItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FormulaItems_Formulas_FormulaID",
                        column: x => x.FormulaID,
                        principalTable: "Formulas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormulaItems_FormulaID",
                table: "FormulaItems",
                column: "FormulaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormulaItems");

            migrationBuilder.DropTable(
                name: "Formulas");
        }
    }
}
