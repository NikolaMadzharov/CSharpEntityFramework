using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;

namespace P01_StudentSystem.Data
{

    public class StudentSystemContext : DbContext
    {
       

            public StudentSystemContext()
            {

            }

            public StudentSystemContext(DbContextOptions options) : base(options)
            {

            }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(k => new { k.StudentId, k.CourseId });
        }

    }
}