namespace GenogramSystem.WebApp.Models.Dtos;

public class EntityDto
{
    public long             Id              { get; set; }
    public Guid             UniqueId        { get; set; }
}

public class CreatableEntityDto : EntityDto
{
    public long?            CreatedById     { get; set; }
    public DateTimeOffset?  CreatedOn       { get; set; }
    public UserDto?         CreatedBy       { get; set; }
}


public class AuditableDto : CreatableEntityDto
{
    public long?            DeletedById     { get; set; }
    public DateTimeOffset?  DeletedOn       { get; set; }
    public long?            UpdatedById     { get; set; }
    public DateTimeOffset?  UpdatedOn       { get; set; }

    public UserDto?         UpdatedBy       { get; set; }

}
