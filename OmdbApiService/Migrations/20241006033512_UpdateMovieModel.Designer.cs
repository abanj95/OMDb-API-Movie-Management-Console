﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OmdbApiService.Data;

#nullable disable

namespace OmdbApiService.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20241006033512_UpdateMovieModel")]
    partial class UpdateMovieModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OmdbApiService.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Awards")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImdbId")
                        .HasColumnType("longtext");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rated")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Released")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
