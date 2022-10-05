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
        public static GaBivector<T> CopyToBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                indexScalarDictionary.CopyToBivectorStorage()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> SumToBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.SumToBivectorStorage(termsList)
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorZero<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                BivectorStorage<T>.ZeroBivector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index, T scalar)
        {
            return new GaBivector<T>(
                geometricProcessor,
                geometricProcessor.CreateBivectorStorageTerm((ulong) index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index, T scalar)
        {
            return new GaBivector<T>(
                geometricProcessor,
                geometricProcessor.CreateBivectorStorageTerm(index, scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorageTerm(indexScalarPair.Index, indexScalarPair.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorageTerm((ulong) index, geometricProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorageTerm(index, geometricProcessor.ScalarOne)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateStorageBivectorOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.ScalarOne.CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateStorageBivectorUnitOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            var length = geometricProcessor.Sqrt(termsCount);

            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor
                        .Divide(geometricProcessor.ScalarOne, length)
                        .CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return new GaBivector<T>(
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
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorStorage<T> storage)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorGradedStorage<T> storage)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, BivectorStorage<T> storage)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                storage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this BivectorStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                storage
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params int[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<int> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params uint[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<uint> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params long[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<long> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params ulong[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<ulong> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params float[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<float> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params double[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<double> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromText(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params string[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<string> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params object[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromObjects(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<object> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(geometricProcessor.CreateLinVectorStorage(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params int[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params long[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params uint[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params ulong[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params float[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params double[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params string[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params T[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params object[] scalarArray)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<T> scalarList)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarList.ToArray())
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageZero<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                BivectorStorage<T>.ZeroBivector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorStorageRepeatedScalar<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int count, T value)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) value.CreateLinVectorDenseStorage(count)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T value)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(
                    (ILinVectorStorage<T>) new LinVectorSingleScalarDenseStorage<T>(value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return new GaBivector<T>(
                geometricProcessor, 
                geometricProcessor.CreateBivectorStorage(indexScalarRecords.CreateLinVectorStorage())
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index1, int index2, T scalar)
        {
            if (index1 < index2)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2), 
                        scalar
                    )
                );

            if (index2 < index1)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                        geometricProcessor.Negative(scalar)
                    )
                );

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index1, ulong index2, T scalar)
        {
            if (index1 < index2)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                        scalar
                    )
                );

            if (index2 < index1)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                    geometricProcessor.Negative(scalar)
                    )
                );

            throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index1, int index2)
        {
            if (index1 < index2)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                        geometricProcessor.ScalarOne
                    )
                );
            
            if (index2 < index1)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index2, index1),
                        geometricProcessor.ScalarMinusOne
                    )
                );
            
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index1, ulong index2)
        {
            if (index1 < index2)
                return new GaBivector<T>(
                    geometricProcessor, 
                    geometricProcessor.CreateBivectorTermStorage(
                        BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(index1, index2),
                        geometricProcessor.ScalarOne
                    )
                );
            
            if (index2 < index1)
                return new GaBivector<T>(
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