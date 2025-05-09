namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public interface IScalarProcessor<T>
{
    double ZeroEpsilon { get; set; }

    bool IsNumeric { get; }

    bool IsSymbolic { get; }


    Scalar<T> Zero { get; }
    
    Scalar<T> PositiveInfinity { get; }

    Scalar<T> NegativeInfinity { get; }

    Scalar<T> One { get; }

    Scalar<T> MinusOne { get; }

    Scalar<T> Two { get; }

    Scalar<T> MinusTwo { get; }

    Scalar<T> Ten { get; }

    Scalar<T> MinusTen { get; }

    Scalar<T> Pi { get; }

    Scalar<T> PiTimes2 { get; }
    
    Scalar<T> PiTimes4 { get; }

    Scalar<T> PiOver2 { get; }

    Scalar<T> E { get; }

    Scalar<T> DegreeToRadianFactor { get; }

    Scalar<T> RadianToDegreeFactor { get; }
    

    T ZeroValue { get; }
    
    T PositiveInfinityValue { get; }

    T NegativeInfinityValue { get; }

    T OneValue { get; }

    T MinusOneValue { get; }

    T TwoValue { get; }

    T MinusTwoValue { get; }

    T TenValue { get; }

    T MinusTenValue { get; }

    T PiValue { get; }

    T PiTimes2Value { get; }
    
    T PiTimes4Value { get; }

    T PiOver2Value { get; }

    T EValue { get; }

    T DegreeToRadianFactorValue { get; }

    T RadianToDegreeFactorValue { get; }


    bool IsValid(T scalar);

    double ToFloat64(T scalar);

    string ToText(T scalar);
    

    Scalar<T> ScalarFromNumber(int value);

    Scalar<T> ScalarFromNumber(uint value);

    Scalar<T> ScalarFromNumber(long value);

    Scalar<T> ScalarFromNumber(ulong value);

    Scalar<T> ScalarFromNumber(float value);

    Scalar<T> ScalarFromNumber(double value);

    Scalar<T> ScalarFromRational(long numerator, long denominator);

    Scalar<T> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue);

    Scalar<T> ScalarFromText(string text);


    Scalar<T> Add(T scalar1, T scalar2);

    Scalar<T> Subtract(T scalar1, T scalar2);

    Scalar<T> Times(T scalar1, T scalar2);

    Scalar<T> Divide(T scalar1, T scalar2);

    //Scalar<T> Remainder(T scalar1, T scalar2);

    Scalar<T> Power(T baseScalar, T scalar);

    Scalar<T> Log(T baseScalar, T scalar);

    Scalar<T> VectorToRadians(T scalarX, T scalarY);


    Scalar<T> Positive(T scalar);

    Scalar<T> Negative(T scalar);

    Scalar<T> Inverse(T scalar);

    Scalar<T> Sign(T scalar);

    Scalar<T> UnitStep(T scalar);
    
    //Scalar<T> IntegerPart(T scalar);

    //Scalar<T> FractionalPart(T scalar);

    Scalar<T> Abs(T scalar);
    
    Scalar<T> Sqrt(T scalar);

    Scalar<T> SqrtOfAbs(T scalar);

    Scalar<T> Exp(T scalar);

    Scalar<T> LogE(T scalar);

    Scalar<T> Log2(T scalar);

    Scalar<T> Log10(T scalar);
    
    Scalar<T> Cos(T scalar);

    Scalar<T> Sin(T scalar);

    Scalar<T> Tan(T scalar);

    Scalar<T> Cosh(T scalar);

    Scalar<T> Sinh(T scalar);

    Scalar<T> Tanh(T scalar);
    
    //bool IsFiniteNumber(T scalar);

    //bool IsZero(T scalar);

    //bool IsZero(T scalar, bool nearZeroFlag);

    //bool IsNearZero(T scalar);

    //bool IsNotZero(T scalar);

    //bool IsNotZero(T scalar, bool nearZeroFlag);

    //bool IsNotNearZero(T scalar);

    //bool IsPositive(T scalar);

    //bool IsNegative(T scalar);

    //bool IsNotPositive(T scalar);

    //bool IsNotNegative(T scalar);

    //bool IsNotNearPositive(T scalar);

    //bool IsNotNearNegative(T scalar);
    
}