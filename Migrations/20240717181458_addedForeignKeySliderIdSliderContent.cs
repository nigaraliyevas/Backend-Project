using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Migrations
{
    /// <inheritdoc />
    public partial class addedForeignKeySliderIdSliderContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "SliderContent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SliderContent_SliderId",
                table: "SliderContent",
                column: "SliderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SliderContent_Sliders_SliderId",
                table: "SliderContent",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SliderContent_Sliders_SliderId",
                table: "SliderContent");

            migrationBuilder.DropIndex(
                name: "IX_SliderContent_SliderId",
                table: "SliderContent");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "SliderContent");
        }
    }
}
