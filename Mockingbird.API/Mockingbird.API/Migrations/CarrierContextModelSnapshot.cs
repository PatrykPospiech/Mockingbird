﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mockingbird.API.Database;

#nullable disable

namespace Mockingbird.API.Migrations
{
    [DbContext(typeof(CarrierContext))]
    partial class CarrierContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mockingbird.API.Database.ApiResource", b =>
                {
                    b.Property<int>("ApiResourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApiResourceId"));

                    b.Property<int>("CarrierId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApiResourceId");

                    b.HasIndex("CarrierId");

                    b.ToTable("ApiResources");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Carrier", b =>
                {
                    b.Property<int>("CarrierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarrierId"));

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CarrierId");

                    b.HasIndex("Name", "Nickname")
                        .IsUnique();

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Header", b =>
                {
                    b.Property<int>("HeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HeaderId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResponseId")
                        .HasColumnType("int");

                    b.Property<string>("ResponseId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HeaderId");

                    b.HasIndex("ResponseId1");

                    b.ToTable("Headers");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Method", b =>
                {
                    b.Property<int>("MethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MethodId"));

                    b.Property<int>("ApiResourceId")
                        .HasColumnType("int");

                    b.Property<string>("MethodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MethodId");

                    b.HasIndex("ApiResourceId", "Name", "MethodType")
                        .IsUnique();

                    b.ToTable("Method");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OptionId"));

                    b.Property<int>("CarrierId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OptionId");

                    b.HasIndex("CarrierId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Response", b =>
                {
                    b.Property<string>("ResponseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MethodId")
                        .HasColumnType("int");

                    b.Property<string>("ResponseBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseStatusCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResponseId");

                    b.HasIndex("MethodId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("Mockingbird.API.Database.ApiResource", b =>
                {
                    b.HasOne("Mockingbird.API.Database.Carrier", "Carrier")
                        .WithMany("ApiResources")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Header", b =>
                {
                    b.HasOne("Mockingbird.API.Database.Response", "Response")
                        .WithMany("Headers")
                        .HasForeignKey("ResponseId1");

                    b.Navigation("Response");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Method", b =>
                {
                    b.HasOne("Mockingbird.API.Database.ApiResource", "ApiResource")
                        .WithMany("Methods")
                        .HasForeignKey("ApiResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApiResource");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Option", b =>
                {
                    b.HasOne("Mockingbird.API.Database.Carrier", "Carrier")
                        .WithMany("Options")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Response", b =>
                {
                    b.HasOne("Mockingbird.API.Database.Method", "Method")
                        .WithMany("Responses")
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Method");
                });

            modelBuilder.Entity("Mockingbird.API.Database.ApiResource", b =>
                {
                    b.Navigation("Methods");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Carrier", b =>
                {
                    b.Navigation("ApiResources");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Method", b =>
                {
                    b.Navigation("Responses");
                });

            modelBuilder.Entity("Mockingbird.API.Database.Response", b =>
                {
                    b.Navigation("Headers");
                });
#pragma warning restore 612, 618
        }
    }
}
