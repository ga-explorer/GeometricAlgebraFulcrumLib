using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Processors;

public sealed class ScalarProcessorOfWolframExpr
    : ISymbolicScalarProcessor<Expr>
{
    public static ScalarProcessorOfWolframExpr DefaultProcessor { get; }
        = new ScalarProcessorOfWolframExpr();


    public int RoundingPlaces { get; set; }
        = 13;

    public double ZeroEpsilon 
        => Math.Pow(10, -RoundingPlaces);

    public Func<Expr, Expr> SimplificationFunc { get; set; }

    public bool IsNumeric 
        => false;

    public bool IsSymbolic 
        => true;

        
    public Expr ScalarZero 
        => Expr.INT_ZERO;

    public Expr ScalarOne 
        => Expr.INT_ONE;

    public Expr ScalarMinusOne 
        => Expr.INT_MINUSONE;

    public Expr ScalarTwo { get; } 
        = 2.ToExpr();
        
    public Expr ScalarMinusTwo { get; } 
        = (-2).ToExpr();
        
    public Expr ScalarTen { get; } 
        = 10.ToExpr();
        
    public Expr ScalarMinusTen { get; } 
        = (-10).ToExpr();

    public Expr ScalarPi 
        => MathematicaInterface.DefaultCasConstants.ExprPi;

    public Expr ScalarTwoPi { get; }
        = MathematicaInterface.DefaultCas["2 * Pi"];

    public Expr ScalarPiOver2 { get; }
        = MathematicaInterface.DefaultCas["Pi / 2"];

    public Expr ScalarE 
        => MathematicaInterface.DefaultCasConstants.ExprE;

    public Expr ScalarDegreeToRadian { get; }
        = MathematicaInterface.DefaultCas["Pi / 180"];

    public Expr ScalarRadianToDegree { get; }
        = MathematicaInterface.DefaultCas["180 / Pi"];


    private ScalarProcessorOfWolframExpr()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Expr PreProcessScalar([NotNull] Expr scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Expr PostProcessScalar([NotNull] Expr scalar)
    {
        var expr = 
            SimplificationFunc is null 
                ? scalar.Simplify() 
                : SimplificationFunc(scalar) ?? scalar;

        if (expr.ToString() == "Indeterminate")
            throw new InvalidDataException();

        return expr;
        //return Mfs.Round[Mfs.N[scalar], ZeroEpsilon.ToExpr()].Simplify();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Add(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Plus[
            PreProcessScalar(scalar1), 
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Subtract(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Subtract[
            PreProcessScalar(scalar1), 
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Times(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Times[
            PreProcessScalar(scalar1), 
            PreProcessScalar(scalar2)
        ]);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Times(IntegerSign sign, Expr scalar)
    {
        if (sign.IsZero) return ScalarZero;

        return sign.IsPositive
            ? scalar 
            : Negative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr NegativeTimes(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Minus[Mfs.Times[
            PreProcessScalar(scalar1), 
            PreProcessScalar(scalar2)
        ]]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Divide(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Divide[
            PreProcessScalar(scalar1), 
            PreProcessScalar(scalar2)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr NegativeDivide(Expr scalar1, Expr scalar2)
    {
        return PostProcessScalar(Mfs.Minus[Mfs.Divide[
            PreProcessScalar(scalar1), 
            PreProcessScalar(scalar2)
        ]]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Positive(Expr scalar)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Negative(Expr scalar)
    {
        return PostProcessScalar(Mfs.Minus[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Inverse(Expr scalar)
    {
        return PostProcessScalar(Mfs.Divide[
            Expr.INT_ONE, 
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Sign(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sign[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr UnitStep(Expr scalar)
    {
        return PostProcessScalar(Mfs.UnitStep[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Abs(Expr scalar)
    {
        return PostProcessScalar(Mfs.Abs[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Sqrt(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sqrt[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr SqrtOfAbs(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sqrt[Mfs.Abs[
            PreProcessScalar(scalar)
        ]]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Exp(Expr scalar)
    {
        return PostProcessScalar(Mfs.Exp[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr LogE(Expr scalar)
    {
        return PostProcessScalar(Mfs.Log[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Log2(Expr scalar)
    {
        return PostProcessScalar(Mfs.Log2[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Log10(Expr scalar)
    {
        return PostProcessScalar(Mfs.Log2[
            PreProcessScalar(scalar)
        ]);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Power(Expr baseScalar, Expr scalar)
    {
        return PostProcessScalar(Mfs.Power[
            PreProcessScalar(baseScalar),
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Log(Expr baseScalar, Expr scalar)
    {
        return PostProcessScalar(Mfs.Log[
            PreProcessScalar(baseScalar),
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Cos(Expr scalar)
    {
        return PostProcessScalar(Mfs.Cos[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Sin(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sin[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Tan(Expr scalar)
    {
        return PostProcessScalar(Mfs.Tan[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr ArcCos(Expr scalar)
    {
        return PostProcessScalar(Mfs.ArcCos[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr ArcSin(Expr scalar)
    {
        return PostProcessScalar(Mfs.ArcSin[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr ArcTan(Expr scalar)
    {
        return PostProcessScalar(Mfs.ArcTan[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr ArcTan2(Expr scalarX, Expr scalarY)
    {
        return PostProcessScalar(Mfs.ArcTan[
            PreProcessScalar(scalarX), 
            PreProcessScalar(scalarY)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Cosh(Expr scalar)
    {
        return PostProcessScalar(Mfs.Cosh[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Sinh(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sinh[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Tanh(Expr scalar)
    {
        return PostProcessScalar(Mfs.Tanh[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr Sinc(Expr scalar)
    {
        return PostProcessScalar(Mfs.Sinc[
            PreProcessScalar(scalar)
        ]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Expr scalar)
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFiniteNumber(Expr scalar)
    {
        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return double.IsFinite(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(Expr scalar)
    {
        return scalar.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(Expr scalar, bool nearZeroFlag)
    {
        if (scalar.IsZero())
            return true;

        if (!nearZeroFlag)
            return false;

        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return number > -ZeroEpsilon && number < ZeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(Expr scalar)
    {
        if (scalar.IsZero())
            return true;

        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return number > -ZeroEpsilon && number < ZeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(Expr scalar)
    {
        return !IsZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(Expr scalar, bool nearZeroFlag)
    {
        return !IsZero(scalar, nearZeroFlag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearZero(Expr scalar)
    {
        return !IsNearZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPositive(Expr scalar)
    {
        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return number > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegative(Expr scalar)
    {
        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return number < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotPositive(Expr scalar)
    {
        return !IsPositive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNegative(Expr scalar)
    {
        return !IsNegative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearPositive(Expr scalar)
    {
        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return number < -ZeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearNegative(Expr scalar)
    {
        if (!scalar.NumberQ())
            return false;

        var number = 
            scalar.ToNumber();

        return number > ZeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromText(string text)
    {
        return text.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromNumber(int value)
    {
        return value.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromNumber(uint value)
    {
        return value.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromNumber(long value)
    {
        return value.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromNumber(ulong value)
    {
        return value.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromNumber(float value)
    {
        return value.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromNumber(double value)
    {
        return value.ToExpr();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromRational(long numerator, long denominator)
    {
        return Mfs.Rational[numerator.ToExpr(), denominator.ToExpr()];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Expr GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return value.ToExpr(); 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(Expr scalar)
    {
        return scalar.ToString();
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