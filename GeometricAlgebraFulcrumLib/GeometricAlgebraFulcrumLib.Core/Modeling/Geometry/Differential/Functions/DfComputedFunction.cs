using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;

public class DfComputedFunction :
    DifferentialCustomFunction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfComputedFunction Create(Func<double, double> valueFunc)
    {
        return new DfComputedFunction(new[]
        {
            valueFunc
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfComputedFunction Create(DifferentialFunction valueFunc)
    {
        return new DfComputedFunction(new Func<double, double>[]
        {
            valueFunc.GetValue
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfComputedFunction Create(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc)
    {
        return new DfComputedFunction(new[]
        {
            valueFunc,
            firstDerivativeFunc
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfComputedFunction Create(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc)
    {
        return new DfComputedFunction(new[]
        {
            valueFunc,
            firstDerivativeFunc,
            secondDerivativeFunc
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfComputedFunction Create(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc, Func<double, double> thirdDerivativeFunc)
    {
        return new DfComputedFunction(new[]
        {
            valueFunc,
            firstDerivativeFunc,
            secondDerivativeFunc,
            thirdDerivativeFunc
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfComputedFunction Create(Func<double, double> valueFunc, Func<double, double> firstDerivativeFunc, Func<double, double> secondDerivativeFunc, Func<double, double> thirdDerivativeFunc, Func<double, double> fourthDerivativeFunc)
    {
        return new DfComputedFunction(new[]
        {
            valueFunc,
            firstDerivativeFunc,
            secondDerivativeFunc,
            thirdDerivativeFunc,
            fourthDerivativeFunc
        });
    }


    private readonly IReadOnlyList<Func<double, double>> _functionList;

    
    public override bool IsConstant
        => false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DfComputedFunction(Func<double, double> function1)
        : base(false)
    {
        _functionList = new[] { function1 };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DfComputedFunction(IReadOnlyList<Func<double, double>> functionList)
        : base(false)
    {
        _functionList = functionList;
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return _functionList[0](t);
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
        var maxOrder = _functionList.Count - 1;

        if (maxOrder >= 1)
            return new DfComputedFunction(_functionList.Skip(1).ToImmutableArray());

        throw new ArgumentOutOfRangeException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetDerivativeValue(double t, int order)
    {
        var maxOrder = _functionList.Count - 1;

        if (order <= maxOrder)
            return _functionList[order](t);

        return Differentiate.Derivative(
            _functionList[maxOrder],
            t,
            order - maxOrder
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        if (order == 0)
            return this;

        var maxOrder = _functionList.Count - 1;

        if (order <= maxOrder)
            return new DfComputedFunction(_functionList.Skip(order).ToImmutableArray());

        throw new ArgumentOutOfRangeException();
    }
}