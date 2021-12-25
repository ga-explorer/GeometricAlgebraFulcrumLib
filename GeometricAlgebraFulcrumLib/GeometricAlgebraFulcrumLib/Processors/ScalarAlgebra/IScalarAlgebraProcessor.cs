namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public interface IScalarAlgebraProcessor<T> 
    {
        bool IsNumeric { get; }

        bool IsSymbolic { get; }

        T ScalarZero { get; }

        T ScalarOne { get; }

        T ScalarMinusOne { get; }

        T ScalarTwo { get; }

        T ScalarMinusTwo { get; }

        T ScalarTen { get; }

        T ScalarMinusTen { get; }

        T ScalarPi { get; }
        
        T ScalarPiOver2 { get; }

        T ScalarE { get; }

        T GetScalarFromNumber(int value);
        
        T GetScalarFromNumber(uint value);
        
        T GetScalarFromNumber(long value);
        
        T GetScalarFromNumber(ulong value);

        T GetScalarFromNumber(float value);

        T GetScalarFromNumber(double value);

        T GetScalarFromRational(long numerator, long denominator);

        T GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue);

        T GetScalarFromText(string text);

        T Add(T scalar1, T scalar2);

        T Subtract(T scalar1, T scalar2);

        T Times(T scalar1, T scalar2);

        T NegativeTimes(T scalar1, T scalar2);

        T Divide(T scalar1, T scalar2);

        T NegativeDivide(T scalar1, T scalar2);

        /// <summary>
        /// Get same value of given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        T Positive(T scalar);
        
        /// <summary>
        /// Get negative value of given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        T Negative(T scalar);

        T Inverse(T scalar);

        T Abs(T scalar);

        T Power(T baseScalar, T scalar);

        /// <summary>
        /// The square root of the given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        T Sqrt(T scalar);

        T SqrtOfAbs(T scalar);

        T Exp(T scalar);

        T LogE(T scalar);

        T Log2(T scalar);

        T Log10(T scalar);

        T Log(T baseScalar, T scalar);

        T Cos(T scalar);

        T Sin(T scalar);

        T Tan(T scalar);

        T ArcCos(T scalar);

        T ArcSin(T scalar);

        T ArcTan(T scalar);

        T ArcTan2(T scalarX, T scalarY);

        T Cosh(T scalar);

        T Sinh(T scalar);

        T Tanh(T scalar);

        bool IsValid(T scalar);

        bool IsZero(T scalar);

        bool IsZero(T scalar, bool nearZeroFlag);

        bool IsNearZero(T scalar);

        bool IsNotZero(T scalar);

        bool IsNotZero(T scalar, bool nearZeroFlag);

        bool IsNotNearZero(T scalar);

        bool IsPositive(T scalar);

        bool IsNegative(T scalar);

        bool IsNotPositive(T scalar);

        bool IsNotNegative(T scalar);

        bool IsNotNearPositive(T scalar);

        bool IsNotNearNegative(T scalar);

        string ToText(T scalar);
    }
}