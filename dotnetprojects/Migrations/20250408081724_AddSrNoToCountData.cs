using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetprojects.Migrations
{
    /// <inheritdoc />
    public partial class AddSrNoToCountData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountData",
                columns: table => new
                {
                    SrNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraId = table.Column<int>(type: "int", nullable: false),
                    CameraName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    In = table.Column<int>(type: "int", nullable: false),
                    Out = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountData", x => x.SrNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountData");
        }
    }
}
