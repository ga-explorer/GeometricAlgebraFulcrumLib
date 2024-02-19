using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

public sealed class DfConstant :
    DifferentialBasicFunction
{
    public static DfConstant Zero { get; }
        = new DfConstant(0d);

    public static DfConstant One { get; }
        = new DfConstant(1d);

    public static DfConstant Pi { get; }
        = new DfConstant(Math.PI, "Pi");

    public static DfConstant E { get; }
        = new DfConstant(Math.E, "E");
    
    public static DfConstant Degree { get; }
        = new DfConstant(180d / Math.PI, "Degree");

    // TODO: add more from here: https://reference.wolfram.com/legacy/language/v13.1/tutorial/MathematicalFunctions.html#1408


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfConstant Create(double value)
    {
        return new DfConstant(value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(DfConstant constantFunction)
    {
        return constantFunction.Value;
    }


    public string Name { get; }

    public override bool IsConstant 
        => true;

    public override bool CanBeSimplified 
        => false;

    public bool IsZero 
        => Value == 0d;

    public bool IsOne 
        => Value == 1d;

    public double Value { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfConstant(double value)
    {
        if (value.IsNaNOrInfinite())
            throw new ArgumentException(nameof(value));

        Value = value;
        Name = Value.ToString("G");
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfConstant(double value, string name)
    {
        if (value.IsNaNOrInfinite())
            throw new ArgumentException(nameof(value));

        Value = value;
        Name = name;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Tuple<bool, DifferentialFunction> TrySimplify()
    {
        return new Tuple<bool, DifferentialFunction>(false, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction Simplify()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        if (order < 1) 
            throw new ArgumentException(nameof(order));

        return Zero;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override string ToString()
    //{
    //    return Value.ToString("G");
    //}
}