using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Mvc.Models;

public partial class BookSwapDbContext : DbContext
{
    public BookSwapDbContext()
    {
    }

    public BookSwapDbContext(DbContextOptions<BookSwapDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<BookCatalog> BookCatalogs { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<PhysicalItem> PhysicalItems { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Name=BookSwapDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("author_pkey");

            entity.ToTable("author");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
        });

        modelBuilder.Entity<BookCatalog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("book_catalog_pkey");

            entity.ToTable("book_catalog");

            entity.HasIndex(e => e.Isbn, "book_catalog_isbn_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("book_author_author_id_fkey"),
                    l => l.HasOne<BookCatalog>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("book_author_book_id_fkey"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("book_author_pkey");
                        j.ToTable("book_author");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("book_genre_genre_id_fkey"),
                    l => l.HasOne<BookCatalog>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("book_genre_book_id_fkey"),
                    j =>
                    {
                        j.HasKey("BookId", "GenreId").HasName("book_genre_pkey");
                        j.ToTable("book_genre");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("GenreId").HasColumnName("genre_id");
                    });
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.ToTable("city");

            entity.HasIndex(e => e.Name, "city_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genre_pkey");

            entity.ToTable("genre");

            entity.HasIndex(e => e.Name, "genre_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("message_pkey");

            entity.ToTable("message");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MessageText).HasColumnName("message_text");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sent_at");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("message_sender_id_fkey");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Messages)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("message_transaction_id_fkey");
        });

        modelBuilder.Entity<PhysicalItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("physical_item_pkey");

            entity.ToTable("physical_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'available'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Book).WithMany(p => p.PhysicalItems)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("physical_item_book_id_fkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.PhysicalItems)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("physical_item_owner_id_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.RequesterId).HasColumnName("requester_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TargetItemId).HasColumnName("target_item_id");

            entity.HasOne(d => d.Requester).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.RequesterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_requester_id_fkey");

            entity.HasOne(d => d.TargetItem).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TargetItemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_target_item_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "User_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("User_city_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
