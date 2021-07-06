using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars
{
    public interface IGaScalarProcessor<TScalar> 
    {
        TScalar ZeroScalar { get; }
        
        TScalar OneScalar { get; }
        
        TScalar MinusOneScalar { get; }
        
        TScalar PiScalar { get; }

        TScalar Add(TScalar scalar1, TScalar scalar2);

        TScalar Add(params TScalar[] scalarsList);

        TScalar Add(IEnumerable<TScalar> scalarsList);
        
        TScalar Subtract(TScalar scalar1, TScalar scalar2);

        TScalar Times(TScalar scalar1, TScalar scalar2);
        
        TScalar Times(params TScalar[] scalarsList);

        TScalar Times(IEnumerable<TScalar> scalarsList);

        TScalar NegativeTimes(TScalar scalar1, TScalar scalar2);
        
        TScalar NegativeTimes(params TScalar[] scalarsList);

        TScalar NegativeTimes(IEnumerable<TScalar> scalarsList);

        TScalar Divide(TScalar scalar1, TScalar scalar2);

        TScalar NegativeDivide(TScalar scalar1, TScalar scalar2);

        /// <summary>
        /// Get same value of given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        TScalar Positive(TScalar scalar);
        
        /// <summary>
        /// Get negative value of given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        TScalar Negative(TScalar scalar);

        TScalar Inverse(TScalar scalar);

        TScalar Abs(TScalar scalar);

        /// <summary>
        /// The square root of the given scalar
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        TScalar Sqrt(TScalar scalar);

        TScalar SqrtOfAbs(TScalar scalar);

        TScalar Exp(TScalar scalar);

        TScalar Log(TScalar scalar);

        TScalar Log2(TScalar scalar);

        TScalar Log10(TScalar scalar);

        TScalar Log(TScalar scalar, TScalar baseScalar);

        TScalar Cos(TScalar scalar);

        TScalar Sin(TScalar scalar);

        TScalar Tan(TScalar scalar);

        TScalar ArcCos(TScalar scalar);

        TScalar ArcSin(TScalar scalar);

        TScalar ArcTan(TScalar scalar);

        TScalar ArcTan2(TScalar scalarX, TScalar scalarY);

        bool IsValid(TScalar scalar);

        bool IsZero(TScalar scalar);

        bool IsZero(TScalar scalar, bool nearZeroFlag);

        bool IsNearZero(TScalar scalar);

        TScalar TextToScalar(string text);

        TScalar IntegerToScalar(int value);
        
        TScalar Float64ToScalar(double value);

        TScalar GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue);

        string ToText(TScalar scalar);
    }
}