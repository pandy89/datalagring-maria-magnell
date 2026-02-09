namespace ClassCloud.Domain.Entities;

public class LocationEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
    public Byte[] RowVersion { get; set; } = null!;
}
