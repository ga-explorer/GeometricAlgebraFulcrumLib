using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class VectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CopyToVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor, 
                indexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => pair.Value
                ).CreateVectorStorage()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> SumToVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return geometricProcessor
                .CreateVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateVector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorZero<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Vector<T>(
                geometricProcessor,
                VectorStorage<T>.ZeroVector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index, T scalar)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorTermStorage((ulong) index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index, T scalar)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorTermStorage(index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorTermStorage(indexScalarPair.Index, indexScalarPair.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorTermStorage((ulong) index, geometricProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorTermStorage(index, geometricProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector<T>> CreateVectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return geometricProcessor
                .VSpaceDimension
                .GetRange()
                .Select(index => geometricProcessor.CreateVectorBasis(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            var scalar =
                geometricProcessor.ScalarOne;

            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    scalar.CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorUnitOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            var scalar = 
                geometricProcessor.Inverse(geometricProcessor.Sqrt(termsCount));

            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    scalar.CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorAverageOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            var scalar = 
                geometricProcessor.Inverse(geometricProcessor.GetScalarFromNumber(termsCount));

            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    scalar.CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    termsList.ToDictionary(
                        t => t.BasisBlade.Index, 
                        t => t.Scalar
                    )
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorStorage<T> storage)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this ILinVectorStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorGradedStorage<T> storage)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this ILinVectorGradedStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, VectorStorage<T> storage)
        {
            return new Vector<T>(
                geometricProcessor,
                storage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this VectorStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Vector<T>(
                geometricProcessor,
                storage
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params int[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<int> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params uint[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<uint> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params long[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<long> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params ulong[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<ulong> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params float[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<float> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params double[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<double> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromText(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params string[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromText(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<string> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromText(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params object[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<object> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorage(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params T[] scalarArray)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorage(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<T> scalarList)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorage(scalarList.ToArray()))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorRepeatedScalar<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int count, T value)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(value.CreateLinVectorDenseStorage(count))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T value)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(new LinVectorSingleScalarDenseStorage<T>(value))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return new Vector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(indexScalarRecords.CreateLinVectorStorage())
            );
        }
    }
}