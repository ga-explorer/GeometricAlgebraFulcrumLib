using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;

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
            return scalarProcessor.ZeroValue;

        var scalar = vector[index];

        return scalar is not null
            ? scalar
            : scalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetVectorTermScalar<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> vector, int index)
    {
        return vector.TryGetValue(index, out var scalar) && scalar is not null
            ? scalar
            : scalarProcessor.ZeroValue;
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
    public static LinRandomComposer<T> CreateLinRandomComposer<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, Random randomGenerator)
    {
        return new LinRandomComposer<T>(scalarProcessor, vSpaceDimensions, randomGenerator);
    }


}