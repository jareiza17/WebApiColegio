using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.Entities;

namespace DataAccessLayer
{
    /// <summary>
    /// Contexto de la base de datos para la aplicación.
    /// </summary>
    public class SchoolContext : IdentityDbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SchoolContext"/>.
        /// </summary>
        /// <param name="options">Opciones para configurar el contexto.</param>
        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Configura las relaciones entre las entidades del modelo.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo.</param>
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Subject)
                .WithMany()
                .HasForeignKey(e => e.SubjectID);
        }

    }

}