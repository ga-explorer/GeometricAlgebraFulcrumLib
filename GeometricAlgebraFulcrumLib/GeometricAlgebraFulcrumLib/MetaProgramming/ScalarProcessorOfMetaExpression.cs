using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Numbers;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MetaProgramming;

/// <summary>
/// This processor performs basic operations on symbolic expressions
/// of all kinds. This processor only constructs new expressions without
/// adding or querying data of the associated Context object
/// </summary>
public class ScalarProcessorOfMetaExpression :
    ISymbolicScalarProcessor<IMetaExpression>
{
    public MetaContext Context { get; }

    public bool IsNumeric 
        => false;

    public bool IsSymbolic 
        => true;

    public IMetaExpression ScalarZero
        => Context.ScalarZero;

    public IMetaExpression ScalarOne
        => Context.ScalarOne;

    public IMetaExpression ScalarMinusOne
        => Context.ScalarMinusOne;

    public IMetaExpression ScalarTwo
        => Context.ScalarTwo;

    public IMetaExpression ScalarMinusTwo
        => Context.ScalarMinusOne;

    public IMetaExpression ScalarTen
        => Context.ScalarTen;

    public IMetaExpression ScalarMinusTen
        => Context.ScalarMinusTen;

    public IMetaExpression ScalarPi
        => Context.ScalarPi;

    public IMetaExpression ScalarTwoPi 
        => Context.ScalarTwoPi;

    public IMetaExpression ScalarPiOver2 
        => Context.ScalarPiOver2;

    public IMetaExpression ScalarE
        => Context.ScalarE;

    public IMetaExpression ScalarDegreeToRadian
        => Context.ScalarDegreeToRadian;

    public IMetaExpression ScalarRadianToDegree 
        => Context.ScalarRadianToDegree;


    internal ScalarProcessorOfMetaExpression(MetaContext context)
    {
        Context = context;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Add(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Plus
            .CreateFunction(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Subtract(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Subtract
            .CreateFunction(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Times(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Times
            .CreateFunction(scalar1, scalar2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Times(IntegerSign sign, IMetaExpression scalar)
    {
        if (sign.IsZero) return ScalarZero;

        return sign.IsPositive
            ? scalar 
            : Negative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression NegativeTimes(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Negative(Times(scalar1, scalar2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Divide(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Divide
            .CreateFunction(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression NegativeDivide(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Negative(Divide(scalar1, scalar2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Positive(IMetaExpression scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Negative(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Negative
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Inverse(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Inverse
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Sign(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sign
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression UnitStep(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .UnitStep
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Abs(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Abs
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Sqrt(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sqrt
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression SqrtOfAbs(IMetaExpression scalar)
    {
        return Sqrt(Abs(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Exp(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Exp
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression LogE(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Log2(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log2
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Log10(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log10
            .CreateFunction(scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Power(IMetaExpression baseScalar, IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Power
            .CreateFunction(baseScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Log(IMetaExpression baseScalar, IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log
            .CreateFunction(baseScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Cos(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Cos
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Sin(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sin
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Tan(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Tan
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ArcCos(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .ArcCos
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ArcSin(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .ArcSin
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ArcTan(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .ArcTan
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ArcTan2(IMetaExpression scalarX, IMetaExpression scalarY)
    {
        return Context
            .FunctionHeadSpecsFactory
            .ArcTan2
            .CreateFunction(scalarX, scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Cosh(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Cosh
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Sinh(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sinh
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Tanh(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Tanh
            .CreateFunction(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Sinc(IMetaExpression scalar)
    {
        return IsZero(scalar) 
            ? ScalarOne 
            : Divide(Sin(scalar), scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(IMetaExpression scalar)
    {
        return scalar is not null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFiniteNumber(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsFiniteNumber: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsZero: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(IMetaExpression scalar, bool nearZeroFlag)
    {
        if (scalar is not IMetaExpressionNumber number)
            return false;

        return nearZeroFlag 
            ? number.IsNearZero 
            : number.IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsNearZero: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsZero: false};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(IMetaExpression scalar, bool nearZeroFlag)
    {
        if (scalar is not IMetaExpressionNumber number)
            return false;

        return nearZeroFlag 
            ? !number.IsNearZero 
            : !number.IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearZero(IMetaExpression scalar)
    {
        if (scalar is not IMetaExpressionNumber number)
            return false;

        return !number.IsNearZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPositive(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsPositive: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegative(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsNegative: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotPositive(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsPositive: false};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNegative(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsNegative: false};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearPositive(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsNotNearPositive: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearNegative(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber {IsNotNearNegative: true};
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromText(string text)
    {
        throw new System.NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromNumber(int value)
    {
        return MetaExpressionNumber.Create(Context, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromNumber(uint value)
    {
        return MetaExpressionNumber.Create(Context, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromNumber(long value)
    {
        return MetaExpressionNumber.Create(Context, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromNumber(ulong value)
    {
        return MetaExpressionNumber.Create(Context, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromNumber(float value)
    {
        return MetaExpressionNumber.Create(Context, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromNumber(double value)
    {
        return MetaExpressionNumber.Create(Context, value);
    }

    public IMetaExpression GetScalarFromRational(long numerator, long denominator)
    {
        return MetaExpressionNumber.CreateRational(Context, numerator, denominator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
    {
        var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(IMetaExpression scalar)
    {
        return scalar.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(IMetaExpression scalar)
    {
        throw new System.NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetSymbol(string symbolNameText)
    {
        throw new System.NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression MetaExpressionToScalar(IMetaExpression expression)
    {
        return expression;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression ScalarToMetaExpression(MetaContext context, IMetaExpression scalar)
    {
        return scalar;
    }
}