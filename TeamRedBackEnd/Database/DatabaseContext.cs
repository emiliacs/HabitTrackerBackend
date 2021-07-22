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
                entity.Property(e => e.Email).HasColumnName("user_email");
                entity.Property(e => e.Password).HasColumnName("user_password");
                entity.Property(e => e.Picture).HasColumnName("user_picture");
                entity.Property(e => e.PublicProfile).HasColumnName("user_public_profile");
                entity.Property(e => e.VerificationCode).HasColumnName("user_verification_code");
                entity.Property(e => e.Verified).HasColumnName("user_verified").HasDefaultValue(false);
                entity.Property(e => e.FriendIds).HasColumnName("user_friends");
                entity.Property(e => e.GroupIds).HasColumnName("user_groups");

                entity.HasIndex(e => e.Name).IsUnique(true);
                entity.HasIndex(e => e.Email).IsUnique(true);
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
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("history");
                entity.Property(e => e.Id).HasColumnName("history_id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.HabitHistoryDate).HasColumnName("history_date").IsRequired();
                entity.Property(e => e.HabitHistoryResult).HasColumnName("history_result_bool");
                entity.Property(e => e.HabitHistoryNum).HasColumnName("history_result_num");

                entity.HasOne(h => h.Habit).WithMany(r => r.History).HasForeignKey(k => k.HabitId).HasConstraintName("FK_habitId");
            });
        }
    }
}
