using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public static class BivectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CopyToBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                indexScalarDictionary.CopyToBivectorStorage()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> SumToBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.SumToBivectorStorage(termsList)
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorZero<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Bivector<T>(
                geometricProcessor, 
                BivectorStorage<T>.ZeroBivector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index, T scalar)
        {
            return new Bivector<T>(
                geometricProcessor,
                geometricProcessor.CreateBivectorStorageTerm((ulong) index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index, T scalar)
        {
            return new Bivector<T>(
                geometricProcessor,
                geometricProcessor.CreateBivectorStorageTerm(index, scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorageTerm(indexScalarPair.Index, indexScalarPair.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorageTerm((ulong) index, geometricProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorageTerm(index, geometricProcessor.ScalarOne)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateStorageBivectorOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    geometricProcessor.ScalarOne.CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateStorageBivectorUnitOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            var length = geometricProcessor.Sqrt(termsCount);

            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    geometricProcessor
                        .Divide(geometricProcessor.ScalarOne, length)
                        .CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    termsList.ToDictionary(
                        t => t.BasisBlade.Index, 
                        t => t.Scalar
                    )
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorStorage<T> storage)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorGradedStorage<T> storage)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, BivectorStorage<T> storage)
        {
            return new Bivector<T>(
                geometricProcessor, 
                storage
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params int[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<int> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params uint[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<uint> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params long[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<long> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params ulong[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<ulong> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params float[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<float> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params double[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<double> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromText(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params string[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromText(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<string> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromText(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params object[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<object> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray()))
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorage(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params T[] scalarArray)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorage(scalarArray))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<T> scalarList)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorage(scalarList.ToArray()))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageZero<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Bivector<T>(
                geometricProcessor, 
                BivectorStorage<T>.ZeroBivector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorStorageRepeatedScalar<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int count, T value)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(value.CreateLinVectorDenseStorage(count))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T value)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(new LinVectorSingleScalarDenseStorage<T>(value))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return new Bivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(indexScalarRecords.CreateLinVectorStorage())
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index1, int index2, T scalar)
        {
            if (index1 < index2)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2), 
                        scalar
                    )
                );

            if (index2 < index1)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                        geometricProcessor.Negative(scalar)
                    )
                );

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index1, ulong index2, T scalar)
        {
            if (index1 < index2)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                        scalar
                    )
                );

            if (index2 < index1)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                    geometricProcessor.Negative(scalar)
                    )
                );

            throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index1, int index2)
        {
            if (index1 < index2)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                        geometricProcessor.ScalarOne
                    )
                );
            
            if (index2 < index1)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                        geometricProcessor.ScalarMinusOne
                    )
                );
            
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index1, ulong index2)
        {
            if (index1 < index2)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                        geometricProcessor.ScalarOne
                    )
                );
            
            if (index2 < index1)
                return new Bivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                        geometricProcessor.ScalarMinusOne
                    )
                );
            
            throw new InvalidOperationException();
        }
    }
}