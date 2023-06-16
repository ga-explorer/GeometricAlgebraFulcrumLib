using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

public static class LinUnilinearMapComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMapComposer<T> CreateLinUnilinearMapComposer<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinUnilinearMapComposer<T>(scalarProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ToDiagonalLinUnilinearMap<T>(this IEnumerable<T> diagonalVector, IScalarProcessor<T> scalarProcessor)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var index = 0;
        foreach (var scalar in diagonalVector)
        {
            if (!scalarProcessor.IsZero(scalar))
                indexVectorDictionary.Add(
                    index, 
                    scalarProcessor.CreateLinVector(index, scalar)
                );

            index++;
        }

        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ToDiagonalLinUnilinearMap<T>(this IReadOnlyDictionary<int, T> diagonalVector, IScalarProcessor<T> scalarProcessor)
    {
        var indexVectorDictionary =
            diagonalVector
                .Where(p => !scalarProcessor.IsZero(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => scalarProcessor.CreateLinVector(p.Key, p.Value)
                );

        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ToDiagonalLinUnilinearMap<T>(this LinVector<T> diagonalVector)
    {
        var scalarProcessor = diagonalVector.ScalarProcessor;

        var indexVectorDictionary =
            diagonalVector
                .ToDictionary(
                    p => p.Key,
                    p => scalarProcessor.CreateLinVector(p.Key, p.Value)
                );

        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ToLinUnilinearMap<T>(this T[,] array, IScalarProcessor<T> scalarProcessor)
    {
        return array
            .ColumnsToLinVectors(scalarProcessor)
            .CreateLinUnilinearMap(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ToLinUnilinearMap<T>(this IReadOnlyDictionary<int, LinVector<T>> indexVectorDictionary, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateDiagonalLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> diagonalVector)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var index = 0;
        foreach (var scalar in diagonalVector)
        {
            if (!scalarProcessor.IsZero(scalar))
                indexVectorDictionary.Add(
                    index, 
                    scalarProcessor.CreateLinVector(index, scalar)
                );

            index++;
        }

        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateDiagonalLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> diagonalVector)
    {
        var indexVectorDictionary =
            diagonalVector
                .Where(p => !scalarProcessor.IsZero(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => scalarProcessor.CreateLinVector(p.Key, p.Value)
                );

        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateIdentityLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
    {
        return new LinUnilinearMap<T>(
            scalarProcessor,
            vSpaceDimensions
                .GetRange()
                .ToDictionary(
                    i => i, 
                    scalarProcessor.CreateLinVector
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, T[,] array)
    {
        return array
            .ColumnsToLinVectors(scalarProcessor)
            .CreateLinUnilinearMap(scalarProcessor);
    }

    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, params LinVector<T>[] vectorList)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var i = 0;
        foreach (var vector in vectorList)
        {
            if (!vector.IsZero) 
                indexVectorDictionary.Add(i, vector);

            i++;
        }

        if (indexVectorDictionary.Count == 0)
            return new LinUnilinearMap<T>(
                scalarProcessor, 
                new EmptyDictionary<int, LinVector<T>>()
            );

        if (indexVectorDictionary.Count == 1)
            return new LinUnilinearMap<T>(
                scalarProcessor, 
                new SingleItemDictionary<int, LinVector<T>>(indexVectorDictionary.First())
            );

        return new LinUnilinearMap<T>(
            scalarProcessor, 
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<LinVector<T>> vectorList)
    {
        return CreateLinUnilinearMap(vectorList, scalarProcessor);
    }
    
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IEnumerable<LinVector<T>> vectorList, IScalarProcessor<T> scalarProcessor)
    {
        var indexVectorDictionary = new Dictionary<int, LinVector<T>>();

        var i = 0;
        foreach (var vector in vectorList)
        {
            if (!vector.IsZero) 
                indexVectorDictionary.Add(i, vector);

            i++;
        }

        if (indexVectorDictionary.Count == 0)
            return new LinUnilinearMap<T>(
                scalarProcessor, 
                new EmptyDictionary<int, LinVector<T>>()
            );

        if (indexVectorDictionary.Count == 1)
            return new LinUnilinearMap<T>(
                scalarProcessor, 
                new SingleItemDictionary<int, LinVector<T>>(indexVectorDictionary.First())
            );

        return new LinUnilinearMap<T>(
            scalarProcessor, 
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, LinVector<T>> indexVectorDictionary)
    {
        if (indexVectorDictionary.Count == 0 && indexVectorDictionary is not EmptyDictionary<int, LinVector<T>>)
            return new LinUnilinearMap<T>(
                scalarProcessor,
                new EmptyDictionary<int, LinVector<T>>()
            );

        if (indexVectorDictionary.Count == 1 && indexVectorDictionary is not SingleItemDictionary<int, LinVector<T>>)
            return new LinUnilinearMap<T>(
                scalarProcessor,
                new SingleItemDictionary<int, LinVector<T>>(
                    indexVectorDictionary.First()
                )
            );

        return new LinUnilinearMap<T>(
            scalarProcessor, 
            indexVectorDictionary
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IReadOnlyDictionary<int, LinVector<T>> indexVectorDictionary, IScalarProcessor<T> scalarProcessor)
    {
        return new LinUnilinearMap<T>(
            scalarProcessor, 
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, Func<int, LinVector<T>> indexVectorMapping)
    {
        var indexVectorDictionary = vSpaceDimensions
            .GetRange()
            .ToDictionary(i => i, indexVectorMapping);

        return new LinUnilinearMap<T>(
            scalarProcessor, 
            indexVectorDictionary
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateLinUnilinearMap<T>(this IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, Func<LinVector<T>, LinVector<T>> indexVectorMapping)
    {
        var indexVectorDictionary = vSpaceDimensions
            .GetRange()
            .ToDictionary(
                i => i, 
                i => indexVectorMapping(
                    scalarProcessor.CreateLinVector(i)
                )
            );

        return new LinUnilinearMap<T>(
            scalarProcessor, 
            indexVectorDictionary
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<IndexPair, T> Transpose<T>(this KeyValuePair<IndexPair, T> pair)
    {
        return new KeyValuePair<IndexPair, T>(
            pair.Key.Transpose(),
            pair.Value
        );
    }
}