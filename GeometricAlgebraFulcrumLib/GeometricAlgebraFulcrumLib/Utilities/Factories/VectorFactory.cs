using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;
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
        public static GaVector<T> CopyToVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor, 
                indexScalarDictionary.ToDictionary(
                    pair => pair.Key, 
                    pair => pair.Value
                ).CreateVectorStorage()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> SumToVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return geometricProcessor
                .CreateVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateVector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorZero<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaVector<T>(
                geometricProcessor,
                VectorStorage<T>.ZeroVector
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index, T scalar)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageTerm((ulong) index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index, T scalar)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageTerm(index, scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageTerm(indexScalarPair.Index, indexScalarPair.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int index)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageTerm((ulong) index, geometricProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ulong index)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageTerm(index, geometricProcessor.ScalarOne)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaVector<T>> CreateVectorBasis<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return geometricProcessor
                .VSpaceDimension
                .GetRange()
                .Select(index => geometricProcessor.CreateVectorBasis(index));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorSymmetric<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageSymmetric(termsCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorSymmetricUnit<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorageSymmetricUnit(termsCount)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorAverageOnes<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount)
        {
            var scalar = 
                geometricProcessor.Inverse(geometricProcessor.GetScalarFromNumber(termsCount));

            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) scalar.CreateLinVectorRepeatedScalarStorage(termsCount)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return new GaVector<T>(
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
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorStorage<T> storage)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this ILinVectorStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinVectorGradedStorage<T> storage)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this ILinVectorGradedStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, VectorStorage<T> storage)
        {
            return new GaVector<T>(
                geometricProcessor,
                storage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, VectorStorage<T> storage, bool makeUnitVector)
        {
            if (makeUnitVector)
                return new GaVector<T>(
                    geometricProcessor,
                    geometricProcessor.DivideByNorm(storage)
                );

            return new GaVector<T>(
                geometricProcessor,
                storage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateUnitVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, VectorStorage<T> storage)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.DivideByNorm(storage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateEUnitVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, VectorStorage<T> storage)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.DivideByENorm(storage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this VectorStorage<T> storage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaVector<T>(
                geometricProcessor,
                storage
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params int[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<int> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params uint[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<uint> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params long[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<long> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params ulong[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<ulong> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params float[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<float> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params double[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromNumbers<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<double> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromText(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params string[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromText<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<string> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromText(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params object[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromObjects(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorFromObjects<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<object> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray())
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(geometricProcessor.CreateLinVectorStorage(indexScalarDictionary))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params int[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params uint[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params long[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params ulong[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params float[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params double[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params string[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T scalar1)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(scalar1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T scalar1, T scalar2)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(scalar1, scalar2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T scalar1, T scalar2, T scalar3)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(scalar1, scalar2, scalar3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, params T[] scalarArray)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IEnumerable<T> scalarArray, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this T[] scalarArray, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarArray)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<T> scalarList)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) geometricProcessor.CreateLinVectorStorage(scalarList.ToArray())
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorRepeatedScalar<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int count, T value)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) value.CreateLinVectorDenseStorage(count)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVectorTerm<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T value)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(
                    (ILinVectorStorage<T>) new LinVectorSingleScalarDenseStorage<T>(value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return new GaVector<T>(
                geometricProcessor,
                geometricProcessor.CreateVectorStorage(indexScalarRecords.CreateLinVectorStorage())
            );
        }


        
        public static GaVector<ScalarSignalFloat64> CreateVectorSignal(this IGeometricAlgebraProcessor<ScalarSignalFloat64> geometricProcessor, IEnumerable<GaVector<double>> vectorList, double samplingRate)
        {
            var vSpaceDimension = geometricProcessor.VSpaceDimension;
            var vectorArray = vectorList.ToArray();
            var scalarArray = new ScalarSignalFloat64[vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
            {
                var index = i;

                scalarArray[i] = vectorArray
                    .Select(v => v[index].ScalarValue)
                    .CreateSignal(samplingRate);
            }

            return geometricProcessor.CreateVector(scalarArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<ScalarSignalFloat64> CreateVectorSignal(this IEnumerable<GaVector<double>> vectorList, IGeometricAlgebraProcessor<ScalarSignalFloat64> geometricProcessor, double samplingRate)
        {
            return geometricProcessor.CreateVectorSignal(vectorList, samplingRate);
        }

        public static GaVector<IReadOnlyList<T>> CreateVector<T>(this IGeometricAlgebraProcessor<IReadOnlyList<T>> geometricProcessor, IReadOnlyList<GaVector<T>> vectorList)
        {
            var vSpaceDimension = geometricProcessor.VSpaceDimension;
            var scalarArray = new IReadOnlyList<T>[vSpaceDimension];

            for (var i = 0; i < vSpaceDimension; i++)
                scalarArray[i] = vectorList.Select(v => v[i].ScalarValue).ToArray();

            return geometricProcessor.CreateVector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<IReadOnlyList<T>> CreateVector<T>(this IReadOnlyList<GaVector<T>> vectorList, IGeometricAlgebraProcessor<IReadOnlyList<T>> geometricProcessor)
        {
            return geometricProcessor.CreateVector(vectorList);
        }
    }
}