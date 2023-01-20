﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParksApi.Models;

#nullable disable

namespace ParksApi.Migrations
{
    [DbContext(typeof(ParksApiContext))]
    [Migration("20230120191352_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ParksApi.Models.Park", b =>
                {
                    b.Property<int>("ParkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("ParkId");

                    b.HasIndex("StateId");

                    b.ToTable("Parks");

                    b.HasData(
                        new
                        {
                            ParkId = 1,
                            Description = "A high elevation lake in the crater of an exploded volcano",
                            Name = "Crater Lake",
                            StateId = 1
                        },
                        new
                        {
                            ParkId = 2,
                            Description = "A forested valley high in the Sierra Nevada mountains, enclosed by towering granite cliffs with cascading waterfalls",
                            Name = "Yosemite",
                            StateId = 3
                        },
                        new
                        {
                            ParkId = 3,
                            Description = "A 14,410 active volcano with extensive (but shrinking) glacier fields. Beloved for alpine meadows carpeted in summer wildflowers",
                            Name = "Mount Rainier",
                            StateId = 2
                        });
                });

            modelBuilder.Entity("ParksApi.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("StateId");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            StateId = 1,
                            Name = "Oregon"
                        },
                        new
                        {
                            StateId = 2,
                            Name = "Washington"
                        },
                        new
                        {
                            StateId = 3,
                            Name = "California"
                        });
                });

            modelBuilder.Entity("ParksApi.Models.Park", b =>
                {
                    b.HasOne("ParksApi.Models.State", "State")
                        .WithMany("Parks")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("ParksApi.Models.State", b =>
                {
                    b.Navigation("Parks");
                });
#pragma warning restore 612, 618
        }
    }
}
