namespace CleanArchitectureTemplate.SqlServer.Maps.Users;
public class AccountRecoveryMap : Map<AccountRecovery>
{
    public override void Configure(EntityTypeBuilder<AccountRecovery> entity)
    {
        entity
            .ToTable("AccountRecoveries");

        entity
            .Property(e => e.ResetLink)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}