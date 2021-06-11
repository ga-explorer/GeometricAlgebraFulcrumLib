using System.Collections.Generic;

namespace GeometricAlgebraLib.Processors.Scalars
{
    public interface IGaScalarProcessor<T> 
    {
        T ZeroScalar { get; }
        
        T OneScalar { get; }
        
        T MinusOneScalar { get; }
        
        T PiScalar { get; }

        T Add(T scalar1, T scalar2);

        T Add(params T[] scalarsList);

        T Add(IEnumerable<T> scalarsList);
        
        T Subtract(T scalar1, T scalar2);

        T Times(T scalar1, T scalar2);
        
        T Times(params T[] scalarsList);

        T Times(IEnumerable<T> scalarsList);

        T NegativeTimes(T scalar1, T scalar2);
        
        T NegativeTimes(params T[] scalarsList);

        T NegativeTimes(IEnumerable<T> scalarsList);

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

        /// <summary>
        /// The square root of the given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        T Sqrt(T scalar);

        T SqrtOfAbs(T scalar);

        T Exp(T scalar);

        T Log(T scalar);

        T Log2(T scalar);

        T Log10(T scalar);

        T Log(T scalar, T baseScalar);

        T Cos(T scalar);

        T Sin(T scalar);

        T Tan(T scalar);

        T ArcCos(T scalar);

        T ArcSin(T scalar);

        T ArcTan(T scalar);

        T ArcTan2(T scalarX, T scalarY);

        bool IsValid(T scalar);

        bool IsZero(T scalar);

        bool IsZero(T scalar, bool nearZeroFlag);

        bool IsNearZero(T scalar);
        
        T IntegerToScalar(int value);
    }
}