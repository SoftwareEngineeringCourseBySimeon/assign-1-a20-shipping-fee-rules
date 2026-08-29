namespace ShippingFeeRules;

/// <summary>
/// Calculator that finds the first applicable shipping fee rule and calculates the fee.
/// </summary>
/// <param name="rules">Collection of shipping fee rules to apply.</param>
public sealed class ShippingCalculator(IEnumerable<IShippingFeeRule> rules)
{
    /// <summary>
    /// Applies the first applicable shipping fee rule and throws an error if no rule applies.
    /// </summary>
    /// <param name="shipment">The shipment for which the shipping fee is calculated.</param>
    /// <returns>The calculated shipping fee (decimal).</returns>
    /// <exception cref="InvalidOperationException">Thrown when no applicable shipping fee rule is found.</exception>
    public decimal Calculate(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(argument: shipment);

        foreach (IShippingFeeRule rule in rules)
        {
            if (rule.AppliesTo(shipment))
            {
                return rule.Calculate(shipment);
            }
        }

        throw new InvalidOperationException("No applicable shipping fee rule found.");
    }
}
