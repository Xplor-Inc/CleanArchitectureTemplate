﻿namespace CleanArchitectureTemplate.SqlServer.Maps.Audits;
public class UserLoginMap : Map<UserLogin>
{
    public override void Configure(EntityTypeBuilder<UserLogin> entity)
    {
        entity
            .ToTable("UserLogins");

        entity
            .Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.EMAIL_LENGTH);

        entity
            .Property(e => e.VisitorId)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.EMAIL_LENGTH);
    }
}