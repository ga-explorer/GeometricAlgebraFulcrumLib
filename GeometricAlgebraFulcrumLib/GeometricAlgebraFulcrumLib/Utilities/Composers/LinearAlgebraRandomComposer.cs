using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class LinearAlgebraRandomComposer<T> :
        ScalarAlgebraRandomComposer<T>
    {
        public ILinearAlgebraProcessor<T> LinearProcessor { get; }


        internal LinearAlgebraRandomComposer([NotNull] ILinearAlgebraProcessor<T> linearProcessor)
            : base(linearProcessor)
        {
            LinearProcessor = linearProcessor;
        }

        internal LinearAlgebraRandomComposer([NotNull] ILinearAlgebraProcessor<T> linearProcessor, int seed)
            : base(linearProcessor, seed)
        {
            LinearProcessor = linearProcessor;
        }

        internal LinearAlgebraRandomComposer([NotNull] ILinearAlgebraProcessor<T> linearProcessor, Random randomGenerator)
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
            return GetScalar().CreateLinVectorSingleScalarStorage(index);
        }

        public LinVectorSparseStorage<T> GetLinVectorSparseStorage(int denseCount)
        {
            var vector = new LinVectorSparseStorage<T>();

            for (var index = 1UL; index < (ulong) denseCount; index++)
                if (RandomGenerator.GetBoolean())
                    vector.AddValue(index, GetScalar());

            return vector;
        }

        public LinVectorTreeStorage<T> GetLinVectorTreeStorage(int denseCount)
        {
            var vectorData = new Dictionary<ulong, T>();

            for (var index = 1UL; index < (ulong) denseCount; index++)
                if (RandomGenerator.GetBoolean())
                    vectorData.Add(index, GetScalar());

            return vectorData.CreateLinVectorTreeStorage();
        }
        
        public LinVectorArrayStorage<T> GetLinVectorArrayStorage(int denseCount)
        {
            var vector = new T[denseCount];

            for (var index = 1UL; index < (ulong) denseCount; index++)
                vector[index] = GetScalar();

            return vector.CreateLinVectorArrayStorage();
        }
        
        public LinVectorListStorage<T> GetLinVectorListStorage(int denseCount)
        {
            var vector = new LinVectorListStorage<T>();

            for (var index = 1UL; index < (ulong) denseCount; index++)
                vector.Append(GetScalar());

            return vector;
        }
        
        public LinVectorComputedDenseStorage<T> GetLinVectorComputedDenseStorage(int denseCount)
        {
            var vectorData = new Dictionary<ulong, T>();

            for (var index = 1UL; index < (ulong) denseCount; index++)
                if (RandomGenerator.GetBoolean())
                    vectorData.Add(index, GetScalar());

            return new LinVectorComputedDenseStorage<T>(
                denseCount, 
                index => 
                    vectorData.TryGetValue(index, out var scalar) 
                        ? scalar 
                        : LinearProcessor.ScalarZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorRepeatedScalarStorage<T> GetLinVectorRepeatedScalarStorage(int denseCount)
        {
            return new LinVectorRepeatedScalarStorage<T>(denseCount, GetScalar());
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
                        new IndexPairRecord(
                            index / (ulong) denseCount, 
                            index % (ulong) denseCount
                        )
                    ,
                    (_, _) => LinearProcessor.ScalarZero
                )
            );

            return vectorsList;
        }


        public ILinMatrixDenseStorage<T> GetLinMatrixDenseStorage(int denseCount1, int denseCount2)
        {
            var array = new T[denseCount1, denseCount2];

            for (var index1 = 0UL; index1 < (ulong) denseCount1; index1++)
            for (var index2 = 0UL; index2 < (ulong) denseCount2; index2++)
                array[index1, index2] = GetScalar();

            return array.CreateLinMatrixDenseStorage();
        }

    }
}