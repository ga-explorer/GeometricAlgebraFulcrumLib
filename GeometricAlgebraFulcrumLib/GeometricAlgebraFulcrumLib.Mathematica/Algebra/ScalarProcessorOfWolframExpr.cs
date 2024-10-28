using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

namespace GeometricAlgebraFulcrumLib.Mathematica.Algebra;

public sealed class ScalarProcessorOfWolframExpr
    : ISymbolicScalarProcessor<Expr>
{
    public static ScalarProcessorOfWolframExpr Instance { get; }
        = new ScalarProcessorOfWolframExpr();


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


    public Func<Expr, Expr> SimplificationFunc { get; set; }

    public bool IsNumeric
        => false;

    public bool IsSymbolic
        => true;

    public Scalar<Expr> Zero { get; }

    public Scalar<Expr> PositiveInfinity { get; }

    public Scalar<Expr> NegativeInfinity { get; }

    public Scalar<Expr> One { get; }

    public Scalar<Expr> MinusOne { get; }

    public Scalar<Expr> Two { get; }

    public Scalar<Expr> MinusTwo { get; }

    public Scalar<Expr> Ten { get; }

    public Scalar<Expr> MinusTen { get; }

    public Scalar<Expr> Pi { get; }

    public Scalar<Expr> PiTimes2 { get; }

    public Scalar<Expr> PiTimes4 { get; }

    public Scalar<Expr> PiOver2 { get; }

    public Scalar<Expr> E { get; }

    public Scalar<Expr> DegreeToRadianFactor { get; }

    public Scalar<Expr> RadianToDegreeFactor { get; }


    public Expr ZeroValue
        => Expr.INT_ZERO;

    public Expr PositiveInfinityValue { get; }
        = "Infinity".ToExpr();

    public Expr NegativeInfinityValue { get; }
        = "Infinity".ToExpr();

    public Expr OneValue
        => Expr.INT_ONE;

    public Expr MinusOneValue
        => Expr.INT_MINUSONE;

    public Expr TwoValue { get; }
        = 2.ToExpr();

    public Expr MinusTwoValue { get; }
        = (-2).ToExpr();

    public Expr TenValue { get; }
        = 10.ToExpr();

    public Expr MinusTenValue { get; }
        = (-10).ToExpr();

    public Expr PiValue
        => MathematicaInterface.DefaultCasConstants.ExprPi;

    public Expr PiTimes2Value { get; }
        = MathematicaInterface.DefaultCas["2 * Pi"];

    public Expr PiTimes4Value { get; }
        = MathematicaInterface.DefaultCas["4 * Pi"];

    public Expr PiOver2Value { get; }
        = MathematicaInterface.DefaultCas["Pi / 2"];

    public Expr EValue
        => MathematicaInterface.DefaultCasConstants.ExprE;

    public Expr DegreeToRadianFactorValue { get; }
        = MathematicaInterface.DefaultCas["Pi / 180"];

    public Expr RadianToDegreeFactorValue { get; }
        = MathematicaInterface.DefaultCas["180 / Pi"];


    private ScalarProcessorOfWolframExpr()
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
    private Expr PreProcessScalar([NotNull] Expr scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Scalar<Expr> PostProcessScalar([NotNull] Expr scalar)
    {
        var expr =
            SimplificationFunc is null
                ? scalar.Simplify()
                : SimplificationFunc(scalar) ?? scalar;

        if (expr.ToString() == "Indeterminate")
            throw new InvalidDataException();

        return this.ScalarFromValue(expr);
        //return Mfs.Round[Mfs.N[scalar], ZeroEpsilon.ToExpr()].Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Add(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Plus[
            PreProcessScalar(scalar1),
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Subtract(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Subtract[
            PreProcessScalar(scalar1),
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Times(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Times[
            PreProcessScalar(scalar1),
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Divide(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Divide[
            PreProcessScalar(scalar1),
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Positive(Expr scalar)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Negative(Expr scalar)
    {
        return PostProcessScalar(Mfs.Minus[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Inverse(Expr scalar)
    {
        return PostProcessScalar(Mfs.Divide[
            Expr.INT_ONE,
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Sign(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sign[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> UnitStep(Expr scalar)
    {
        return PostProcessScalar(Mfs.UnitStep[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Abs(Expr scalar)
    {
        return PostProcessScalar(Mfs.Abs[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Sqrt(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sqrt[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> SqrtOfAbs(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sqrt[Mfs.Abs[
            PreProcessScalar(scalar)
        ]]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Exp(Expr scalar)
    {
        return PostProcessScalar(Mfs.Exp[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> LogE(Expr scalar)
    {
        return PostProcessScalar(Mfs.Log[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Log2(Expr scalar)
    {
        return PostProcessScalar(Mfs.Log2[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Log10(Expr scalar)
    {
        return PostProcessScalar(Mfs.Log2[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Power(Expr baseScalar, Expr scalar)
    {
        return PostProcessScalar(Mfs.Power[
            PreProcessScalar(baseScalar),
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Log(Expr baseScalar, Expr scalar)
    {
        return PostProcessScalar(Mfs.Log[
            PreProcessScalar(baseScalar),
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Cos(Expr scalar)
    {
        return PostProcessScalar(Mfs.Cos[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Sin(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sin[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Tan(Expr scalar)
    {
        return PostProcessScalar(Mfs.Tan[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Cosh(Expr scalar)
    {
        return PostProcessScalar(Mfs.Cosh[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Sinh(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sinh[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> Tanh(Expr scalar)
    {
        return PostProcessScalar(Mfs.Tanh[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Expr scalar)
    {
        return true;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNumber(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return double.IsFinite(number);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && 
    //           number.IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Expr scalar, bool nearZeroFlag)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && 
    //           nearZeroFlag ? number.IsNearZero(ZeroEpsilon) : number.IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && 
    //           number.IsNearZero(ZeroEpsilon);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && 
    //           !number.IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Expr scalar, bool nearZeroFlag)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && 
    //           nearZeroFlag ? !number.IsNearZero(ZeroEpsilon) : !number.IsZero();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && 
    //           !number.IsNearZero(ZeroEpsilon);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(Expr scalar)
    //{
    //    var number = scalar.ToNumber();

    //    return !double.IsNaN(number) && 
    //           number > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(Expr scalar)
    //{
    //    var number = scalar.ToNumber();

    //    return !double.IsNaN(number) && 
    //           number < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(Expr scalar)
    //{
    //    return !IsPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(Expr scalar)
    //{
    //    return !IsNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && number < -ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(Expr scalar)
    //{
    //    var number = ToFloat64(scalar);

    //    return !double.IsNaN(number) && number > ZeroEpsilon;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromText(string text)
    {
        return this.ScalarFromValue(text.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue(Mfs.Rational[numerator.ToExpr(), denominator.ToExpr()]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return this.ScalarFromValue(value.ToExpr());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(Expr scalar)
    {
        return scalar.ToNumber();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(Expr scalar)
    {
        return scalar.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Expr> VectorToRadians(Expr scalarX, Expr scalarY)
    {
        var scalar = PostProcessScalar(
            Mfs.ArcTan[
                PreProcessScalar(scalarX),
                PreProcessScalar(scalarY)
            ]
        );

        //var float64Value = value.ScalarValue.ToNumber(double.NaN);

        if (scalar.IsNegative()) scalar += PiTimes2;

        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Simplify(Expr scalar)
    {
        return scalar.Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetSymbol(string symbolNameText)
    {
        return symbolNameText.ToSymbolExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr MetaExpressionToScalar(IMetaExpression expression)
    {
        return expression.ToString().ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ScalarToMetaExpression(MetaContext context, Expr scalar)
    {
        return context.ToSymbolicExpression(scalar);
    }
}