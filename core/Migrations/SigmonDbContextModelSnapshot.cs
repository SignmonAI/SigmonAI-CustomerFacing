﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using core.Contexts;

#nullable disable

namespace core.Migrations
{
    [DbContext(typeof(SigmonDbContext))]
    partial class SigmonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("core.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("ExpiralDate")
                        .HasColumnType("date")
                        .HasColumnName("expiral_date");

                    b.Property<DateOnly?>("PaymentDate")
                        .HasColumnType("date")
                        .HasColumnName("payment_date");

                    b.Property<decimal>("PaymentDue")
                        .HasColumnType("decimal(5, 4)")
                        .HasColumnName("payment_due");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("core.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneCode")
                        .HasColumnType("varchar(3)")
                        .HasColumnName("phone_code");

                    b.HasKey("Id");

                    b.HasIndex("PhoneCode")
                        .IsUnique()
                        .HasFilter("[phone_code] IS NOT NULL");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("core.Models.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("core.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("PaymentDue")
                        .HasColumnType("decimal(5, 4)")
                        .HasColumnName("payment_due");

                    b.Property<Guid?>("TierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TierId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("core.Models.Tier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModelDescription")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("model_desctiption");

                    b.Property<byte>("ModelNumber")
                        .HasColumnType("tinyint")
                        .HasColumnName("model_number");

                    b.HasKey("Id");

                    b.ToTable("Tiers");
                });

            modelBuilder.Entity("core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(17)")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("core.Models.Bill", b =>
                {
                    b.HasOne("core.Models.Subscription", "Subscription")
                        .WithMany("Bills")
                        .HasForeignKey("SubscriptionId");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("core.Models.Language", b =>
                {
                    b.HasOne("core.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("core.Models.Subscription", b =>
                {
                    b.HasOne("core.Models.Tier", "Tier")
                        .WithMany()
                        .HasForeignKey("TierId");

                    b.HasOne("core.Models.User", "User")
                        .WithOne("Subscription")
                        .HasForeignKey("core.Models.Subscription", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tier");

                    b.Navigation("User");
                });

            modelBuilder.Entity("core.Models.User", b =>
                {
                    b.HasOne("core.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("core.Models.Subscription", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("core.Models.User", b =>
                {
                    b.Navigation("Subscription");
                });
#pragma warning restore 612, 618
        }
    }
}
