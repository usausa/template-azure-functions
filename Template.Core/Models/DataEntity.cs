namespace Template.Models;

using Smart.Data.Accessor.Attributes;

public sealed class DataEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public bool Flag { get; set; }

    public DateTime UpdateAt { get; set; }
}
