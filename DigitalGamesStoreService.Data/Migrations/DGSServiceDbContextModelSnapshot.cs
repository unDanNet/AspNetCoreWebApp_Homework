﻿// <auto-generated />
using System;
using DigitalGamesStoreService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DigitalGamesStoreService.Data.Migrations
{
    [DbContext(typeof(DGSServiceDbContext))]
    partial class DGSServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DigitalGamesStoreService.Data.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("DeveloperName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.OwnedGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<float>("HoursPlayed")
                        .HasColumnType("real");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("OwnedGame");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("money");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.UserPublicProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CurrentlyPlayedGameId")
                        .HasColumnType("integer");

                    b.Property<string>("Nickname")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("ProfileDescription")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrentlyPlayedGameId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.UserSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime?>("ExpiredAt")
                        .HasColumnType("timestamptz");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastRequestedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("SessionToken")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSession");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.OwnedGame", b =>
                {
                    b.HasOne("DigitalGamesStoreService.Data.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigitalGamesStoreService.Data.User", null)
                        .WithMany("OwnedGames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.UserPublicProfile", b =>
                {
                    b.HasOne("DigitalGamesStoreService.Data.Game", "CurrentlyPlayedGame")
                        .WithMany()
                        .HasForeignKey("CurrentlyPlayedGameId");

                    b.HasOne("DigitalGamesStoreService.Data.User", null)
                        .WithOne("UserPublicProfile")
                        .HasForeignKey("DigitalGamesStoreService.Data.UserPublicProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentlyPlayedGame");
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.UserSession", b =>
                {
                    b.HasOne("DigitalGamesStoreService.Data.User", null)
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalGamesStoreService.Data.User", b =>
                {
                    b.Navigation("OwnedGames");

                    b.Navigation("Sessions");

                    b.Navigation("UserPublicProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
