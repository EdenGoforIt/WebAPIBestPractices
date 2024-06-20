using System;

namespace Entities.Models;

public class ShapedEntity
{
    public Guid Id { get; set; }
    public Entity Entity { get; set; } = new();
}