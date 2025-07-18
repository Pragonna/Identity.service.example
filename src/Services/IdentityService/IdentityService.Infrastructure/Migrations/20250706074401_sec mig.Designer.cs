﻿// <auto-generated />
using System;
using IdentityService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IdentityService.Infrastructure.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20250706074401_sec mig")]
    partial class secmig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IdentityService.Domain.Entities.OperationClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OperationClaims");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 0
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Role = 1
                        });
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpireAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OldRefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RevokedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfBirth = new DateTime(2025, 7, 6, 7, 44, 0, 734, DateTimeKind.Utc).AddTicks(2550),
                            Email = "alizadeemil5@gmail.com",
                            FirstName = "Emil",
                            Gender = 0,
                            IsActive = false,
                            LastName = "Alizada",
                            PasswordHash = new byte[] { 115, 199, 219, 13, 254, 77, 50, 185, 169, 69, 29, 79, 236, 63, 43, 228, 137, 14, 17, 16, 19, 254, 206, 120, 187, 1, 230, 134, 153, 93, 117, 235, 139, 103, 175, 159, 226, 178, 32, 48, 247, 15, 12, 147, 2, 217, 68, 208, 112, 167, 205, 148, 7, 55, 131, 191, 116, 80, 239, 46, 241, 34, 72, 161 },
                            PasswordSalt = new byte[] { 253, 243, 90, 158, 26, 201, 245, 234, 141, 202, 71, 85, 8, 110, 186, 217, 142, 9, 129, 116, 196, 231, 125, 98, 205, 153, 152, 249, 24, 121, 193, 174, 245, 184, 105, 6, 218, 120, 216, 67, 246, 200, 46, 110, 216, 94, 231, 98, 105, 36, 91, 152, 119, 245, 110, 76, 63, 33, 81, 67, 73, 223, 220, 202, 198, 164, 7, 48, 14, 149, 13, 166, 35, 19, 82, 143, 85, 51, 97, 136, 118, 53, 22, 144, 10, 3, 157, 72, 118, 57, 47, 244, 142, 32, 101, 40, 44, 159, 114, 105, 116, 40, 221, 208, 4, 88, 160, 121, 0, 117, 244, 242, 254, 160, 42, 157, 58, 167, 76, 6, 152, 141, 241, 213, 74, 200, 56, 44 },
                            Username = "pragonna"
                        });
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.UserOperationClaim", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OperationClaimId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "OperationClaimId");

                    b.HasIndex("OperationClaimId");

                    b.ToTable("UserOperationClaims");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            OperationClaimId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            OperationClaimId = new Guid("00000000-0000-0000-0000-000000000003")
                        });
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("IdentityService.Domain.Entities.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("IdentityService.Domain.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.UserOperationClaim", b =>
                {
                    b.HasOne("IdentityService.Domain.Entities.OperationClaim", "OperationClaim")
                        .WithMany("UserOperationClaim")
                        .HasForeignKey("OperationClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IdentityService.Domain.Entities.User", "User")
                        .WithMany("UserOperationClaim")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OperationClaim");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.OperationClaim", b =>
                {
                    b.Navigation("UserOperationClaim");
                });

            modelBuilder.Entity("IdentityService.Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshToken")
                        .IsRequired();

                    b.Navigation("UserOperationClaim");
                });
#pragma warning restore 612, 618
        }
    }
}
