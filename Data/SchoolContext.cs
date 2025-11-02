using ApiProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ApiProject.Data
{
    public class SchoolContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) 
        { 
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Parent>()
                .HasMany(p => p.Students)
                .WithOne(s => s.Parent)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Parent
            modelBuilder.Entity<Parent>().HasData(
          new Parent { Id = 1, FullName = "Ahmed Ali", Email = "ahmed@example.com", Phone = "01012345678" },
          new Parent { Id = 2, FullName = "Mona Youssef", Email = "mona@example.com", Phone = "01198765432" },
          new Parent { Id = 3, FullName = "Tarek Hassan", Email = "tarek@example.com", Phone = "01234567890" }
      );

            //  Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Omar Ahmed", Grade = "Grade 5", BirthDate = new DateTime(2013, 4, 12), ParentId = 1 },
                new Student { Id = 2, Name = "Laila Mona", Grade = "Grade 3", BirthDate = new DateTime(2015, 9, 3), ParentId = 2 },
                new Student { Id = 3, Name = "Sara Mona", Grade = "Grade 6", BirthDate = new DateTime(2012, 7, 21), ParentId = 2 },
                new Student { Id = 4, Name = "Khaled Tarek", Grade = "Grade 4", BirthDate = new DateTime(2014, 11, 2), ParentId = 3 }
            );
        }
    }
}
