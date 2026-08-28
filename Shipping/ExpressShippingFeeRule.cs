namespace Shipping;

/// <summary>
/// Calculates shipping fees for Express shipments.
/// </summary>
public sealed class ExpressShippingFeeRule : IShippingFeeRule
{
    private const decimal BaseFee = 1000m; // fixed pay
    private const decimal FeePerKg = 300m; // pay per kg

    public bool AppliesTo(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        return shipment.ServiceLevel == "express";
    }

    public decimal Calculate(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        return BaseFee + (shipment.WeightKg * FeePerKg);
    }
}

