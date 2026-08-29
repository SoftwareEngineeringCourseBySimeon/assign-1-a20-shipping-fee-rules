using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using ShippingFeeRules;

namespace UnitTests;

[TestClass]
public class ShippingCalculatorUnitTests
{
    /// <summary>
    /// Test-only rule used to verify that a new implementation of
    /// IShippingFeeRule can be added without modifying ShippingCalculator.
    /// </summary>
    private sealed class MockShippingFeeRule : IShippingFeeRule
    {
        public bool AppliesTo(Shipment shipment)
        {
            ArgumentNullException.ThrowIfNull(shipment);

            return shipment.ServiceLevel == "mock";
        }

        public decimal Calculate(Shipment shipment)
        {
            ArgumentNullException.ThrowIfNull(shipment);

            return 999m;
        }
    }

    /// <summary>
    /// Test-only rule that never applies.
    /// Used to verify that the calculator continues checking rules.
    /// </summary>
    private sealed class NonApplicableShippingFeeRule : IShippingFeeRule
    {
        public bool AppliesTo(Shipment shipment)
        {
            ArgumentNullException.ThrowIfNull(shipment);

            return false;
        }

        public decimal Calculate(Shipment shipment)
        {
            ArgumentNullException.ThrowIfNull(shipment);

            return 111m;
        }
    }

    [TestMethod]
    [Owner("Simeon K.")]
    [Priority(1)]
    public void TestCalculatorThrowsWhenShipmentIsNull()
    {
        Logger.LogMessage("Running TestCalculatorThrowsWhenShipmentIsNull");

        List<IShippingFeeRule> rules =
        [
            new StandardShippingFeeRule(),
            new ExpressShippingFeeRule(),
            new EconomyShippingFeeRule()
        ];

        ShippingCalculator calculator = new ShippingCalculator(rules);

        Assert.Throws<ArgumentNullException>(() => calculator.Calculate(null!));
    }

    [TestMethod]
    [Owner("Simeon K.")]
    [Priority(1)]
    public void TestExistingStandardRule()
    {
        Logger.LogMessage("Running TestExistingStandardRule");

        List<IShippingFeeRule> rules =
        [
            new StandardShippingFeeRule(),
            new ExpressShippingFeeRule(),
            new EconomyShippingFeeRule()
        ];

        ShippingCalculator calculator = new ShippingCalculator(rules);
        Shipment shipment = new Shipment(10, "Standard");

        decimal fee = calculator.Calculate(shipment);

        Assert.IsGreaterThanOrEqualTo(0, fee);
    }

    [TestMethod]
    [Owner("Simeon K.")]
    [Priority(1)]
    public void TestNoMatchingRuleThrows()
    {
        Logger.LogMessage("Running TestNoMatchingRuleThrows");

        List<IShippingFeeRule> rules =
        [
            new StandardShippingFeeRule(),
            new ExpressShippingFeeRule(),
            new EconomyShippingFeeRule()
        ];

        ShippingCalculator calculator = new ShippingCalculator(rules);
        Shipment shipment = new Shipment(10, "Unknown");

        Assert.Throws<InvalidOperationException>(() => calculator.Calculate(shipment));
    }

    [TestMethod]
    [Owner("Simeon K.")]
    [Priority(1)]
    public void TestCalculatorSkipsNonApplicableRule()
    {
        Logger.LogMessage("Running TestCalculatorSkipsNonApplicableRule");

        List<IShippingFeeRule> rules =
        [
            new NonApplicableShippingFeeRule(),
            new EconomyShippingFeeRule()
        ];

        ShippingCalculator calculator = new ShippingCalculator(rules);
        Shipment shipment = new Shipment(20, "Economy");

        decimal fee = calculator.Calculate(shipment);

        Assert.AreEqual(2300m, fee);
    }

    [TestMethod]
    [Owner("Simeon K.")]
    [Priority(1)]
    public void TestNewRuleCanBeAddedWithoutModifyingCalculator()
    {
        Logger.LogMessage("Running TestNewRuleCanBeAddedWithoutModifyingCalculator");

        List<IShippingFeeRule> rules =
        [
            // Existing production rules.
            new StandardShippingFeeRule(),
            new ExpressShippingFeeRule(),
            new EconomyShippingFeeRule(),

            // New rule added through the extension point.
            new MockShippingFeeRule()
        ];

        ShippingCalculator calculator = new ShippingCalculator(rules);
        Shipment shipment = new Shipment(10, "Mock");

        decimal fee = calculator.Calculate(shipment);

        Assert.AreEqual(999m, fee);
    }
}
