using BookTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MoviePoint;Integrated Security=True;TrustServerCertificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Movie>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Cinema>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Category>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Actor>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<ActorMovie>()
                .HasKey(e => new {e.ActorsId , e.MoviesId});

            modelBuilder.Entity<Movie>()
                .Property(e => e.Name)
                .HasColumnType("varchar(100)");
            modelBuilder.Entity<Movie>()
                .Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false) ;
            modelBuilder.Entity<Movie>()
                .Property(e => e.TrailerUrl)
                 .HasMaxLength(1000);
            modelBuilder.Entity<Movie>()
                .Property(e => e.ImgUrl)
                .HasColumnType("varchar(1000)");


            modelBuilder.Entity<Cinema>()
                .Property(e => e.Name)
                .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Cinema>()
               .Property(e => e.Description)
               .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Cinema>()
               .Property(e => e.CinemaLogo)
               .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Cinema>()
               .Property(e => e.Address)
               .HasColumnType("varchar(1000)");

            modelBuilder.Entity<Category>()
               .Property(e => e.Name)
               .HasColumnType("varchar(50)");

            modelBuilder.Entity<Actor>()
                .Property(e=>e.FirstName)
                .HasColumnType("varchar(50)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.LastName)
                .HasColumnType("varchar(50)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.Bio)
                .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.ProfilePicture)
                .HasColumnType("varchar(1000)");
            modelBuilder.Entity<Actor>()
                .Property(e => e.News)
                .HasColumnType("varchar(1000)");

            modelBuilder.Entity<Category>()
               .HasMany(e => e.Movies)
               .WithOne(e => e.Category)
               .HasForeignKey(e => e.CategoryId);
            modelBuilder.Entity<Movie>()
               .HasOne(e => e.Category)
               .WithMany(e => e.Movies)
               .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Cinema>()
               .HasMany(e => e.Movies)
               .WithOne(e => e.Cinema)
               .HasForeignKey(e => e.CinemaId);
            modelBuilder.Entity<Movie>()
               .HasOne(e => e.Cinema)
               .WithMany(e => e.Movies)
               .HasForeignKey(e => e.CinemaId);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Actors)
                .WithMany(e => e.Movies);

            modelBuilder.Entity<Actor>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Actors);





        }
    }
}
