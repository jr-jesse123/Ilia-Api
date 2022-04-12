namespace ILIA.SimpleStore.API.Models;

/// <summary>
/// sumary
/// </summary>
/// <example> Jessé Junior</example>
public class CustomerModel
{
    /// <summary>
    /// sumary
    /// </summary>
    /// <example> Jessé Junior</example>
    public Guid? Id { get; set; }
    /// <summary>
    /// sumary
    /// </summary>
    /// <example> Jessé Junior</example>
    public string Name { get; set; }
    /// <summary>
    /// sumary
    /// </summary>
    /// <example> Jessé Junior</example>
    public string Email { get; set; }
    /// <summary>
    /// sumary
    /// </summary>
    /// <example> Jessé Junior</example>
    public IEnumerable<OrderModel>? Orders { get; set; }
}
