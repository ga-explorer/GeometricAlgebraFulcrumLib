using System;
using System.Runtime.CompilerServices;
using AngouriMath;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

public sealed class ScalarProcessorOfAngouriMathEntity
    : ISymbolicScalarProcessor<Entity>
{
    public static ScalarProcessorOfAngouriMathEntity DefaultProcessor { get; }
        = new ScalarProcessorOfAngouriMathEntity();

        
    public Entity ScalarZero 
        => Entity.Number.Integer.Zero;

    public Entity ScalarOne
        => Entity.Number.Integer.One;

    public Entity ScalarMinusOne 
        => Entity.Number.Integer.MinusOne;

    public Entity ScalarTwo 
        => MathS.Numbers.Create(2);

    public Entity ScalarMinusTwo 
        => MathS.Numbers.Create(-2);

    public Entity ScalarTen 
        => MathS.Numbers.Create(10);

    public Entity ScalarMinusTen 
        => MathS.Numbers.Create(-10);

    public Entity ScalarPi 
        => MathS.pi;

    public Entity ScalarTwoPi 
        => MathS.pi * 2;

    public Entity ScalarPiOver2 
        => MathS.pi / 2;

    public Entity ScalarE 
        => MathS.e;

    public Entity ScalarDegreeToRadian 
        => MathS.pi / 180;

    public Entity ScalarRadianToDegree 
        => 180 / MathS.pi;

    public bool IsNumeric 
        => false;

    public bool IsSymbolic 
        => true;

    public int RoundingPlaces { get; set; }
        = 13;

    public double ZeroEpsilon 
        => Math.Pow(10, -RoundingPlaces);


    private ScalarProcessorOfAngouriMathEntity()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity PreProcessScalar(Entity scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity PostProcessScalar(Entity scalar)
    {
        return scalar.Simplify();
        //return Mfs.Round[Mfs.N[scalar], ZeroEpsilon.ToExpr()].Simplify();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Add(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) + 
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Subtract(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) - 
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Times(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) * 
            PreProcessScalar(scalar2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Times(IntegerSign sign, Entity scalar)
    {
        if (sign.IsZero) return ScalarZero;

        return sign.IsPositive
            ? scalar 
            : Negative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity NegativeTimes(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            -(PreProcessScalar(scalar1) * PreProcessScalar(scalar2))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Divide(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            PreProcessScalar(scalar1) / 
            PreProcessScalar(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity NegativeDivide(Entity scalar1, Entity scalar2)
    {
        return PostProcessScalar(
            -(PreProcessScalar(scalar1) / PreProcessScalar(scalar2))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Positive(Entity scalar)
    {
        return PostProcessScalar(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Negative(Entity scalar)
    {
        return PostProcessScalar(
            -PreProcessScalar(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Inverse(Entity scalar)
    {
        return PostProcessScalar(
            ScalarOne / PreProcessScalar(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Sign(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Signum(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity UnitStep(Entity scalar)
    {
        return PostProcessScalar(
            (1 + MathS.Signum(PreProcessScalar(scalar))) / 2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Abs(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Abs(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Sqrt(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Sqrt(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity SqrtOfAbs(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Sqrt(MathS.Abs(PreProcessScalar(scalar)))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Exp(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Pow(MathS.e, PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity LogE(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Ln(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Log2(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Log(ScalarTwo, PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Log10(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Log(PreProcessScalar(scalar))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Power(Entity baseScalar, Entity scalar)
    {
        return PostProcessScalar(
            MathS.Pow(PreProcessScalar(baseScalar), PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Log(Entity baseScalar, Entity scalar)
    {
        return PostProcessScalar(
            MathS.Log(PreProcessScalar(baseScalar), PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Cos(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Cos(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Sin(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Sin(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Tan(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Tan(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity ArcCos(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Arccos(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity ArcSin(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Arcsin(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity ArcTan(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Arctan(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity ArcTan2(Entity scalarX, Entity scalarY)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Cosh(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Hyperbolic.Cosh(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Sinh(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Hyperbolic.Sinh(PreProcessScalar(scalar))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Tanh(Entity scalar)
    {
        return PostProcessScalar(
            MathS.Hyperbolic.Tanh(PreProcessScalar(scalar))
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Sinc(Entity scalar)
    {
        if (IsZero(scalar))
            return ScalarOne;

        var s = PreProcessScalar(scalar);
            
        return PostProcessScalar(
            MathS.Sin(s) / s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Entity scalar)
    {
        return scalar is not null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFiniteNumber(Entity scalar)
    {
        var simpleScalar = scalar.Simplify();
            
        return simpleScalar.EvaluableNumerical && 
               simpleScalar.EvalNumerical().IsFinite;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(Entity scalar)
    {
        var simpleScalar = scalar.Simplify();
            
        return simpleScalar.EvaluableNumerical && 
               simpleScalar.EvalNumerical().IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(Entity scalar, bool nearZeroFlag)
    {
        var simpleScalar = scalar.Simplify();

        return 
            simpleScalar.EvaluableNumerical && 
            nearZeroFlag 
                ? simpleScalar.EvalNumerical().Abs() < ZeroEpsilon
                : simpleScalar.EvalNumerical().IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(Entity scalar)
    {
        var simpleScalar = scalar.Simplify();

        return simpleScalar.EvaluableNumerical && 
               simpleScalar.EvalNumerical().Abs() < ZeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(Entity scalar)
    {
        return !IsZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(Entity scalar, bool nearZeroFlag)
    {
        return !IsZero(scalar, nearZeroFlag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearZero(Entity scalar)
    {
        return !IsNearZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPositive(Entity scalar)
    {
        var simpleScalar = scalar.Simplify();

        return simpleScalar.EvaluableNumerical && 
               simpleScalar.EvalNumerical().RealPart.IsPositive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegative(Entity scalar)
    {
        var simpleScalar = scalar.Simplify();

        return simpleScalar.EvaluableNumerical && 
               simpleScalar.EvalNumerical().RealPart.IsNegative;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotPositive(Entity scalar)
    {
        return !IsPositive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNegative(Entity scalar)
    {
        return !IsNegative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearPositive(Entity scalar)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearNegative(Entity scalar)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromText(string text)
    {
        return MathS.FromString(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromNumber(int value)
    {
        return MathS.Numbers.Create(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromNumber(uint value)
    {
        return MathS.Numbers.Create(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromNumber(long value)
    {
        return MathS.Numbers.Create(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromNumber(ulong value)
    {
        return MathS.Numbers.Create((long) value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromNumber(float value)
    {
        return MathS.Numbers.Create(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromNumber(double value)
    {
        return MathS.Numbers.Create(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromRational(long numerator, long denominator)
    {
        return MathS.Numbers.CreateRational(numerator, denominator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return MathS.Numbers.Create(value);
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