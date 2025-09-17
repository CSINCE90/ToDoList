using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.model;

namespace ToDoListAPI.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<TaskActivity> TaskActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoList>(entity =>
            {
                entity.ToTable("ToDoLists");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
                entity.Property(x => x.CreatedAt);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.HasMany(x => x.Activities)
                      .WithOne(x => x.ToDoList)
                      .HasForeignKey(x => x.ToDoListId)
                      .OnDelete(DeleteBehavior.Restrict);
                // Global filter: non mostra liste soft-deleted
                entity.HasQueryFilter(x => !x.IsDeleted);
            });

            modelBuilder.Entity<TaskActivity>(entity =>
            {
                entity.ToTable("TaskActivities");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(120);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.DueDate);
                entity.Property(x => x.IsCompleted).HasDefaultValue(false);
                entity.Property(x => x.IsDeleted).HasDefaultValue(false);
                entity.Property(x => x.CreatedAt);
                entity.Property(x => x.UpdatedAt);

                // Global filter: non mostra task soft-deleted nelle query standard
                entity.HasQueryFilter(x => !x.IsDeleted);
            });
        }
    }
}
