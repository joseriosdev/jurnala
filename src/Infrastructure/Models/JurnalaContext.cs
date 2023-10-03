using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Models
{
    public partial class JurnalaContext : DbContext
    {
        public JurnalaContext()
        {
        }

        public JurnalaContext(DbContextOptions<JurnalaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignedEmployee> AssignedEmployees { get; set; } = null!;
        public virtual DbSet<AssignedProject> AssignedProjects { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Timesheet> Timesheets { get; set; } = null!;
        public virtual DbSet<Timeslot> Timeslots { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=JRIOSM-NH01\\SQLEXPRESS;Database=Jurnala;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignedEmployee>(entity =>
            {
                entity.ToTable("assigned_employees");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .HasColumnName("id");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(40)
                    .HasColumnName("employee_id");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(40)
                    .HasColumnName("manager_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignedEmployeeEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__assigned___emplo__6754599E");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.AssignedEmployeeManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__assigned___manag__68487DD7");
            });

            modelBuilder.Entity<AssignedProject>(entity =>
            {
                entity.ToTable("assigned_projects");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .HasColumnName("id");

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(40)
                    .HasColumnName("project_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(40)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.AssignedProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__assigned___proje__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssignedProjects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__assigned___user___656C112C");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("projects");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("created_by");

                entity.Property(e => e.CurrentDeep).HasColumnName("current_deep");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.ParentProjectId)
                    .HasMaxLength(40)
                    .HasColumnName("parent_project_id");

                entity.Property(e => e.ProjectCode)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("project_code");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ProjectCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__projects__create__5AEE82B9");

                entity.HasOne(d => d.ParentProject)
                    .WithMany(p => p.InverseParentProject)
                    .HasForeignKey(d => d.ParentProjectId)
                    .HasConstraintName("FK__projects__parent__5CD6CB2B");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ProjectUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__projects__update__5BE2A6F2");
            });

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.ToTable("timesheets");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("created_by");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TimesheetCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__timesheet__creat__5DCAEF64");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.TimesheetUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__timesheet__updat__5EBF139D");
            });

            modelBuilder.Entity<Timeslot>(entity =>
            {
                entity.ToTable("timeslots");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("created_by");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.HoursWorked).HasColumnName("hours_worked");

                entity.Property(e => e.ProjectId)
                    .HasMaxLength(40)
                    .HasColumnName("project_id");

                entity.Property(e => e.TimesheetId)
                    .HasMaxLength(40)
                    .HasColumnName("timesheet_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TimeslotCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__timeslots__creat__5FB337D6");

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.Timeslots)
                    .HasForeignKey(d => d.TimesheetId)
                    .HasConstraintName("FK__timeslots__times__619B8048");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.TimeslotUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__timeslots__updat__60A75C0F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("created_by");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.FullName)
                    .HasMaxLength(40)
                    .HasColumnName("full_name");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("last_name");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(40)
                    .HasColumnName("manager_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .HasColumnName("role");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(40)
                    .HasColumnName("updated_by");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__users__created_b__628FA481");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__users__manager_i__6477ECF3");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InverseUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__users__updated_b__6383C8BA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
