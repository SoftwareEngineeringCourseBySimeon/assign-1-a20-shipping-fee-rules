namespace Shipping;

/// <summary>
/// Defines a rule for determining whether a shipment qualifies and calculating its shipping fee.
/// </summary>
public interface IShippingFeeRule
{
    /// <summary>
    /// Determines whether this rule applies to the shipment.
    /// </summary>
    /// <param name="shipment">The shipment to evaluate against the rule.</param>
    /// <returns><c>true</c> if the rule applies; otherwise, <c>false</c>.</returns>
    bool AppliesTo(Shipment shipment);

    /// <summary>
    /// Calculates the shipping fee for the shipment.
    /// </summary>
    /// <param name="shipment">The shipment for which to calculate the fee.</param>
    /// <returns>The calculated shipping fee.</returns>
    decimal Calculate(Shipment shipment);
}