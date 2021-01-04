﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using server;

namespace server.Migrations
{
    [DbContext(typeof(RssReaderContext))]
    partial class RssReaderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("server.Models.Channel", b =>
                {
                    b.Property<int>("IdChannel")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_channel")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnName("link")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("Visible")
                        .HasColumnName("visible")
                        .HasColumnType("boolean");

                    b.HasKey("IdChannel");

                    b.HasIndex("IdUser");

                    b.ToTable("channel");
                });

            modelBuilder.Entity("server.Models.Favorite", b =>
                {
                    b.Property<int>("IdFavorite")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_favorite")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnName("link")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("IdFavorite");

                    b.HasIndex("IdUser");

                    b.ToTable("favorite");
                });

            modelBuilder.Entity("server.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("login")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("IdUser");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("user_account");
                });

            modelBuilder.Entity("server.Models.Channel", b =>
                {
                    b.HasOne("server.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Models.Favorite", b =>
                {
                    b.HasOne("server.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
