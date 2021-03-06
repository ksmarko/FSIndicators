﻿// <auto-generated />
using System;
using AspNetCoreAppPostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AspNetCoreAppPostgreSQL.Migrations
{
    [DbContext(typeof(TvShowsContext))]
    partial class TvShowsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AspNetCoreAppPostgreSQL.Models.ARR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<double>("Profit");

                    b.HasKey("Id");

                    b.ToTable("ARR");
                });

            modelBuilder.Entity("AspNetCoreAppPostgreSQL.Models.Investment", b =>
                {
                    b.Property<double>("Investments");

                    b.HasKey("Investments");

                    b.ToTable("Investments");
                });
#pragma warning restore 612, 618
        }
    }
}
