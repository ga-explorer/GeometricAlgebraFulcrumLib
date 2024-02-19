using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Visitors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

public abstract class DifferentialFunction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator DifferentialFunction(double value)
    {
        if (value.IsNaNOrInfinite())
            throw new ArgumentException(nameof(value));

        return value switch
        {
            0d => DfConstant.Zero,
            1d => DfConstant.One,
            _ => DfConstant.Create(value)
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator -(DifferentialFunction f1)
    {
        return DfTimes.Create(-1, f1).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator +(DifferentialFunction f1, double f2)
    {
        return DfPlus.Create(f1, f2).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator +(double f1, DifferentialFunction f2)
    {
        return DfPlus.Create(f1, f2).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator +(DifferentialFunction f1, DifferentialFunction f2)
    {
        return DfPlus.Create(f1, f2).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator -(DifferentialFunction f1, double f2)
    {
        return DfPlus.Create(f1, -f2).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator -(double f1, DifferentialFunction f2)
    {
        return DfPlus.Create(
            f1, 
            DfTimes.Create(-1, f2)
        ).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator -(DifferentialFunction f1, DifferentialFunction f2)
    {
        return DfPlus.Create(
            f1, 
            DfTimes.Create(-1, f2)
        ).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator *(DifferentialFunction f1, double f2)
    {
        return DfTimes.Create(f2, f1).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator *(double f1, DifferentialFunction f2)
    {
        return DfTimes.Create(f1, f2).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator *(DifferentialFunction f1, DifferentialFunction f2)
    {
        return DfTimes.Create(f1, f2).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator /(DifferentialFunction f1, double f2)
    {
        return DfTimes.Create(1d / f2, f1).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator /(double f1, DifferentialFunction f2)
    {
        return DfTimes.Create(
            f1,
            DfPowerScalar.Create(f2, -1)
        ).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DifferentialFunction operator /(DifferentialFunction f1, DifferentialFunction f2)
    {
        return DfTimes.Create(
            f1, 
            DfPowerScalar.Create(f2, -1)
        ).Simplify();
    }


    public string LaTeXName { get; set; } = string.Empty;

    public abstract int TreeDepth { get; }

    public abstract bool HasArguments { get; }
        
    public abstract int ArgumentCount { get; }

    public abstract IReadOnlyList<DifferentialFunction> Arguments { get; }

    public abstract bool CanBeSimplified { get; }

    public abstract bool IsBasic { get; }

    public abstract bool IsConstant { get; }

    public bool IsConstantZero =>
        this is DfConstant { IsZero: true };

    public bool IsConstantOne =>
        this is DfConstant { IsOne: true };

    public abstract bool IsComposite { get; }

    public abstract bool IsUnary { get; }

    public abstract bool IsBinary { get; }

    public abstract bool IsNary { get; }


    public abstract double GetValue(double t);

    public abstract Tuple<bool, DifferentialFunction> TrySimplify();

    public abstract DifferentialFunction Simplify();

    public abstract DifferentialFunction GetDerivative1();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction GetDerivative2()
    {
        return GetDerivativeN(2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction GetDerivative3()
    {
        return GetDerivativeN(3);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction GetDerivative4()
    {
        return GetDerivativeN(4);
    }

    public virtual DifferentialFunction GetDerivativeN(int order)
    {
        if (order < 1) 
            throw new ArgumentException(nameof(order));

        var df = Simplify();

        while (order > 0)
        {
            df = df.GetDerivative1().Simplify();

            order--;
        }

        return df;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Pair<DifferentialFunction> GetDerivatives2()
    {
        var dfArray = GetDerivatives(2).ToImmutableArray();

        return new Pair<DifferentialFunction>(
            dfArray[0],
            dfArray[1]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Triplet<DifferentialFunction> GetDerivatives3()
    {
        var dfArray = GetDerivatives(3).ToImmutableArray();

        return new Triplet<DifferentialFunction>(
            dfArray[0],
            dfArray[1],
            dfArray[2]
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Quad<DifferentialFunction> GetDerivatives4()
    {
        var dfArray = GetDerivatives(4).ToImmutableArray();

        return new Quad<DifferentialFunction>(
            dfArray[0],
            dfArray[1],
            dfArray[2],
            dfArray[3]
        );
    }

    public virtual IEnumerable<DifferentialFunction> GetDerivatives(int maxOrder)
    {
        if (maxOrder < 1) 
            throw new ArgumentException(nameof(maxOrder));

        var df = Simplify();

        while (maxOrder > 0)
        {
            df = df.GetDerivative1().Simplify();

            yield return df;
                
            maxOrder--;
        }
    }

    public virtual bool IsSame(DifferentialFunction f)
    {
        if (ArgumentCount != f.ArgumentCount) return false;

        return ToString() == f.ToString();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Sin()
    {
        return DfSin.Create(this).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Cos()
    {
        return DfCos.Create(this).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Exp()
    {
        return DfExp.Create(this).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Inverse()
    {
        return DfPowerScalar.Create(this, -1).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Square()
    {
        return DfPowerScalar.Create(this, 2).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Cube()
    {
        return DfPowerScalar.Create(this, 3).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction SquareRoot()
    {
        return DfPowerScalar.Create(this, 0.5d).Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction CubeRoot()
    {
        return DfPowerScalar.Create(this, 1d / 3d).Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual DifferentialFunction Power(double powerValue)
    {
        return DfPowerScalar.Create(this, powerValue).Simplify();
    }


    public override string ToString()
    {
        return this.AcceptVisitor(
            MathematicaStringVisitor.DefaultVisitor
        );
    }
}