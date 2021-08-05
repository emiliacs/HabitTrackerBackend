using Microsoft.EntityFrameworkCore;
using TeamRedBackEnd.Database.Models;

namespace TeamRedBackEnd.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DatabaseContext()
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("userProfile");
                entity.Property(e => e.Id).HasColumnName("user_id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("user_name");
                entity.Property(e => e.UpperName).HasColumnName("user_name_upper");
                entity.Property(e => e.Email).HasColumnName("user_email");
                entity.Property(e => e.UpperEmail).HasColumnName("user_email_upper");
                entity.Property(e => e.Password).HasColumnName("user_password");
                entity.Property(e => e.Salt).HasColumnName("user_salt");
                entity.Property(e => e.BytePassword).HasColumnName("user_byte_password");
                entity.Property(e => e.Picture).HasColumnName("user_picture");
                entity.Property(e => e.PublicProfile).HasColumnName("user_public_profile");
                entity.Property(e => e.VerificationCode).HasColumnName("user_verification_code");
                entity.Property(e => e.Verified).HasColumnName("user_verified").HasDefaultValue(false);
                entity.Property(e => e.FriendIds).HasColumnName("user_friends");
                entity.Property(e => e.GroupIds).HasColumnName("user_groups");

                entity.HasIndex(e => e.UpperName).IsUnique(true);
                entity.HasIndex(e => e.UpperEmail).IsUnique(true);


                entity.HasData(new User {
                    Id = 99, Name = "Test",
                    UpperName = "TEST", 
                    Email = "test@email.com", 
                    UpperEmail = "TEST@EMAIL.COM",
                    Password = "STGItxymuDAL0WDjKua5kg==", 
                    Salt = new byte[16] { 65, 91, 161, 36, 119, 215, 97, 204, 197, 48, 94, 58, 57, 135, 212, 25 }, 
                    BytePassword = new byte[16] { 73, 49, 136, 183, 28, 166, 184, 48, 11, 209, 96, 227, 42, 230, 185, 146 }, 
                    VerificationCode = "44OT3DmJrUTVRMOUi1IRG", 
                    Verified = false,
                    PublicProfile = false 
                });
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");
                entity.Property(e => e.Id).HasColumnName("group_id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.HabitID).HasColumnName("group_habit");
                entity.Property(e => e.UserIds).HasColumnName("group_user_ids");

                entity.HasOne(h => h.Habit).WithOne(g => g.Group).HasConstraintName("FK_habitId");
            
            });

            modelBuilder.Entity<Habit>(entity =>
            {
                entity.ToTable("habit");
                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.HabitId).HasColumnName("habit_id").IsRequired();
                entity.Property(e => e.OwnerId).HasColumnName("habit_owner_id").IsRequired();
                entity.Property(e => e.Name).HasColumnName("habit_name");
                entity.Property(e => e.Description).HasColumnName("habit_description");
                entity.Property(e => e.Reward).HasColumnName("habit_reward");
                entity.Property(e => e.StartDate).HasColumnName("habit_start_date");
                entity.Property(e => e.EndDate).HasColumnName("habit_end_date");
                entity.Property(e => e.TimesTodo).HasColumnName("habit_times_todo");
                entity.Property(e => e.DayRepeat).HasColumnName("habit_day_repeat");
                entity.Property(e => e.TimeSpan).HasColumnName("habit_timespan");
                entity.Property(e => e.Category).HasColumnName("habit_category");
                entity.Property(e => e.ChosenWeekDays).HasColumnName("habit_weekdays");
                entity.Property(e => e.PublicHabit).HasColumnName("habit_public_habit");
                entity.Property(e => e.Favorite).HasColumnName("habit_favourite");

                entity.HasOne(u => u.User).WithMany(h => h.Habits).HasForeignKey(o => o.OwnerId).HasConstraintName("FK_habit_owner_Id");
                entity.HasOne(g => g.Group).WithOne(h => h.Habit).HasConstraintName("FK_habit_group_owner_Id");

                entity.HasOne(u => u.User).WithMany(h => h.Habits).HasForeignKey(o => o.OwnerId).HasConstraintName("FK_owner_Id");
                entity.HasOne(g => g.Group).WithOne(h => h.Habit).HasConstraintName("FK_group_owner_Id");

                entity.HasData(
                    new Habit {
                        Id = 201,
                        HabitId = 201,
                        OwnerId = 99,
                        Name = "The Habit",
                        Description = "Habit todo",
                        Reward = "...pat on back",
                        TimesTodo = 20,
                        StartDate = System.DateTime.Now.AddDays(-14),
                        EndDate = System.DateTime.Now.AddDays(100),
                        PublicHabit = true
                    },
                    new Habit {
                        Id = 202,
                        HabitId = 202,
                        OwnerId = 99,
                        Name = "Wake up",
                        Description = "Zzz...",
                        Reward = "Coffee",
                        TimesTodo = 20,
                        StartDate = System.DateTime.Now.Date,
                        EndDate = System.DateTime.Now.AddDays(100).Date,
                        PublicHabit = true
                    },
                    new Habit {
                        Id = 203,
                        HabitId = 203,
                        OwnerId = 99,
                        Name = "Breath",
                        Description = "You know, to stay alive",
                        Reward = "Staying alive", TimesTodo = 200000,
                        StartDate = System.DateTime.Now.Date,
                        EndDate = System.DateTime.Now.AddDays(100).Date,
                        PublicHabit = true
                    },
                    new Habit { Id = 204,
                        HabitId = 204, OwnerId = 99,
                        Name = "Think",
                        Description = "Doing the thing with brains",
                        Reward = "Not seeming stupid",
                        TimesTodo = 300000,
                        StartDate = System.DateTime.Now.Date,
                        EndDate = System.DateTime.Now.AddDays(100).Date,
                        PublicHabit = true
                    },
                    new Habit {
                        Id = 205,
                        HabitId = 205,
                        OwnerId = 99,
                        Name = "Check if hungry",
                        Description = "hungry ? eat : check again later",
                        Reward = "Not starving",
                        TimesTodo = 300, StartDate = System.DateTime.Now.Date,
                        EndDate = System.DateTime.Now.AddDays(100).Date,
                        PublicHabit = true
                    });

            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("history");
                entity.Property(e => e.Id).HasColumnName("history_id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.OwnerId).HasColumnName("history_owner_id");
                entity.Property(e => e.HabitHistoryDate).HasColumnName("history_date").IsRequired();
                entity.Property(e => e.HabitHistoryResult).HasColumnName("history_result_bool");
                entity.Property(e => e.HabitHistoryNum).HasColumnName("history_result_num");

                entity.HasOne(h => h.Habit).WithMany(r => r.History).HasForeignKey(k => k.HabitId).HasConstraintName("FK_habitId");
                entity.HasOne(u => u.User).WithMany(r => r.Histories).HasForeignKey(k => k.OwnerId).HasConstraintName("FK_ownerId");

                entity.HasData(
                    new History { 
                        Id = 279, 
                        HabitId = 201, 
                        OwnerId = 99,
                        HabitHistoryDate = System.DateTime.Now.AddDays(3).Date,
                        HabitHistoryResult = true, HabitHistoryNum = 1 
                    }, 
                    new History { 
                        Id = 297,
                        HabitId = 201, 
                        OwnerId = 99, 
                        HabitHistoryDate = System.DateTime.Now.AddDays(2).Date, 
                        HabitHistoryResult = true, HabitHistoryNum = 2 
                    },
                    new History {
                        Id = 298, 
                        HabitId = 201, 
                        OwnerId = 99, 
                        HabitHistoryDate = System.DateTime.Now.AddDays(1).Date,
                        HabitHistoryResult = true, HabitHistoryNum = 3
                    },
                    new History{
                        Id = 300,
                        HabitId = 201,
                        OwnerId = 99,
                        HabitHistoryDate = System.DateTime.Now.Date,
                        HabitHistoryResult = true,
                        HabitHistoryNum = 4
                    });
            });
        }
    }
}
