using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class VectorStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CopyToVectorStorage<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return VectorStorage<T>.CreateVectorStorage(evenDictionary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> SumToVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor.CreateVectorStorageComposer()
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .CreateVectorStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageZero<T>()
        {
            return VectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return VectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(int index, T scalar)
        {
            return VectorStorage<T>.CreateVectorStorage((ulong)index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(ulong index, T scalar)
        {
            return VectorStorage<T>.CreateVectorStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index, T scalar)
        {
            return VectorStorage<T>.CreateVectorStorage((ulong)index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index, T scalar)
        {
            return VectorStorage<T>.CreateVectorStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(this IndexScalarRecord<T> indexScalarPair)
        {
            return VectorStorage<T>.CreateVectorStorage(indexScalarPair.Index, indexScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IndexScalarRecord<T> indexScalarPair)
        {
            return VectorStorage<T>.CreateVectorStorage(indexScalarPair.Index, indexScalarPair.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int index)
        {
            return VectorStorage<T>.CreateVectorStorage((ulong)index, scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong index)
        {
            return VectorStorage<T>.CreateVectorStorage(index, scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageSymmetric<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.ScalarOne.CreateLinVectorRepeatedScalarStorage(termsCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageSymmetricUnit<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount)
        {
            var length = scalarProcessor.Sqrt(termsCount);

            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor
                    .Divide(scalarProcessor.ScalarOne, length)
                    .CreateLinVectorRepeatedScalarStorage(termsCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this Dictionary<ulong, T> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(params T[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IReadOnlyList<T> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IEnumerable<T> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageBibolar<T>(this IScalarAlgebraProcessor<T> processor, IEnumerable<char> basisVectorSignatures)
        {
            var scalarList =
                basisVectorSignatures.Select(c =>
                    c switch
                    {
                        '+' or 'p' or 'P' => processor.ScalarOne,
                        '-' or 'n' or 'N' => processor.ScalarMinusOne,
                        _ => processor.ScalarZero
                    }
                );

            return VectorStorage<T>.CreateVectorStorage(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageBibolar<T>(this IScalarAlgebraProcessor<T> processor, IEnumerable<int> basisVectorSignatures)
        {
            var scalarList =
                basisVectorSignatures.Select(c =>
                    c switch
                    {
                        > 0 => processor.ScalarOne,
                        < 0 => processor.ScalarMinusOne,
                        _ => processor.ScalarZero
                    }
                );

            return VectorStorage<T>.CreateVectorStorage(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageBibolar<T>(this IScalarAlgebraProcessor<T> processor, params int[] basisVectorSignatures)
        {
            var scalarList =
                basisVectorSignatures.Select(c =>
                    c switch
                    {
                        > 0 => processor.ScalarOne,
                        < 0 => processor.ScalarMinusOne,
                        _ => processor.ScalarZero
                    }
                );

            return VectorStorage<T>.CreateVectorStorage(scalarList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                termsList.CreateDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IEnumerable<BasisTerm<T>> termsList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index,
                    t => t.Scalar
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<BasisTerm<T>> termsList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                termsList.ToDictionary(
                    t => t.BasisBlade.Index,
                    t => t.Scalar
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> storage)
        {
            return VectorStorage<T>.CreateVectorStorage(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this ILinVectorStorage<T> storage)
        {
            return VectorStorage<T>.CreateVectorStorage(storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> storage)
        {
            return VectorStorage<T>.CreateVectorStorage(
                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this ILinVectorGradedStorage<T> storage)
        {
            return VectorStorage<T>.CreateVectorStorage(
                storage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, int> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, int> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, uint> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, uint> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params uint[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, long> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, long> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params long[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, ulong> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, ulong> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, float> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, float> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params float[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromNumbers(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, double> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params double[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromNumbers<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromNumbers(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, string> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromText(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, string> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromText(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params string[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromText(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromText<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromText(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, object> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorageFromObjects(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, object> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromObjects(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params object[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromObjects(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageFromObjects<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<object> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorageFromObjects(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Dictionary<ulong, T> indexScalarDictionary)
        {
            return VectorStorage<T>.CreateVectorStorage(
                scalarProcessor.CreateLinVectorStorage(indexScalarDictionary)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint termsCount, Func<ulong, T> indexToScalarFunc)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(termsCount, indexToScalarFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(scalarList.ToArray())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1)
        {
            return scalarProcessor.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(scalar1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T scalar2)
        {
            return scalarProcessor.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(scalar1, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
        {
            return scalarProcessor.CreateVectorStorage(
                (ILinVectorStorage<T>)scalarProcessor.CreateLinVectorStorage(scalar1, scalar2, scalar3)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageRepeatedScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count, T value)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)value.CreateLinVectorDenseStorage(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T value)
        {
            return VectorStorage<T>.CreateVectorStorage(
                (ILinVectorStorage<T>)new LinVectorSingleScalarDenseStorage<T>(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> CreateVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            return VectorStorage<T>.CreateVectorStorage(
                indexScalarRecords.CreateLinVectorStorage()
            );
        }



    }
}