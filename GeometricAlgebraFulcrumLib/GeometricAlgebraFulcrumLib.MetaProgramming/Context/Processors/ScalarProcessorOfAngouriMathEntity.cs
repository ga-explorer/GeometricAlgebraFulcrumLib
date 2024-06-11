using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;

public sealed class ScalarProcessorOfAngouriMathEntity
    : ISymbolicScalarProcessor<Entity>
{
    public static ScalarProcessorOfAngouriMathEntity Instance { get; }
        = new ScalarProcessorOfAngouriMathEntity();


    private double _zeroEpsilon = 1e-12;
    public double ZeroEpsilon
    {
        get => _zeroEpsilon;
        set
        {
            if (value.IsNaN() || value.Abs() > 1)
                throw new ArgumentException(nameof(value));

            _zeroEpsilon = value.Abs();
        }
    }

    public Entity ZeroValue
        => Entity.Number.Integer.Zero;

    public Entity PositiveInfinityValue { get; }
        = MathS.Numbers.Create(double.PositiveInfinity);

    public Entity NegativeInfinityValue { get; }
        = MathS.Numbers.Create(double.NegativeInfinity);

    public Entity OneValue
        => Entity.Number.Integer.One;

    public Entity MinusOneValue
        => Entity.Number.Integer.MinusOne;

    public Entity TwoValue { get; }
        = MathS.Numbers.Create(2);

    public Entity MinusTwoValue { get; }
        = MathS.Numbers.Create(-2);

    public Entity TenValue { get; }
        = MathS.Numbers.Create(10);

    public Entity MinusTenValue { get; }
        = MathS.Numbers.Create(-10);

    public Entity PiValue
        => MathS.pi;

    public Entity PiTimes2Value { get; }
        = MathS.pi * 2;

    public Entity PiTimes4Value { get; }
        = MathS.pi * 4;

    public Entity PiOver2Value { get; }
        = MathS.pi / 2;

    public Entity EValue
        => MathS.e;

    public Entity DegreeToRadianFactorValue { get; }
        = MathS.pi / 180;

    public Entity RadianToDegreeFactorValue { get; }
        = 180 / MathS.pi;

    public bool IsNumeric
        => false;

    public bool IsSymbolic
        => true;

    public Scalar<Entity> Zero { get; }

    public Scalar<Entity> PositiveInfinity { get; }

    public Scalar<Entity> NegativeInfinity { get; }

    public Scalar<Entity> One { get; }

    public Scalar<Entity> MinusOne { get; }

    public Scalar<Entity> Two { get; }

    public Scalar<Entity> MinusTwo { get; }

    public Scalar<Entity> Ten { get; }

    public Scalar<Entity> MinusTen { get; }

    public Scalar<Entity> Pi { get; }

    public Scalar<Entity> PiTimes2 { get; }

    public Scalar<Entity> PiTimes4 { get; }

    public Scalar<Entity> PiOver2 { get; }

    public Scalar<Entity> E { get; }

    public Scalar<Entity> DegreeToRadianFactor { get; }

    public Scalar<Entity> RadianToDegreeFactor { get; }


    private ScalarProcessorOfAngouriMathEntity()
    {
        Zero = this.ScalarFromValue(ZeroValue);
        One = this.ScalarFromValue(OneValue);
        MinusOne = this.ScalarFromValue(MinusOneValue);
        Two = this.ScalarFromValue(TwoValue);
        MinusTwo = this.ScalarFromValue(MinusTwoValue);
        Ten = this.ScalarFromValue(TenValue);
        MinusTen = this.ScalarFromValue(MinusTenValue);
        Pi = this.ScalarFromValue(PiValue);
        E = this.ScalarFromValue(EValue);
        PiTimes2 = this.ScalarFromValue(PiTimes2Value);
        PiTimes4 = this.ScalarFromValue(PiTimes4Value);
        PiOver2 = this.ScalarFromValue(PiOver2Value);
        DegreeToRadianFactor = this.ScalarFromValue(DegreeToRadianFactorValue);
        RadianToDegreeFactor = this.ScalarFromValue(RadianToDegreeFactorValue);
        PositiveInfinity = this.ScalarFromValue(PositiveInfinityValue);
        NegativeInfinity = this.ScalarFromValue(NegativeInfinityValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity PreProcessScalar(Entity scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> PostProcessScalar(Entity scalar)
    {
        return scalar.Simplify().ScalarFromValue(this);
        //return Mfs.Round[Mfs.N[scalar], ZeroEpsilon.ToExpr()].Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Add(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) +
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Subtract(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) -
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Times(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) *
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Divide(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) /
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> VectorToRadians(Entity scalarX, Entity scalarY)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Positive(Entity scalar)
    {
        return PostProcessScalar(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Negative(Entity scalar)
    {
        return PostProcessScalar(
            -PreProcessScalar(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Inverse(Entity scalar)
    {
        return PostProcessScalar(
            OneValue / PreProcessScalar(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Sign(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Signum(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> UnitStep(Entity scalar)
    {
        return PostProcessScalar(
            (1 + MathS.Signum(PreProcessScalar(scalar))) / 2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Abs(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Abs(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Sqrt(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Sqrt(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> SqrtOfAbs(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Sqrt(MathS.Abs(PreProcessScalar(scalar)))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Exp(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Pow(MathS.e, PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> LogE(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Ln(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Log2(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Log(TwoValue, PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Log10(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Log(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Power(Entity baseScalar, Entity scalar)
    {
        return PostProcessScalar(
            MathS.Pow(PreProcessScalar(baseScalar), PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Log(Entity baseScalar, Entity scalar)
    {
        return PostProcessScalar(
            MathS.Log(PreProcessScalar(baseScalar), PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Cos(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Cos(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Sin(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Sin(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Tan(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Tan(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Cosh(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Hyperbolic.Cosh(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Sinh(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Hyperbolic.Sinh(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> Tanh(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Hyperbolic.Tanh(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Entity scalar)
    {
        return scalar is not null;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(Entity scalar)
    //{
    //    var simpleScalar = scalar.Simplify();

    //    return simpleScalar.EvaluableNumerical && 
    //           simpleScalar.EvalNumerical().IsFinite;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Entity scalar)
    //{
    //    var simpleScalar = scalar.Simplify();

    //    return simpleScalar.EvaluableNumerical && 
    //           simpleScalar.EvalNumerical().IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Entity scalar, bool nearZeroFlag)
    //{
    //    var simpleScalar = scalar.Simplify();

    //    return 
    //        simpleScalar.EvaluableNumerical && 
    //        nearZeroFlag 
    //            ? simpleScalar.EvalNumerical().Abs() < ZeroEpsilon
    //            : simpleScalar.EvalNumerical().IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(Entity scalar)
    //{
    //    var simpleScalar = scalar.Simplify();

    //    return simpleScalar.EvaluableNumerical && 
    //           simpleScalar.EvalNumerical().Abs() < ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Entity scalar)
    //{
    //    return !IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Entity scalar, bool nearZeroFlag)
    //{
    //    return !IsZero(scalar, nearZeroFlag);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(Entity scalar)
    //{
    //    return !IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(Entity scalar)
    //{
    //    var simpleScalar = scalar.Simplify();

    //    return simpleScalar.EvaluableNumerical && 
    //           simpleScalar.EvalNumerical().RealPart.IsPositive;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(Entity scalar)
    //{
    //    var simpleScalar = scalar.Simplify();

    //    return simpleScalar.EvaluableNumerical && 
    //           simpleScalar.EvalNumerical().RealPart.IsNegative;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(Entity scalar)
    //{
    //    return !IsPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(Entity scalar)
    //{
    //    return !IsNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(Entity scalar)
    //{
    //    throw new NotImplementedException();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(Entity scalar)
    //{
    //    throw new NotImplementedException();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromText(string text)
    {
        return MathS.FromString(text).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromNumber(int value)
    {
        return MathS.Numbers.Create(value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromNumber(uint value)
    {
        return MathS.Numbers.Create(value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromNumber(long value)
    {
        return MathS.Numbers.Create(value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromNumber(ulong value)
    {
        return MathS.Numbers.Create((long)value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromNumber(float value)
    {
        return MathS.Numbers.Create(value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromNumber(double value)
    {
        return MathS.Numbers.Create(value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromRational(long numerator, long denominator)
    {
        return MathS.Numbers.CreateRational(numerator, denominator).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Entity> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return MathS.Numbers.Create(value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(Entity scalar)
    {
        if (!scalar.EvaluableNumerical) return double.NaN;

        var complexNumber = scalar.EvalNumerical();

        if (!complexNumber.ImaginaryPart.IsZero) return double.NaN;

        return complexNumber.RealPart.AsDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(Entity scalar)
    {
        return scalar.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Simplify(Entity scalar)
    {
        return scalar.Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetSymbol(string symbolNameText)
    {
        return MathS.Var(symbolNameText);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity MetaExpressionToScalar(IMetaExpression expression)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ScalarToMetaExpression(MetaContext context, Entity scalar)
    {
        throw new NotImplementedException();
    }
}