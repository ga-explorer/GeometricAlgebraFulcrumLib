using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaKVectorStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSparseEvenStorageComposer<T> CreateKVectorStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaVectorSparseEvenStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseEvenStorageComposer<T> CreateKVectorStorageComposer<T>(this IScalarProcessor<T> scalarProcessor, int count) 
        {
            return new LaVectorDenseEvenStorageComposer<T>(scalarProcessor, count);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CopyToKVectorStorage<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary, uint grade)
        {
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            var evenDictionary = 
                indexScalarDictionary.CopyToDictionary();

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(evenDictionary),
                2 => GaBivectorStorage<T>.Create(evenDictionary),
                _ => GaKVectorStorage<T>.Create(grade, evenDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreatePseudoScalarStorage<T>(this IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = vSpaceDimension.PseudoScalarIndex();
            var scalar = scalarProcessor.ScalarOne;

            return vSpaceDimension switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreatePseudoScalarReverseStorage<T>(this IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = 
                vSpaceDimension.PseudoScalarIndex();

            var scalar = 
                vSpaceDimension.GradeHasNegativeReverse()
                    ? scalarProcessor.ScalarMinusOne 
                    : scalarProcessor.ScalarOne;

            return vSpaceDimension switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateEuclideanPseudoScalarInverseStorage<T>(this IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return CreatePseudoScalarReverseStorage(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreatePseudoScalarInverseStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature baseSignature)
        {
            return scalarProcessor
                .BladeInverse(
                    baseSignature,
                    scalarProcessor.CreatePseudoScalarStorage(baseSignature.VSpaceDimension)
                ).GetKVectorPart(baseSignature.VSpaceDimension);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateZeroKVectorStorage<T>(this uint grade)
        {
            return grade switch
            {
                0 => GaKVectorStorage<T>.ZeroKVector(0),
                1 => GaVectorStorage<T>.ZeroVector,
                2 => GaBivectorStorage<T>.ZeroBivector,
                _ => GaKVectorStorage<T>.ZeroKVector(grade)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateZeroKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.ZeroScalar,
                1 => GaVectorStorage<T>.ZeroVector,
                2 => GaBivectorStorage<T>.ZeroBivector,
                _ => GaKVectorStorage<T>.ZeroKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong id)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalarProcessor.ScalarOne),
                1 => GaVectorStorage<T>.Create(index, scalarProcessor.ScalarOne),
                2 => GaBivectorStorage<T>.Create(index, scalarProcessor.ScalarOne),
                _ => GaKVectorStorage<T>.Create(grade, index, scalarProcessor.ScalarOne)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, ulong index)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalarProcessor.ScalarOne),
                1 => GaVectorStorage<T>.Create(index, scalarProcessor.ScalarOne),
                2 => GaBivectorStorage<T>.Create(index, scalarProcessor.ScalarOne),
                _ => GaKVectorStorage<T>.Create(grade, index, scalarProcessor.ScalarOne)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this GaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndexRecord();

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, GaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndexRecord();

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(ulong id, T scalar)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, ulong id, T scalar)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, IndexScalarRecord<T> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, IndexScalarRecord<T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<T> scalarsList)
        {
            if (grade == 0)
            {
                var scalarsArray = 
                    scalarsList.Take(1).ToArray();

                return scalarsArray.Length == 1
                    ? GaScalarStorage<T>.Create(scalarsArray[0])
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(scalarsList),
                2 => GaBivectorStorage<T>.Create(scalarsList),
                _ => GaKVectorStorage<T>.Create(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            var indexScalarDictionary = 
                termsList.CreateDictionary();

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                _ => GaKVectorStorage<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaBasisTerm<T>> termsList)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                _ => GaKVectorStorage<T>.Create(grade, indexScalarDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> SumToKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<IndexScalarRecord<T>> termsList)
        {
            return CreateKVectorStorageComposer(scalarProcessor)
                .SetTerms(termsList)
                .RemoveZeroTerms()
                .CreateGaKVectorStorage(grade);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IScalarProcessor<T> scalarProcessor, uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                _ => GaKVectorStorage<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<T> scalarsList, uint grade)
        {
            if (grade == 0)
            {
                var scalarsArray = 
                    scalarsList.Take(1).ToArray();

                return scalarsArray.Length == 1
                    ? GaScalarStorage<T>.Create(scalarsArray[0])
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(scalarsList),
                2 => GaBivectorStorage<T>.Create(scalarsList),
                _ => GaKVectorStorage<T>.Create(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<IndexScalarRecord<T>> termsList, uint grade)
        {
            var indexScalarDictionary = 
                termsList.CreateDictionary();
            
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                _ => GaKVectorStorage<T>.Create(grade, indexScalarDictionary)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this ILaVectorEvenStorage<T> termsList, uint grade)
        {
            if (grade == 0)
            {
                return termsList.TryGetScalar(0UL, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(termsList),
                2 => GaBivectorStorage<T>.Create(termsList),
                _ => GaKVectorStorage<T>.Create(grade, termsList)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this GradeVectorStorageRecord<T> gradeListRecord)
        {
            var (grade, termsList) = gradeListRecord;

            return termsList.CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CreateKVectorStorage<T>(this IEnumerable<GaBasisTerm<T>> termsList, uint grade)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );
            
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaScalarStorage<T>.Create(scalar)
                    : GaScalarStorage<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                _ => GaKVectorStorage<T>.Create(grade, indexScalarDictionary)
            };
        }
    }
}