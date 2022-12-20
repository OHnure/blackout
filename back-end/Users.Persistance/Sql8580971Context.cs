using Users.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Users.Domain;

public partial class Sql8580971Context : DbContext, IUsersDbContext
{
    public Sql8580971Context()
    {
    }

    public Sql8580971Context(DbContextOptions<Sql8580971Context> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;port=3306;username=root;database=university", ServerVersion.Parse("5.5.62-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<User>(entity =>
        {
            entity                
                .ToTable("users");
            entity.HasKey(user => user.Id);

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.City)
                .HasMaxLength(32)
                .HasColumnName("city")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Id)
                .HasColumnType("int(3)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasColumnName("password")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
