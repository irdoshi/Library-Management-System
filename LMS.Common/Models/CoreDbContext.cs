//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Microsoft">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace LMS.Common.Models
    {
    using Microsoft.EntityFrameworkCore;

    public partial class CoreDbContext : DbContext
            {
            public CoreDbContext()
                {
                }

            public CoreDbContext(DbContextOptions<CoreDbContext> options)
                : base(options)
                {
                }

            public virtual DbSet<Book> Book 
            { 
             get; 
             set; 
             }
            public virtual DbSet<BookLibraryAssociation> BookLibraryAssociation 
            { 
            get; 
            set; 
            }
            public virtual DbSet<Library> Library 
            {
            get; 
            set; 
            }
            public virtual DbSet<Role> Role 
            { 
            get; 
            set; 
            }
            public virtual DbSet<User> User 
            { 
            get; 
            set; 
            }
            public virtual DbSet<UserBookAssociation> UserBookAssociation 
            { 
            get; 
            set; 
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                modelBuilder.Entity<Book>(entity =>
                {

                    entity.HasIndex(e => e.BookId)
                        .HasName("UX_Book")
                        .IsUnique();
   
                    entity.Property(e => e.Author).IsUnicode(false);

                    entity.Property(e => e.Genre).IsUnicode(false);

                    entity.Property(e => e.Title).IsUnicode(false);
                });

                modelBuilder.Entity<BookLibraryAssociation>(entity =>
                {
                    entity.HasIndex(e => e.BookLibraryAssociationId)
                        .HasName("UX_BookLibraryAssociation")
                        .IsUnique();

                    entity.Property(e => e.IsAvailable).IsUnicode(false);

                    entity.Property(e => e.IsCheckedOut).IsUnicode(false);
                });

                modelBuilder.Entity<Library>(entity =>
                {
                    entity.HasIndex(e => e.LibraryId)
                        .HasName("UX_Library")
                        .IsUnique();

                    entity.Property(e => e.LibraryId).ValueGeneratedNever();
                });

                modelBuilder.Entity<Role>(entity =>
                {
                    entity.HasIndex(e => e.RoleId)
                        .HasName("UX_Role")
                        .IsUnique();

                    entity.Property(e => e.RoleId).ValueGeneratedNever();

                    entity.Property(e => e.Name).IsUnicode(false);
                });

                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasIndex(e => e.UserId)
                        .HasName("IX_User");

                    entity.Property(e => e.UserId).ValueGeneratedNever();
                });

                modelBuilder.Entity<UserBookAssociation>(entity =>
                {
                    entity.HasIndex(e => e.Id)
                        .HasName("UX_UserBookAssociation")
                        .IsUnique();

                    entity.HasOne(d => d.User)
                        .WithMany(p => p.UserBookAssociation)
                        .HasForeignKey(d => d.UserId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserBookAssociation_User");
                });
            
                OnModelCreatingPartial(modelBuilder);
                }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
            }
        }