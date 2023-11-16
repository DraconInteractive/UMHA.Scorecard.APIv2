﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScorecardAPI;

#nullable disable

namespace ScorecardAPI.Migrations
{
    [DbContext(typeof(ScorecardContext))]
    [Migration("20231112221735_TournamentsInitialMigration")]
    partial class TournamentsInitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("ScorecardAPI.Models.Club", b =>
                {
                    b.Property<int>("ClubId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("ClubId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("ScorecardAPI.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FighterOneId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FighterTwoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MatchId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ScorecardAPI.Models.MatchEvent", b =>
                {
                    b.Property<int>("MatchEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventType")
                        .HasColumnType("TEXT");

                    b.Property<int>("FighterOneReduction")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FighterTwoReduction")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatchId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MatchEventId");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchEvents");
                });

            modelBuilder.Entity("ScorecardAPI.Models.MatchPreset", b =>
                {
                    b.Property<int>("PresetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("PresetId");

                    b.ToTable("MatchPresets");
                });

            modelBuilder.Entity("ScorecardAPI.Models.Tournament", b =>
                {
                    b.Property<int>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("TournamentId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("ScorecardAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScorecardAPI.Models.MatchEvent", b =>
                {
                    b.HasOne("ScorecardAPI.Models.Match", "Match")
                        .WithMany("Events")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("ScorecardAPI.Models.Match", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
