using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;

/// <summary>
/// This processor performs basic operations on symbolic expressions
/// of all kinds. This processor only constructs new expressions without
/// adding or querying data of the associated Context object
/// </summary>
public class ScalarProcessorOfMetaExpression :
    ISymbolicScalarProcessor<IMetaExpression>
{
    public MetaContext Context { get; }

    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor 
        => Context.ScalarProcessor;

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

    public bool IsNumeric 
        => false;

    public bool IsSymbolic 
        => true;

    public Scalar<IMetaExpression> Zero { get; }

    public Scalar<IMetaExpression> PositiveInfinity { get; }
    
    public Scalar<IMetaExpression> NegativeInfinity { get; }

    public Scalar<IMetaExpression> One { get; }
    
    public Scalar<IMetaExpression> MinusOne { get; }
    
    public Scalar<IMetaExpression> Two { get; }
    
    public Scalar<IMetaExpression> MinusTwo { get; }
    
    public Scalar<IMetaExpression> Ten { get; }
    
    public Scalar<IMetaExpression> MinusTen { get; }
    
    public Scalar<IMetaExpression> Pi { get; }
    
    public Scalar<IMetaExpression> PiTimes2 { get; }
    
    public Scalar<IMetaExpression> PiTimes4 { get; }

    public Scalar<IMetaExpression> PiOver2 { get; }
    
    public Scalar<IMetaExpression> E { get; }
    
    public Scalar<IMetaExpression> DegreeToRadianFactor { get; }
    
    public Scalar<IMetaExpression> RadianToDegreeFactor { get; }

    public IMetaExpression ZeroValue
        => Context.ZeroValue;

    public IMetaExpression PositiveInfinityValue 
        => Context.PositiveInfinityValue;

    public IMetaExpression NegativeInfinityValue 
        => Context.NegativeInfinityValue;

    public IMetaExpression OneValue
        => Context.OneValue;

    public IMetaExpression MinusOneValue
        => Context.MinusOneValue;

    public IMetaExpression TwoValue
        => Context.TwoValue;

    public IMetaExpression MinusTwoValue
        => Context.MinusOneValue;

    public IMetaExpression TenValue
        => Context.TenValue;

    public IMetaExpression MinusTenValue
        => Context.MinusTenValue;

    public IMetaExpression PiValue
        => Context.PiValue;

    public IMetaExpression PiTimes2Value 
        => Context.PiTimes2Value;
    
    public IMetaExpression PiTimes4Value 
        => Context.PiTimes4Value;

    public IMetaExpression PiOver2Value 
        => Context.PiOver2Value;

    public IMetaExpression EValue
        => Context.EValue;

    public IMetaExpression DegreeToRadianFactorValue
        => Context.DegreeToRadianFactorValue;

    public IMetaExpression RadianToDegreeFactorValue 
        => Context.RadianToDegreeFactorValue;


    internal ScalarProcessorOfMetaExpression(MetaContext context)
    {
        Context = context;

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
    public Scalar<IMetaExpression> Add(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Plus
            .CreateFunction(Context, scalar1, scalar2)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Subtract(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Subtract
            .CreateFunction(Context, scalar1, scalar2)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Times(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Times
            .CreateFunction(Context, scalar1, scalar2)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Divide(IMetaExpression scalar1, IMetaExpression scalar2)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Divide
            .CreateFunction(Context, scalar1, scalar2)
            .ScalarFromValue(this);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> VectorToRadians(IMetaExpression scalarX, IMetaExpression scalarY)
    {
        return Context
            .FunctionHeadSpecsFactory
            .VectorToRadians
            .CreateFunction(Context, scalarX, scalarY)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Positive(IMetaExpression scalar)
    {
        return scalar.ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Negative(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Negative
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Inverse(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Inverse
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Sign(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sign
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> UnitStep(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .UnitStep
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Abs(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Abs
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Sqrt(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sqrt
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> SqrtOfAbs(IMetaExpression scalar)
    {
        return Abs(scalar).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Exp(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Exp
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> LogE(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Log2(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log2
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Log10(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log10
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Power(IMetaExpression baseScalar, IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Power
            .CreateFunction(Context, baseScalar, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Log(IMetaExpression baseScalar, IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Log
            .CreateFunction(Context, baseScalar, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Cos(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Cos
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Sin(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sin
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Tan(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Tan
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Cosh(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Cosh
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Sinh(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Sinh
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> Tanh(IMetaExpression scalar)
    {
        return Context
            .FunctionHeadSpecsFactory
            .Tanh
            .CreateFunction(Context, scalar)
            .ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(IMetaExpression scalar)
    {
        return scalar is not null;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsFiniteNumber: true};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsZero: true};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(IMetaExpression scalar, bool nearZeroFlag)
    //{
    //    if (scalar is not IMetaExpressionNumber number)
    //        return false;

    //    return nearZeroFlag 
    //        ? number.IsNearZero 
    //        : number.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsNearZero: true};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsZero: false};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(IMetaExpression scalar, bool nearZeroFlag)
    //{
    //    if (scalar is not IMetaExpressionNumber number)
    //        return false;

    //    return nearZeroFlag 
    //        ? !number.IsNearZero 
    //        : !number.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(IMetaExpression scalar)
    //{
    //    if (scalar is not IMetaExpressionNumber number)
    //        return false;

    //    return !number.IsNearZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsPositive: true};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsNegative: true};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsPositive: false};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsNegative: false};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsNotNearPositive: true};
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(IMetaExpression scalar)
    //{
    //    return scalar is IMetaExpressionNumber {IsNotNearNegative: true};
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromText(string text)
    {
        var scalarValue = double.TryParse(text, out var value) ? value : double.NaN;

        return MetaExpressionNumber.Create(Context, scalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromNumber(int value)
    {
        return MetaExpressionNumber.Create(Context, value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromNumber(uint value)
    {
        return MetaExpressionNumber.Create(Context, value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromNumber(long value)
    {
        return MetaExpressionNumber.Create(Context, value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromNumber(ulong value)
    {
        return MetaExpressionNumber.Create(Context, value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromNumber(float value)
    {
        return MetaExpressionNumber.Create(Context, value).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromNumber(double value)
    {
        return MetaExpressionNumber.Create(Context, value).ScalarFromValue(this);
    }

    public Scalar<IMetaExpression> ScalarFromRational(long numerator, long denominator)
    {
        return MetaExpressionNumber.CreateRational(Context, numerator, denominator).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IMetaExpression> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var value = minValue + (maxValue - minValue) * randomGenerator.NextDouble();

        return ScalarFromNumber(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(IMetaExpression scalar)
    {
        return scalar is IMetaExpressionNumber number 
            ? number.NumberHeadSpecs.NumberFloat64Value 
            : double.NaN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(IMetaExpression scalar)
    {
        return scalar.ToString() ?? "<null>";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression Simplify(IMetaExpression scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IMetaExpression GetSymbol(string symbolNameText)
    {
        throw new NotImplementedException();
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