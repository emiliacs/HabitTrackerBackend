﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamRedBackEnd.Database;

namespace TeamRedBackEnd.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasData(
                        new
                        {
                            Id = 201,
                            Category = 0,
                            ChosenWeekDays = 0,
                            DayRepeat = 0,
                            Description = "Habit todo",
                            EndDate = new DateTime(2021, 11, 13, 12, 52, 18, 86, DateTimeKind.Local).AddTicks(7252),
                            Favorite = false,
                            HabitId = 201,
                            Name = "The Habit",
                            OwnerId = 99,
                            PublicHabit = true,
                            Reward = "...pat on back",
                            StartDate = new DateTime(2021, 7, 22, 12, 52, 18, 83, DateTimeKind.Local).AddTicks(7092),
                            TimeSpan = 0,
                            TimesTodo = 20
                        },
                        new
                        {
                            Id = 202,
                            Category = 0,
                            ChosenWeekDays = 0,
                            DayRepeat = 0,
                            Description = "Zzz...",
                            EndDate = new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Favorite = false,
                            HabitId = 202,
                            Name = "Wake up",
                            OwnerId = 99,
                            PublicHabit = true,
                            Reward = "Coffee",
                            StartDate = new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            TimeSpan = 0,
                            TimesTodo = 20
                        },
                        new
                        {
                            Id = 203,
                            Category = 0,
                            ChosenWeekDays = 0,
                            DayRepeat = 0,
                            Description = "You know, to stay alive",
                            EndDate = new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Favorite = false,
                            HabitId = 203,
                            Name = "Breath",
                            OwnerId = 99,
                            PublicHabit = true,
                            Reward = "Staying alive",
                            StartDate = new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            TimeSpan = 0,
                            TimesTodo = 200000
                        },
                        new
                        {
                            Id = 204,
                            Category = 0,
                            ChosenWeekDays = 0,
                            DayRepeat = 0,
                            Description = "Doing the thing with brains",
                            EndDate = new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Favorite = false,
                            HabitId = 204,
                            Name = "Think",
                            OwnerId = 99,
                            PublicHabit = true,
                            Reward = "Not seeming stupid",
                            StartDate = new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            TimeSpan = 0,
                            TimesTodo = 300000
                        },
                        new
                        {
                            Id = 205,
                            Category = 0,
                            ChosenWeekDays = 0,
                            DayRepeat = 0,
                            Description = "hungry ? eat : check again later",
                            EndDate = new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Favorite = false,
                            HabitId = 205,
                            Name = "Check if hungry",
                            OwnerId = 99,
                            PublicHabit = true,
                            Reward = "Not starving",
                            StartDate = new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            TimeSpan = 0,
                            TimesTodo = 300
                        });
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

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer")
                        .HasColumnName("history_owner_id");

                    b.HasKey("Id");

                    b.HasIndex("HabitId");

                    b.HasIndex("OwnerId");

                    b.ToTable("history");

                    b.HasData(
                        new
                        {
                            Id = 279,
                            HabitHistoryDate = new DateTime(2021, 8, 8, 0, 0, 0, 0, DateTimeKind.Local),
                            HabitHistoryNum = 1,
                            HabitHistoryResult = true,
                            HabitId = 201,
                            OwnerId = 99
                        },
                        new
                        {
                            Id = 297,
                            HabitHistoryDate = new DateTime(2021, 8, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            HabitHistoryNum = 2,
                            HabitHistoryResult = true,
                            HabitId = 201,
                            OwnerId = 99
                        },
                        new
                        {
                            Id = 298,
                            HabitHistoryDate = new DateTime(2021, 8, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            HabitHistoryNum = 3,
                            HabitHistoryResult = true,
                            HabitId = 201,
                            OwnerId = 99
                        },
                        new
                        {
                            Id = 300,
                            HabitHistoryDate = new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            HabitHistoryNum = 4,
                            HabitHistoryResult = true,
                            HabitId = 201,
                            OwnerId = 99
                        });
                });

            modelBuilder.Entity("TeamRedBackEnd.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("BytePassword")
                        .HasColumnType("bytea")
                        .HasColumnName("user_byte_password");

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
                        .HasColumnType("bytea")
                        .HasColumnName("user_salt");

                    b.Property<string>("UpperEmail")
                        .HasColumnType("text")
                        .HasColumnName("user_email_upper");

                    b.Property<string>("UpperName")
                        .HasColumnType("text")
                        .HasColumnName("user_name_upper");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("text")
                        .HasColumnName("user_verification_code");

                    b.Property<bool>("Verified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("user_verified");

                    b.HasKey("Id");

                    b.HasIndex("UpperEmail")
                        .IsUnique();

                    b.HasIndex("UpperName")
                        .IsUnique();

                    b.ToTable("userProfile");

                    b.HasData(
                        new
                        {
                            Id = 99,
                            BytePassword = new byte[] { 73, 49, 136, 183, 28, 166, 184, 48, 11, 209, 96, 227, 42, 230, 185, 146 },
                            Email = "test@email.com",
                            Name = "Test",
                            Password = "STGItxymuDAL0WDjKua5kg==",
                            PublicProfile = false,
                            Salt = new byte[] { 65, 91, 161, 36, 119, 215, 97, 204, 197, 48, 94, 58, 57, 135, 212, 25 },
                            UpperEmail = "TEST@EMAIL.COM",
                            UpperName = "TEST",
                            VerificationCode = "44OT3DmJrUTVRMOUi1IRG",
                            Verified = false
                        });
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

                    b.HasOne("TeamRedBackEnd.Database.Models.User", "User")
                        .WithMany("Histories")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK_ownerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habit");

                    b.Navigation("User");
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

                    b.Navigation("Histories");
                });
#pragma warning restore 612, 618
        }
    }
}
