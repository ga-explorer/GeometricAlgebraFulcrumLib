using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Basis;

public abstract class GbBernsteinBasisSetBase<T> :
    IPolynomialBasisSet<T>
{
    public IScalarProcessor<T> ScalarProcessor { get; }

    public int Degree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GbBernsteinBasisSetBase(IScalarProcessor<T> scalarProcessor, int degree)
    {
        if (degree is < 2 or > 64)
            throw new ArgumentOutOfRangeException(nameof(degree));

        Degree = degree;
        ScalarProcessor = scalarProcessor;
    }


    /// <summary>
    /// Get value at 'parameterValue' of degree 2 basis polynomial 0
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    public abstract Scalar<T> GetValueDegree20(T parameterValue);

    /// <summary>
    /// Get value at 'parameterValue' of degree 2 basis polynomial 2
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    public abstract Scalar<T> GetValueDegree22(T parameterValue);
        
    /// <summary>
    /// Get value at 'parameterValue' of degree 2 basis polynomial 1
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValueDegree21(T parameterValue)
    {
        return 1 - (GetValueDegree20(parameterValue) + GetValueDegree22(parameterValue));
    }

    /// <summary>
    /// Get value at 'parameterValue' of degree 2 basis polynomials
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<T> GetValueDegree2(T parameterValue)
    {
        var b02 = GetValueDegree20(parameterValue);
        var b22 = GetValueDegree22(parameterValue);
        var b12 = 1 - (b02 + b22);

        return new Triplet<T>(b02.ScalarValue, b12.ScalarValue, b22.ScalarValue);
    }


    private T Power(T value, int power)
    {
        return power == 0
            ? ScalarProcessor.OneValue
            : ScalarProcessor.Power(value, power).ScalarValue;
    }
        
    private T GetValue(int index, T parameterValue, Triplet<T> degree2Values)
    {
        if (index < 0 || index > Degree)
            return ScalarProcessor.ZeroValue;

        if (Degree == 2)
            return index switch
            {
                0 => degree2Values.Item1,
                2 => degree2Values.Item3,
                _ => degree2Values.Item2
            };

        if (index == Degree)
            return ScalarProcessor.Times(
                Power(parameterValue, Degree - 2),
                degree2Values.Item3
            ).ScalarValue;

        var oneMinusParameterValue = 
            ScalarProcessor.Subtract(ScalarProcessor.OneValue, parameterValue).ScalarValue;

        if (index == 0)
            return ScalarProcessor.Times(
                Power(oneMinusParameterValue, Degree - 2),
                degree2Values.Item1
            ).ScalarValue;

        var (b02, b12, b22) = degree2Values;
            
        if (index == Degree - 1)
            return ScalarProcessor.Add(
                ScalarProcessor.Times(
                    Power(parameterValue, Degree - 2),
                    b12
                ).ScalarValue,
                ScalarProcessor.Times(
                    Degree - 2,
                    Power(parameterValue, Degree - 3),
                    oneMinusParameterValue,
                    b22
                ).ScalarValue
            ).ScalarValue;

        if (index == 1)
            return ScalarProcessor.Add(
                ScalarProcessor.Times(
                    Degree - 2,
                    parameterValue,
                    Power(oneMinusParameterValue, Degree - 3),
                    b02
                ).ScalarValue,
                ScalarProcessor.Times(
                    Power(oneMinusParameterValue, Degree - 2),
                    b12
                ).ScalarValue
            ).ScalarValue;

        return ScalarProcessor.Add(
            ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree - 2, index).ScalarValue,
                Power(parameterValue, index),
                Power(oneMinusParameterValue, Degree - 2 - index),
                b02
            ),
            ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree - 2, index - 1).ScalarValue,
                Power(parameterValue, index - 1),
                Power(oneMinusParameterValue, Degree - 2 - (index - 1)),
                b12
            ),
            ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree - 2, index - 2).ScalarValue,
                Power(parameterValue, index - 2),
                Power(oneMinusParameterValue, Degree - 2 - (index - 2)),
                b22
            )
        ).ScalarValue;
    }

    public Scalar<T> GetValue(int index, T parameterValue)
    {
        if (index < 0 || index > Degree)
            return ScalarProcessor.Zero;

        if (Degree == 2)
            return index switch
            {
                0 => GetValueDegree20(parameterValue),
                2 => GetValueDegree22(parameterValue),
                _ => GetValueDegree21(parameterValue)
            };

        if (index == Degree)
            return Power(parameterValue, Degree - 2) * GetValueDegree22(parameterValue);

        var oneMinusParameterValue = 
            ScalarProcessor.Subtract(ScalarProcessor.OneValue, parameterValue).ScalarValue;

        if (index == 0)
            return Power(oneMinusParameterValue, Degree - 2) * GetValueDegree20(parameterValue);

        var (b02, b12, b22) = GetValueDegree2(parameterValue);
            
        if (index == Degree - 1)
            return ScalarProcessor.Add(
                ScalarProcessor.Times(
                    Power(parameterValue, Degree - 2),
                    b12
                ),
                ScalarProcessor.Times(
                    Degree - 2,
                    Power(parameterValue, Degree - 3),
                    oneMinusParameterValue,
                    b22
                )
            );

        if (index == 1)
            return ScalarProcessor.Add(
                ScalarProcessor.Times(
                    Degree - 2,
                    parameterValue,
                    Power(oneMinusParameterValue, Degree - 3),
                    b02
                ),
                ScalarProcessor.Times(
                    Power(oneMinusParameterValue, Degree - 2),
                    b12
                )
            );

        return ScalarProcessor.Add(
            ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree - 2, index).ScalarValue,
                Power(parameterValue, index),
                Power(oneMinusParameterValue, Degree - 2 - index),
                b02
            ).ScalarValue,
            ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree - 2, index - 1).ScalarValue,
                Power(parameterValue, index - 1),
                Power(oneMinusParameterValue, Degree - 2 - (index - 1)),
                b12
            ).ScalarValue,
            ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree - 2, index - 2).ScalarValue,
                Power(parameterValue, index - 2),
                Power(oneMinusParameterValue, Degree - 2 - (index - 2)),
                b22
            ).ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValue(int index, T parameterValue, T termScalar)
    {
        return termScalar * GetValue(index, parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValue(T parameterValue, params T[] termScalarsList)
    {
        var degree2Values = 
            GetValueDegree2(parameterValue);

        return ScalarProcessor.Add(
            termScalarsList.Select((termScalar, index) => 
                ScalarProcessor.Times(
                    termScalar,
                    GetValue(index, parameterValue, degree2Values)
                )
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<T> GetValues(T parameterValue)
    {
        var degree2Values = 
            GetValueDegree2(parameterValue);

        return Enumerable.Range(0, Degree + 1).Select(
            index => GetValue(index, parameterValue, degree2Values)
        ).ToArray();
    }
}