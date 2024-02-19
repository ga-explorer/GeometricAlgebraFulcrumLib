using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers;

public class LinearAlgebraRandomComposer<T> :
    ScalarRandomComposer<T>
{
    public ILinearProcessor<T> LinearProcessor { get; }


    internal LinearAlgebraRandomComposer(ILinearProcessor<T> linearProcessor)
        : base(linearProcessor)
    {
        LinearProcessor = linearProcessor;
    }

    internal LinearAlgebraRandomComposer(ILinearProcessor<T> linearProcessor, int seed)
        : base(linearProcessor, seed)
    {
        LinearProcessor = linearProcessor;
    }

    internal LinearAlgebraRandomComposer(ILinearProcessor<T> linearProcessor, Random randomGenerator)
        : base(linearProcessor, randomGenerator)
    {
        LinearProcessor = linearProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetIndex(ulong maxIndex)
    {
        return (ulong) Math.Floor(RandomGenerator.NextDouble() * (maxIndex + 1));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorSingleScalarStorage<T> GetLinVectorSingleScalarStorage(ulong index)
    {
        return GetScalarValue().CreateLinVectorSingleScalarStorage(index);
    }

    public LinVectorSparseStorage<T> GetLinVectorSparseStorage(int denseCount)
    {
        var vector = new LinVectorSparseStorage<T>();

        for (var index = 1UL; index < (ulong) denseCount; index++)
            if (RandomGenerator.GetBoolean())
                vector.AddValue(index, GetScalarValue());

        return vector;
    }

    public LinVectorTreeStorage<T> GetLinVectorTreeStorage(int denseCount)
    {
        var vectorData = new Dictionary<ulong, T>();

        for (var index = 1UL; index < (ulong) denseCount; index++)
            if (RandomGenerator.GetBoolean())
                vectorData.Add(index, GetScalarValue());

        return vectorData.CreateLinVectorTreeStorage();
    }
        
    public LinVectorArrayStorage<T> GetLinVectorArrayStorage(int denseCount)
    {
        var vector = new T[denseCount];

        for (var index = 1UL; index < (ulong) denseCount; index++)
            vector[index] = GetScalarValue();

        return vector.CreateLinVectorArrayStorage();
    }
        
    public LinVectorListStorage<T> GetLinVectorListStorage(int denseCount)
    {
        var vector = new LinVectorListStorage<T>();

        for (var index = 1UL; index < (ulong) denseCount; index++)
            vector.Append(GetScalarValue());

        return vector;
    }
        
    public LinVectorComputedDenseStorage<T> GetLinVectorComputedDenseStorage(int denseCount)
    {
        var vectorData = new Dictionary<ulong, T>();

        for (var index = 1UL; index < (ulong) denseCount; index++)
            if (RandomGenerator.GetBoolean())
                vectorData.Add(index, GetScalarValue());

        return new LinVectorComputedDenseStorage<T>(
            denseCount, 
            index => 
                vectorData.TryGetValue(index, out var scalar) 
                    ? scalar 
                    : ScalarProcessor.ScalarZero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorRepeatedScalarStorage<T> GetLinVectorRepeatedScalarStorage(int denseCount)
    {
        return new LinVectorRepeatedScalarStorage<T>(denseCount, GetScalarValue());
    }

    public IReadOnlyList<ILinVectorStorage<T>> GetLinVectorStorages(int denseCount)
    {
        var vectorsList = new List<ILinVectorStorage<T>>
        {
            LinVectorEmptyStorage<T>.EmptyStorage,
            GetLinVectorSparseStorage(denseCount),
            GetLinVectorTreeStorage(denseCount),
            GetLinVectorArrayStorage(denseCount),
            GetLinVectorListStorage(denseCount),
            GetLinVectorComputedDenseStorage(denseCount),
            GetLinVectorRepeatedScalarStorage(denseCount)
        };

        vectorsList.AddRange(
            ((ulong) denseCount)
            .GetRange()
            .Select(GetLinVectorSingleScalarStorage)
        );

        var matrix = GetLinMatrixDenseStorage(denseCount, denseCount);
            
        vectorsList.AddRange(
            matrix.GetRows().Select(r => r.Storage)
        );

        vectorsList.AddRange(
            matrix.GetColumns().Select(r => r.Storage)
        );

        vectorsList.Add(
            new LinVectorMatrixSliceDenseStorage<T>(
                matrix, 
                denseCount * denseCount, 
                index => 
                    new RGaKvIndexPairRecord(
                        index / (ulong) denseCount, 
                        index % (ulong) denseCount
                    )
                ,
                (_, _) => ScalarProcessor.ScalarZero
            )
        );

        return vectorsList;
    }


    public ILinMatrixDenseStorage<T> GetLinMatrixDenseStorage(int denseCount1, int denseCount2)
    {
        var array = new T[denseCount1, denseCount2];

        for (var index1 = 0UL; index1 < (ulong) denseCount1; index1++)
        for (var index2 = 0UL; index2 < (ulong) denseCount2; index2++)
            array[index1, index2] = GetScalarValue();

        return array.CreateLinMatrixDenseStorage();
    }

}