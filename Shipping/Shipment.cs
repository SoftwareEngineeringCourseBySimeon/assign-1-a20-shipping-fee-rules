namespace Shipping;

/// <summary>
/// Represents a shipment on which rules are applied and the shipping fee is calculated.
/// </summary>
/// <param name="WeightKg">The weight of the shipment (in kilograms).</param>
/// <param name="ServiceLevel">The delivery speed and handling type (case-insensitive).</param>
public sealed record Shipment
{
    public decimal WeightKg { get; }
    public string ServiceLevel { get; }

    /// <summary>
    /// Validates and normalizes the shipment inputs before assigning them.
    /// </summary>
    /// <param name="weightKg">The weight of the shipment in kilograms.</param>
    /// <param name="serviceLevel">The delivery speed and handling type.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the weight is less than or equal to zero.</exception>
    /// <exception cref="ArgumentException">Thrown when the service level is null, empty or whitespace.</exception>
    public Shipment(decimal weightKg, string serviceLevel)
    {
        if (weightKg <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(weightKg), "Weight must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(serviceLevel))
        {
            throw new ArgumentException("Service level cannot be empty.", nameof(serviceLevel));
        }

        WeightKg = weightKg;
        ServiceLevel = serviceLevel.Trim().ToLowerInvariant();
    }
}