using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

public sealed class DfVar :
    DifferentialBasicFunction
{
    public static DfVar DefaultFunction { get; }
        = new DfVar();

    
    public override bool CanBeSimplified 
        => false;

    public override bool IsConstant 
        => false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfVar()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return t;
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
        return DfConstant.One;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        if (order < 1) 
            throw new ArgumentException(nameof(order));

        return order > 1 
            ? DfConstant.Zero 
            : DfConstant.One;
    }
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override string ToString()
    //{
    //    return "x";
    //}

}