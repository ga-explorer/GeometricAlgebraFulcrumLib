using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Optimization.GeneticProgramming.Cartesian;

public sealed class CGpNodeInputWeight :
    IEquatable<CGpNodeInputWeight>
{
    public static CGpNodeInputWeight One { get; }
        = new CGpNodeInputWeight(1);

    public static CGpNodeInputWeight MinusOne { get; }
        = new CGpNodeInputWeight(-1);


    public static implicit operator double(CGpNodeInputWeight? weight)
    {
        return weight?.Value ?? 1d;
    }

    public static bool operator ==(CGpNodeInputWeight? left, CGpNodeInputWeight? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CGpNodeInputWeight? left, CGpNodeInputWeight? right)
    {
        return !Equals(left, right);
    }


    public double MidValue { get; }

    public double ValueRange { get; }

    public double Value { get; private set; }

    public bool IsParametric 
        => ValueRange > 0;

    
    internal CGpNodeInputWeight(double midValue, double valueRange = 0d)
    {
        Debug.Assert(
            !double.IsNaN(midValue) &&
            double.IsFinite(midValue) &&
            !double.IsNaN(valueRange) &&
            double.IsFinite(valueRange)
        );

        MidValue = midValue;
        ValueRange = Math.Abs(valueRange);
        Value = midValue;
    }


    public CGpNodeInputWeight GetCopy()
    {
        return IsParametric 
            ? new CGpNodeInputWeight(MidValue, ValueRange) { Value = Value }
            : this;
    }

    public void SetValue(double value)
    {
        Debug.Assert(
            value >= MidValue - ValueRange &&
            value <= MidValue + ValueRange
        );

        if (!IsParametric)
            throw new InvalidOperationException();

        Value = value;
    }

    public double SetRandomValue()
    {
        if (IsParametric)
            Value = (CGpParameters.GetRandomDouble() * 2 - 1) * ValueRange + MidValue;

        return Value;
    }

    public bool Equals(CGpNodeInputWeight? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is CGpNodeInputWeight other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString("G");
    }
}