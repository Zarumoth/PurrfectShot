using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrfectShot.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesWithComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Votes",
                comment: "Represents a single rating given to a photo.");

            migrationBuilder.AlterTable(
                name: "Photos",
                comment: "Represents an uploaded image of a cat.");

            migrationBuilder.AlterTable(
                name: "Cats",
                comment: "Represents a cat housemate in the system.");

            migrationBuilder.AlterColumn<string>(
                name: "VoterName",
                table: "Votes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "The name of the user who cast the vote.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Stars",
                table: "Votes",
                type: "int",
                nullable: false,
                comment: "The number of stars awarded (from 1 to 5).",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "PhotoId",
                table: "Votes",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Foreign key referencing the rated photo.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Votes",
                type: "int",
                nullable: false,
                comment: "Unique identifier for the vote entry.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Photos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "The relative server path where the physical image file is stored.",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUploaded",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                comment: "The exact date and time when the photo was uploaded.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "Photos",
                type: "int",
                nullable: false,
                comment: "Foreign key referencing the cat shown in the photo.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Photos",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "A descriptive text or story accompanying the photo.",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Unique identifier for the photo entry.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "The name of the cat.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "MainPhotoId",
                table: "Cats",
                type: "uniqueidentifier",
                nullable: true,
                comment: "The unique identifier of the photo chosen as the cat's profile picture.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "The ID of the photo selected as the main profile picture for the cat.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Cats",
                type: "bit",
                nullable: false,
                comment: "Flag indicating if the cat is active or archived (soft-deleted).",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicates if the cat is still an active member of the household gallery.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cats",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "A short biography or description of the cat's personality.",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Breed",
                table: "Cats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "The specific breed of the cat.",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cats",
                type: "int",
                nullable: false,
                comment: "Unique identifier for the cat.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Votes",
                oldComment: "Represents a single rating given to a photo.");

            migrationBuilder.AlterTable(
                name: "Photos",
                oldComment: "Represents an uploaded image of a cat.");

            migrationBuilder.AlterTable(
                name: "Cats",
                oldComment: "Represents a cat housemate in the system.");

            migrationBuilder.AlterColumn<string>(
                name: "VoterName",
                table: "Votes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "The name of the user who cast the vote.");

            migrationBuilder.AlterColumn<int>(
                name: "Stars",
                table: "Votes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The number of stars awarded (from 1 to 5).");

            migrationBuilder.AlterColumn<Guid>(
                name: "PhotoId",
                table: "Votes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Foreign key referencing the rated photo.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Votes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Unique identifier for the vote entry.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Photos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "The relative server path where the physical image file is stored.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUploaded",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The exact date and time when the photo was uploaded.");

            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key referencing the cat shown in the photo.");

            migrationBuilder.AlterColumn<string>(
                name: "Caption",
                table: "Photos",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "A descriptive text or story accompanying the photo.");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Unique identifier for the photo entry.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "The name of the cat.");

            migrationBuilder.AlterColumn<Guid>(
                name: "MainPhotoId",
                table: "Cats",
                type: "uniqueidentifier",
                nullable: true,
                comment: "The ID of the photo selected as the main profile picture for the cat.",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "The unique identifier of the photo chosen as the cat's profile picture.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Cats",
                type: "bit",
                nullable: false,
                comment: "Indicates if the cat is still an active member of the household gallery.",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Flag indicating if the cat is active or archived (soft-deleted).");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Cats",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "A short biography or description of the cat's personality.");

            migrationBuilder.AlterColumn<string>(
                name: "Breed",
                table: "Cats",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "The specific breed of the cat.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cats",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Unique identifier for the cat.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
