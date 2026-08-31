namespace Template.Models;

public sealed class DataEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public bool Flag { get; set; }

    public DateTime UpdateAt { get; set; }
}
