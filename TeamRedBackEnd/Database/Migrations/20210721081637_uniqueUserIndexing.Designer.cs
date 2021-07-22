﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamRedBackEnd.Database;

namespace TeamRedBackEnd.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210721081637_uniqueUserIndexing")]
    partial class uniqueUserIndexing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("group_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("HabitID")
                        .HasColumnType("integer")
                        .HasColumnName("group_habit");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int[]>("UserIds")
                        .HasColumnType("integer[]")
                        .HasColumnName("group_user_ids");

                    b.HasKey("Id");

                    b.HasIndex("HabitID")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("group");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("habit_category");

                    b.Property<int>("ChosenWeekDays")
                        .HasColumnType("integer")
                        .HasColumnName("habit_weekdays");

                    b.Property<int>("DayRepeat")
                        .HasColumnType("integer")
                        .HasColumnName("habit_day_repeat");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("habit_description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("habit_end_date");

                    b.Property<bool>("Favorite")
                        .HasColumnType("boolean")
                        .HasColumnName("habit_favourite");

                    b.Property<int>("HabitId")
                        .HasColumnType("integer")
                        .HasColumnName("habit_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("habit_name");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("habit_owner_id");

                    b.Property<bool>("PublicHabit")
                        .HasColumnType("boolean")
                        .HasColumnName("habit_public_habit");

                    b.Property<string>("Reward")
                        .HasColumnType("text")
                        .HasColumnName("habit_reward");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("habit_start_date");

                    b.Property<int>("TimeSpan")
                        .HasColumnType("integer")
                        .HasColumnName("habit_timespan");

                    b.Property<int>("TimesTodo")
                        .HasColumnType("integer")
                        .HasColumnName("habit_times_todo");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("habit");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("history_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("HabitHistoryDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("history_date");

                    b.Property<int>("HabitHistoryNum")
                        .HasColumnType("integer")
                        .HasColumnName("history_result_num");

                    b.Property<bool>("HabitHistoryResult")
                        .HasColumnType("boolean")
                        .HasColumnName("history_result_bool");

                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.ToTable("history");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("BytePassword")
                        .HasColumnType("bytea");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("user_email");

                    b.Property<int[]>("FriendIds")
                        .HasColumnType("integer[]")
                        .HasColumnName("user_friends");

                    b.Property<int[]>("GroupIds")
                        .HasColumnType("integer[]")
                        .HasColumnName("user_groups");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("user_password");

                    b.Property<string>("Picture")
                        .HasColumnType("text")
                        .HasColumnName("user_picture");

                    b.Property<bool>("PublicProfile")
                        .HasColumnType("boolean")
                        .HasColumnName("user_public_profile");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("bytea");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("text")
                        .HasColumnName("user_verification_code");

                    b.Property<bool>("Verified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("user_verified");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Name", "Email")
                        .IsUnique();

                    b.ToTable("userProfile");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.Group", b =>
                {
                    b.HasOne("TeamRedBackEnd.Database.Models.Habit", "Habit")
                        .WithOne("Group")
                        .HasForeignKey("TeamRedBackEnd.Database.Models.Group", "HabitID")
                        .HasConstraintName("FK_group_owner_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamRedBackEnd.Database.Models.User", null)
                        .WithMany("Groups")
                        .HasForeignKey("UserId");

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.Habit", b =>
                {
                    b.HasOne("TeamRedBackEnd.Database.Models.User", "User")
                        .WithMany("Habits")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK_owner_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.History", b =>
                {
                    b.HasOne("TeamRedBackEnd.Database.Models.Habit", "Habit")
                        .WithMany("History")
                        .HasForeignKey("HabitId")
                        .HasConstraintName("FK_habitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.Habit", b =>
                {
                    b.Navigation("Group");

                    b.Navigation("History");
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.User", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Habits");
                });
#pragma warning restore 612, 618
        }
    }
}
