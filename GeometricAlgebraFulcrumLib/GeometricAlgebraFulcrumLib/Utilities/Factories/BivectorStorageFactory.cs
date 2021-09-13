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
    public static class BivectorStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CopyToBivectorStorage<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return BivectorStorage<T>.CreateBivector(evenDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> SumToBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor.CreateVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateBivectorStorage();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorZeroStorage<T>()
        {
            return BivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorZeroStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return BivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(int index, T scalar)
        {
            return BivectorStorage<T>.CreateBivector((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(ulong index, T scalar)
        {
            return BivectorStorage<T>.CreateBivector(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index, T scalar)
        {
            return BivectorStorage<T>.CreateBivector((ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return BivectorStorage<T>.CreateBivector(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(this IndexScalarRecord<T> indexScalarPair)
        {
            return BivectorStorage<T>.CreateBivector(indexScalarPair.Index, indexScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return BivectorStorage<T>.CreateBivector(indexScalarPair.Index, indexScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorBasisStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index)
        {
            return BivectorStorage<T>.CreateBivector((ulong) index, scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorBasisStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index)
        {
            return BivectorStorage<T>.CreateBivector(index, scalarProcessor.ScalarOne);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateStorageBivectorOnes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.ScalarOne
                    .CreateLinVectorRepeatedScalarStorage(termsCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateStorageBivectorUnitOnes<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount)
        {
            var length = scalarProcessor.Sqrt(termsCount);

            return BivectorStorage<T>.CreateBivector(
                scalarProcessor
                    .Divide(scalarProcessor.ScalarOne, length)
                    .CreateLinVectorRepeatedScalarStorage(termsCount)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(params T[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IReadOnlyList<T> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(scalarList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IEnumerable<T> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return BivectorStorage<T>.CreateBivector(
                termsList.CreateDictionary()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return BivectorStorage<T>.CreateBivector(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return BivectorStorage<T>.CreateBivector(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index, 
                    t => t.Scalar
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> storage)
        {
            return BivectorStorage<T>.CreateBivector(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this ILinVectorStorage<T> storage)
        {
            return BivectorStorage<T>.CreateBivector(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> storage)
        {
            return BivectorStorage<T>.CreateBivector(
                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this ILinVectorGradedStorage<T> storage)
        {
            return BivectorStorage<T>.CreateBivector(
                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params uint[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params long[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params float[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params double[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromText(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params string[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromText(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromText(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params object[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromObjects(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<object> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray())
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorage(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorage(scalarArray)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            return BivectorStorage<T>.CreateBivector(
                scalarProcessor.CreateLinVectorStorage(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return BivectorStorage<T>.CreateBivector(
                LinVectorEmptyStorage<T>.EmptyStorage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageRepeatedScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count, T value)
        {
            return BivectorStorage<T>.CreateBivector(
                value.CreateLinVectorDenseStorage(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T value)
        {
            return BivectorStorage<T>.CreateBivector(
                new LinVectorSingleScalarDenseStorage<T>(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index, T value)
        {
            return BivectorStorage<T>.CreateBivector(
                value.CreateLinVectorSingleScalarStorage(index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return BivectorStorage<T>.CreateBivector(
                indexScalarRecords.CreateLinVectorStorage()
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(int index1, int index2, T scalar)
        {
            if (index1 >= index2) 
                throw new InvalidOperationException();

            var index = BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2);

            return BivectorStorage<T>.CreateBivector(index, scalar);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index1, int index2, T scalar)
        {
            if (index1 < index2)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                    scalar
                );

            if (index2 < index1)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                    scalarProcessor.Negative(scalar)
                );

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorTermStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index1, ulong index2, T scalar)
        {
            if (index1 < index2)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                    scalar
                );

            if (index2 < index1)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                    scalarProcessor.Negative(scalar)
                );

            throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorBasisStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index1, int index2)
        {
            if (index1 < index2)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                    scalarProcessor.ScalarOne
                );
            
            if (index2 < index1)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                    scalarProcessor.ScalarMinusOne
                );
            
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> CreateBivectorBasisStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index1, ulong index2)
        {
            if (index1 < index2)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                    scalarProcessor.ScalarOne
                );
            
            if (index2 < index1)
                return BivectorStorage<T>.CreateBivector(
                    BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                    scalarProcessor.ScalarMinusOne
                );
            
            throw new InvalidOperationException();
        }
    }
}