﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.Basis;

/// <summary>
/// This class defines a set of Bernstein basis polynomials of given degree
/// https://en.wikipedia.org/wiki/Bernstein_polynomial
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class BernsteinBasisSet<T> :
    IPolynomialBasisSet<T>
{
    private static readonly Dictionary<int, BernsteinBasisSet<T>> BasisSetCache
        = new Dictionary<int, BernsteinBasisSet<T>>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BernsteinBasisSet<T> Create(IScalarProcessor<T> scalarProcessor, int degree)
    {
        if (BasisSetCache.TryGetValue(degree, out var basisSet))
        {
            if (ReferenceEquals(basisSet.ScalarProcessor, scalarProcessor))
                return basisSet;

            basisSet = new BernsteinBasisSet<T>(scalarProcessor, degree);

            BasisSetCache[degree] = basisSet;

            return basisSet;
        }

        basisSet = new BernsteinBasisSet<T>(scalarProcessor, degree);

        BasisSetCache.Add(degree, basisSet);

        return basisSet;
    }


    public IScalarProcessor<T> ScalarProcessor { get; }

    public int Degree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BernsteinBasisSet(IScalarProcessor<T> scalarProcessor, int degree)
    {
        if (degree is < 0 or > 64)
            throw new ArgumentOutOfRangeException(nameof(degree));

        Degree = degree;
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T Power(T value, int power)
    {
        return power == 0
            ? ScalarProcessor.OneValue
            : ScalarProcessor.Power(value, power).ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetValue(int index, T parameterValue)
    {
        if (index < 0 || index > Degree)
            return ScalarProcessor.Zero;

        var parameterValueMinusOne = 
            ScalarProcessor.Subtract(ScalarProcessor.OneValue, parameterValue).ScalarValue;

        return ScalarProcessor.Times(
            ScalarProcessor.BinomialCoefficient(Degree, index).ScalarValue,
            Power(parameterValue, index),
            Power(parameterValueMinusOne, Degree - index)
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
        return ScalarProcessor.Add(
            termScalarsList.Select(
                (termScalar, index) => GetValue(index, parameterValue, termScalar).ScalarValue
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<T> GetValues(T parameterValue)
    {
        return Enumerable.Range(0, Degree + 1).Select(
            index => GetValue(index, parameterValue).ScalarValue
        ).ToArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BernsteinBasisPairProductSet<T> CreatePairProductSet()
    {
        return BernsteinBasisPairProductSet<T>.Create(this);
    }
}