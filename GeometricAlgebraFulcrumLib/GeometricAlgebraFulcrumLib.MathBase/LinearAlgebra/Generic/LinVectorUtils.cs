using System.Runtime.CompilerServices;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

public static class LinVectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidLinVectorDictionary<T>(this IReadOnlyDictionary<int, T> indexScalarDictionary, IScalarProcessor<T> scalarProcessor)
    {
        return indexScalarDictionary.Count switch
        {
            0 => indexScalarDictionary is EmptyDictionary<int, T>,

            1 => indexScalarDictionary is SingleItemDictionary<int, T> dict &&
                 dict.Key >= 0 &&
                 scalarProcessor.IsValid(dict.Value) &&
                 !scalarProcessor.IsZero(dict.Value),

            _ => indexScalarDictionary.All(p =>
                p.Key >= 0 &&
                scalarProcessor.IsValid(p.Value) &&
                !scalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetVectorTermScalar<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector, int index)
    {
        if (index < 0 || index >= vector.Count)
            return scalarProcessor.ScalarZero;

        var scalar = vector[index];

        return scalar is not null
            ? scalar
            : scalarProcessor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetVectorTermScalar<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> vector, int index)
    {
        return vector.TryGetValue(index, out var scalar) && scalar is not null
            ? scalar
            : scalarProcessor.ScalarZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinRandomComposer<T> CreateLinRandomComposer<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        return new LinRandomComposer<T>(scalarProcessor, vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinRandomComposer<T> CreateLinRandomComposer<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, int seed)
    {
        return new LinRandomComposer<T>(scalarProcessor, vSpaceDimensions, seed);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinRandomComposer<T> CreateLinRandomComposer<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, System.Random randomGenerator)
    {
        return new LinRandomComposer<T>(scalarProcessor, vSpaceDimensions, randomGenerator);
    }


    public static T[] ToArray<T>(this LinVector<T> vector, int vSpaceDimensions)
    {
        if (vSpaceDimensions < vector.VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));
            
        var array = vector.ScalarProcessor.CreateArrayZero1D(vSpaceDimensions);

        foreach (var (index, scalar) in vector.IndexScalarPairs)
            array[index] = scalar;

        return array;
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> ENorm<T>(this LinVector<T> mv)
    {
        return mv.ENormSquared().Sqrt();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> EInverse<T>(this LinVector<T> mv1)
    {
        return mv1.Divide(
            mv1.ESp(mv1).ScalarValue
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapScalars<T>(this LinVector<T> mv, Func<T, T> scalarMapping)
    {
        var termList =
            mv.IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return mv
            .ScalarProcessor
            .CreateLinVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapScalars<T>(this LinVector<T> mv, Func<int, T, T> scalarMapping)
    {
        var termList =
            mv.IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return mv
            .ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapBasisVectors<T>(this LinVector<T> mv, Func<int, int> basisMapping, bool simplify = true)
    {
        var termList =
            mv.IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    basisMapping(term.Key),
                    term.Value
                )
            );

        return mv
            .ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapBasisVectors<T>(this LinVector<T> mv, Func<int, T, int> basisMapping, bool simplify = true)
    {
        var termList =
            mv.IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    basisMapping(term.Key, term.Value),
                    term.Value
                )
            );

        return mv
            .ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapTerms<T>(this LinVector<T> mv, Func<int, T, KeyValuePair<int, T>> termMapping, bool simplify = true)
    {
        var termList =
            mv.IndexScalarPairs.Select(
                term => termMapping(term.Key, term.Value)
            );

        return mv
            .ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetEuclideanAngle<T>(this LinVector<T> v1, LinVector<T> v2, bool assumeUnitVectors = false)
    {
        var v12Sp = v1.ESp(v2);

        var angle = assumeUnitVectors
            ? v12Sp
            : v12Sp / (v1.ENormSquared() * v2.ENormSquared()).Sqrt();

        return angle.ArcCos();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> GetBisector<T>(this LinVector<T> v1, LinVector<T> v2, bool assumeEqualNormVectors = false)
    {
        var scalarProcessor = v1.ScalarProcessor;
            
        return (v1 + v2).Divide(scalarProcessor.ScalarTwo);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> GetUnitBisector<T>(this LinVector<T> v1, LinVector<T> v2, bool assumeEqualNormVectors = false)
    {
        var bisector = assumeEqualNormVectors
            ? v1 + v2
            : v1.DivideByENorm() + v2.DivideByENorm();
            
        return bisector.DivideByENorm();
    }

}