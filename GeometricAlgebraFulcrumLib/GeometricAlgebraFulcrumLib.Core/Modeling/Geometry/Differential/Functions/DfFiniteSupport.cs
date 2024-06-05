using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;

public sealed class DfFiniteSupport :
    DifferentialUnaryFunction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfFiniteSupport Create(double minVarValue, double maxVarValue, DifferentialFunction baseFunction, bool canBeSimplified = true)
    {
        return new DfFiniteSupport(
            minVarValue,
            maxVarValue,
            baseFunction, 
            canBeSimplified
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfFiniteSupport CreateSmoothUnitStep()
    {
        var t = DfVar.DefaultFunction;
        var s = 1 - t;
        var x = 1 / t - 1 / s;
        var baseFunction = 1 / (1 + x.Exp());

        return new DfFiniteSupport(
            0d, 
            1d, 
            baseFunction, 
            false
        );
    }
    
    public double MinVarValue { get; }

    public double MaxVarValue { get; }

    public double MinVarFunctionValue { get; }

    public double MaxVarFunctionValue { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfFiniteSupport(double minVarValue, double maxVarValue, DifferentialFunction baseFunction, bool canBeSimplified) 
        : base(baseFunction, canBeSimplified)
    {
        if (minVarValue.IsNaNOrInfinite() || maxVarValue.IsNaNOrInfinite() || minVarValue >= maxVarValue)
            throw new ArgumentException();
        
        MinVarValue = minVarValue;
        MaxVarValue = maxVarValue;
        
        MinVarFunctionValue = baseFunction.GetValue(minVarValue);
        MaxVarFunctionValue = baseFunction.GetValue(maxVarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        if (t <= MinVarValue) return MinVarFunctionValue;
        if (t >= MaxVarValue) return MaxVarFunctionValue;

        return Argument.GetValue(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Tuple<bool, DifferentialFunction> TrySimplify()
    {
        if (!CanBeSimplified) 
            return new Tuple<bool, DifferentialFunction>(false, this);

        var (argSimplified, arg) = 
            Argument.TrySimplify();

        if (argSimplified)
            return new Tuple<bool, DifferentialFunction>(
                true,
                new DfFiniteSupport(MinVarValue, MaxVarValue, arg, false)
            );

        return new Tuple<bool, DifferentialFunction>(false, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction Simplify()
    {
        if (!CanBeSimplified) return this;
        
        return new DfFiniteSupport(
            MinVarValue,
            MaxVarValue,
            Argument.Simplify(),
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return new DfFiniteSupport(
            MinVarValue,
            MaxVarValue,
            Argument.GetDerivative1().Simplify(),
            false
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction MapArguments(Func<DifferentialFunction, DifferentialFunction> functionMapping)
    {
        return new DfFiniteSupport(
            MinVarValue, 
            MaxVarValue, 
            functionMapping(Argument), 
            true
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction MapArguments(Func<int, DifferentialFunction, DifferentialFunction> functionMapping)
    {
        return new DfFiniteSupport(
            MinVarValue, 
            MaxVarValue, 
            functionMapping(0, Argument), 
            true
        );
    }
}