namespace Shipping;

/// <summary>
/// Calculates shipping fees for standard shipments.
/// </summary>
public sealed class StandardShippingFeeRule : IShippingFeeRule
{
    private const decimal BaseFee = 500m; // fixed pay
    private const decimal FeePerKg = 150m; // pay per kg

    public bool AppliesTo(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        return shipment.ServiceLevel == "standard";
    }

    public decimal Calculate(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        return BaseFee + (shipment.WeightKg * FeePerKg);
    }
}