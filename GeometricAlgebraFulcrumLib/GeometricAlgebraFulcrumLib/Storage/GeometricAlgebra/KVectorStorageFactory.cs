using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class KVectorStorageFactory
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static VectorSparseStorageComposer<T> CreateVectorStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        //{
        //    return new VectorSparseStorageComposer<T>(scalarProcessor);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static VectorDenseStorageComposer<T> CreateVectorStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int count) 
        //{
        //    return new VectorDenseStorageComposer<T>(scalarProcessor, count);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CopyToKVectorStorage<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary, uint grade)
        {
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            var evenDictionary =
                indexScalarDictionary.ToDictionary();

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(evenDictionary),
                2 => BivectorStorage<T>.Create(evenDictionary),
                _ => KVectorStorage<T>.CreateKVector(grade, evenDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStoragePseudoScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = vSpaceDimension.PseudoScalarIndex();
            var scalar = scalarProcessor.ScalarOne;

            return vSpaceDimension switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStoragePseudoScalar<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            var vSpaceDimension = processor.VSpaceDimension;
            var index = vSpaceDimension.PseudoScalarIndex();
            var scalar = processor.ScalarOne;

            return vSpaceDimension switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStoragePseudoScalarReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index =
                vSpaceDimension.PseudoScalarIndex();

            var scalar =
                vSpaceDimension.ReverseIsNegativeOfGrade()
                    ? scalarProcessor.ScalarMinusOne
                    : scalarProcessor.ScalarOne;

            return vSpaceDimension switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageEuclideanPseudoScalarInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return scalarProcessor.CreateKVectorStoragePseudoScalarReverse(vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStoragePseudoScalarInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet)
        {
            return scalarProcessor
                .BladeInverse(
                    basisSet,
                    scalarProcessor.CreateKVectorStoragePseudoScalar(basisSet.VSpaceDimension)
                ).GetKVectorPart(basisSet.VSpaceDimension);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageZero<T>(this uint grade)
        {
            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorZero(0),
                1 => VectorStorage<T>.ZeroVector,
                2 => BivectorStorage<T>.ZeroBivector,
                _ => KVectorStorage<T>.CreateKVectorZero(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageZero<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade)
        {
            return grade switch
            {
                0 => KVectorStorage<T>.ZeroScalar,
                1 => VectorStorage<T>.ZeroVector,
                2 => BivectorStorage<T>.ZeroBivector,
                _ => KVectorStorage<T>.CreateKVectorZero(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasisScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return KVectorStorage<T>.CreateKVectorScalar(scalarProcessor.ScalarOne);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params int[] basisVectorIndices)
        {
            var basisVectorIndicesArray = basisVectorIndices.ToArray();
            var swapCount = basisVectorIndicesArray.SortWithSwapCount();
            var basisBladeId = basisVectorIndicesArray.Aggregate(
                0UL,
                (id, index) => id | 1UL << index
            );

            return CreateKVectorStorageTerm(
                basisBladeId,
                swapCount.IsEven()
                    ? scalarProcessor.ScalarOne
                    : scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, params ulong[] basisVectorIndices)
        {
            var basisVectorIndicesArray = basisVectorIndices.ToArray();
            var swapCount = basisVectorIndicesArray.SortWithSwapCount();
            var basisBladeId = basisVectorIndicesArray.Aggregate(
                0UL,
                (id, index) => id | 1UL << (int)index
            );

            return CreateKVectorStorageTerm(
                basisBladeId,
                swapCount.IsEven()
                    ? scalarProcessor.ScalarOne
                    : scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<int> basisVectorIndices)
        {
            var basisVectorIndicesArray = basisVectorIndices.ToArray();
            var swapCount = basisVectorIndicesArray.SortWithSwapCount();
            var basisBladeId = basisVectorIndicesArray.Aggregate(
                0UL,
                (id, index) => id | 1UL << index
            );

            return CreateKVectorStorageTerm(
                basisBladeId,
                swapCount.IsEven()
                    ? scalarProcessor.ScalarOne
                    : scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<ulong> basisVectorIndices)
        {
            var basisVectorIndicesArray = basisVectorIndices.ToArray();
            var swapCount = basisVectorIndicesArray.SortWithSwapCount();
            var basisBladeId = basisVectorIndicesArray.Aggregate(
                0UL,
                (id, index) => id | 1UL << (int)index
            );

            return CreateKVectorStorageTerm(
                basisBladeId,
                swapCount.IsEven()
                    ? scalarProcessor.ScalarOne
                    : scalarProcessor.ScalarMinusOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong id)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalarProcessor.ScalarOne),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalarProcessor.ScalarOne),
                2 => BivectorStorage<T>.Create(index, scalarProcessor.ScalarOne),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalarProcessor.ScalarOne)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageBasis<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, ulong index)
        {
            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalarProcessor.ScalarOne),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalarProcessor.ScalarOne),
                2 => BivectorStorage<T>.Create(index, scalarProcessor.ScalarOne),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalarProcessor.ScalarOne)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, long scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, float scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, double scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromNumber(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this T scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, string scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromText(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, object scalar)
        {
            return KVectorStorage<T>.CreateKVectorScalar(
                scalarProcessor.GetScalarFromObject(scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this BasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndexRecord();

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndexRecord();

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(ulong id, T scalar)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong id, T scalar)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IndexScalarRecord<T> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorageTerm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, IndexScalarRecord<T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params T[] scalarsArray)
        {
            if (grade == 0)
            {
                return scalarsArray.Length >= 1
                    ? KVectorStorage<T>.CreateKVectorScalar(scalarsArray[0])
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(scalarsArray),
                2 => BivectorStorage<T>.Create(scalarsArray),
                _ => KVectorStorage<T>.CreateKVector(grade, scalarsArray)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params int[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromNumber).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params uint[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromNumber).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params long[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromNumber).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params ulong[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromNumber).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params float[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromNumber).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params double[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromNumber).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params string[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromText).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, params object[] scalarsArray)
        {
            return scalarProcessor.CreateKVectorStorage(
                grade,
                scalarsArray.Select(scalarProcessor.GetScalarFromObject).ToArray()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, IEnumerable<T> scalarsList)
        {
            if (grade == 0)
            {
                var scalarsArray =
                    scalarsList.Take(1).ToArray();

                return scalarsArray.Length == 1
                    ? KVectorStorage<T>.CreateKVectorScalar(scalarsArray[0])
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(scalarsList),
                2 => BivectorStorage<T>.Create(scalarsList),
                _ => KVectorStorage<T>.CreateKVector(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            var indexScalarDictionary =
                termsList.CreateDictionary();

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(indexScalarDictionary),
                2 => BivectorStorage<T>.Create(indexScalarDictionary),
                _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, IEnumerable<BasisTerm<T>> termsList)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(indexScalarDictionary),
                2 => BivectorStorage<T>.Create(indexScalarDictionary),
                _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> SumToKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return scalarProcessor
                .CreateVectorStorageComposer()
                .SetTerms(termsList)
                .RemoveZeroTerms()
                .CreateKVectorStorage(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(indexScalarDictionary),
                2 => BivectorStorage<T>.Create(indexScalarDictionary),
                _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<T> scalarsList, uint grade)
        {
            if (grade == 0)
            {
                var scalarsArray =
                    scalarsList.Take(1).ToArray();

                return scalarsArray.Length == 1
                    ? KVectorStorage<T>.CreateKVectorScalar(scalarsArray[0])
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(scalarsList),
                2 => BivectorStorage<T>.Create(scalarsList),
                _ => KVectorStorage<T>.CreateKVector(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList, uint grade)
        {
            var indexScalarDictionary =
                termsList.CreateDictionary();

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(indexScalarDictionary),
                2 => BivectorStorage<T>.Create(indexScalarDictionary),
                _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this ILinVectorStorage<T> termsList, uint grade)
        {
            if (grade == 0)
            {
                return termsList.TryGetScalar(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(termsList),
                2 => BivectorStorage<T>.Create(termsList),
                _ => KVectorStorage<T>.CreateKVector(grade, termsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this GradeLinVectorStorageRecord<T> gradeListRecord)
        {
            var (grade, termsList) = gradeListRecord;

            return termsList.CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<BasisTerm<T>> termsList, uint grade)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? KVectorStorage<T>.CreateKVectorScalar(scalar)
                    : KVectorStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => VectorStorage<T>.CreateVectorStorage(indexScalarDictionary),
                2 => BivectorStorage<T>.Create(indexScalarDictionary),
                _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
            };
        }
    }
}