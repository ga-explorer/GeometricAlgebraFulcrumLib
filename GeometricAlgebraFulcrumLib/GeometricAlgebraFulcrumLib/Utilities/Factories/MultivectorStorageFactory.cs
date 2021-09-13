using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class MultivectorStorageFactory
    {
        public static IMultivectorStorage<T> CreateMultivectorStorage<T>(this ILinVectorStorage<T> idScalarList)
        {
            idScalarList = idScalarList.GetCompactList();

            if (idScalarList.IsEmpty())
                return KVectorStorage<T>.ZeroScalar;

            if (idScalarList.GetSparseCount() > 1)
                return MultivectorStorage<T>.Create(idScalarList);

            var (id, scalar) = idScalarList.GetMinIndexScalarRecord();

            if (id == 0)
                return KVectorStorage<T>.CreateKVectorScalar(scalar);

            return MultivectorStorage<T>.Create(idScalarList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CopyToMultivectorSparseStorage<T>(this IReadOnlyDictionary<ulong, T> idScalarDictionary)
        {
            var evenDictionary = idScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return MultivectorStorage<T>.Create(evenDictionary);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorZeroSparseStorage<T>()
        {
            return MultivectorStorage<T>.ZeroMultivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorZeroSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return MultivectorStorage<T>.ZeroMultivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorTermSparseStorage<T>(int index, T scalar)
        {
            return MultivectorStorage<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorTermSparseStorage<T>(ulong index, T scalar)
        {
            return MultivectorStorage<T>.Create(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorTermSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index, T scalar)
        {
            return MultivectorStorage<T>.Create((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorTermSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return MultivectorStorage<T>.Create(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorTermSparseStorage<T>(this IndexScalarRecord<T> idScalarPair)
        {
            return MultivectorStorage<T>.Create(idScalarPair.Index, idScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorTermSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IndexScalarRecord<T> idScalarPair)
        {
            return MultivectorStorage<T>.Create(idScalarPair.Index, idScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorBasisSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index)
        {
            return MultivectorStorage<T>.Create((ulong) index, scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorBasisSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index)
        {
            return MultivectorStorage<T>.Create(index, scalarProcessor.ScalarOne);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this Dictionary<ulong, T> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(params T[] scalarArray)
        {
            return MultivectorStorage<T>.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IReadOnlyList<T> scalarList)
        {
            return MultivectorStorage<T>.Create(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IEnumerable<T> scalarList)
        {
            return MultivectorStorage<T>.Create(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return MultivectorStorage<T>.Create(
                termsList.CreateDictionary()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return MultivectorStorage<T>.Create(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return MultivectorStorage<T>.Create(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> storage)
        {
            return MultivectorStorage<T>.Create(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this ILinVectorStorage<T> storage)
        {
            return MultivectorStorage<T>.Create(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> storage)
        {
            return MultivectorStorage<T>.Create(
                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this ILinVectorGradedStorage<T> storage)
        {
            return MultivectorStorage<T>.Create(
                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, int> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, uint> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params uint[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, long> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params long[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, ulong> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, float> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params float[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, double> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params double[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, string> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromText(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params string[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromText(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromText(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, object> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromObjects(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params object[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromObjects(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<object> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Dictionary<ulong, T> idScalarDictionary)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorage(idScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorage(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            return MultivectorStorage<T>.Create(
                scalarProcessor.CreateLinVectorStorage(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return MultivectorStorage<T>.Create(
                LinVectorEmptyStorage<T>.EmptyStorage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageRepeatedScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count, T value)
        {
            return MultivectorStorage<T>.Create(
                value.CreateLinVectorDenseStorage(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T value)
        {
            return MultivectorStorage<T>.Create(
                new LinVectorSingleScalarDenseStorage<T>(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index, T value)
        {
            return MultivectorStorage<T>.Create(
                value.CreateLinVectorSingleScalarStorage(index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            return MultivectorStorage<T>.Create(
                idScalarRecords.CreateLinVectorStorage()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> CreateMultivectorSparseStorage<T>(this IEnumerable<GradeIndexScalarRecord<T>> termsList)
        {
            var idScalarDictionary =
                termsList
                    .CreateDictionary(BasisBladeUtils.BasisBladeGradeIndexToId)
                    .CreateLinVectorStorage();

            return MultivectorStorage<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> SumToMultivectorSparseStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor.CreateVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> SumToMultivectorSparseStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return scalarProcessor.CreateVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateMultivectorSparseStorage();
        }
    }
}