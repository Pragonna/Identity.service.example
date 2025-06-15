using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using IdentityService.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
        builder.Property(x => x.DateOfBirth);
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.PasswordSalt).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        builder
            .HasOne(x => x.Image)
            .WithOne(x => x.User)
            .HasForeignKey<Image>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.RefreshToken)
            .WithOne(x => x.User)
            .HasForeignKey<RefreshToken>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasData(CreateAdminUser());
    }

    private User CreateAdminUser()
    {
        HashingHelper.GenerateHashPassword("Tomas123-", out var passwordHash, out var passwordSalt);
        var user = new User("alizadeemil5@gmail.com",
            "pragonna",
            "Emil", "Alizada",
            DateTime.UtcNow,
            Gender.Male,
            passwordSalt, passwordHash);

        user.Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
        
        return user;
    }
}