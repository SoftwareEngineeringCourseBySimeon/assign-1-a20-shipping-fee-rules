namespace ShippingFeeRules;

/// <summary>
/// Calculates shipping fees for Economy shipments.
/// </summary>
public sealed class EconomyShippingFeeRule : IShippingFeeRule
{
    private const decimal BaseFee = 300m; // fixed pay
    private const decimal FeePerKg = 100m; // pay per kg

    public bool AppliesTo(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        return shipment.ServiceLevel == "economy";
    }

    public decimal Calculate(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        return BaseFee + (shipment.WeightKg * FeePerKg);
    }
}
