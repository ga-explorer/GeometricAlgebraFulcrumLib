namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Constants;

public abstract class DfConstantValue
{
    public abstract bool IsZero { get; }

    public abstract bool IsOne { get; }

    public abstract double Float64Value { get; }

    public abstract DfConstantValue Simplify();
}