using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageKVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenComposerSparse<T> CreateStorageKVectorComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaListEvenComposerSparse<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenComposerDense<T> CreateStorageKVectorComposer<T>(this IGaScalarProcessor<T> scalarProcessor, int count) 
        {
            return new GaListEvenComposerDense<T>(scalarProcessor, count);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CopyToStorageKVector<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary, uint grade)
        {
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            var evenDictionary = 
                indexScalarDictionary.CopyToDictionary();

            return grade switch
            {
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => GaStorageKVector<T>.Create(grade, evenDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStoragePseudoScalar<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = vSpaceDimension.PseudoScalarIndex();
            var scalar = scalarProcessor.GetOneScalar();

            return vSpaceDimension switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStoragePseudoScalarReverse<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = 
                vSpaceDimension.PseudoScalarIndex();

            var scalar = 
                vSpaceDimension.GradeHasNegativeReverse()
                    ? scalarProcessor.GetMinusOneScalar() 
                    : scalarProcessor.GetOneScalar();

            return vSpaceDimension switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(vSpaceDimension, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageEuclideanPseudoScalarInverse<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return CreateStoragePseudoScalarReverse(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStoragePseudoScalarInverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature baseSignature)
        {
            return scalarProcessor
                .BladeInverse(
                    baseSignature,
                    scalarProcessor.CreateStoragePseudoScalar(baseSignature.VSpaceDimension)
                ).GetKVectorPart(baseSignature.VSpaceDimension);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageZeroKVector<T>(this uint grade)
        {
            return grade switch
            {
                0 => GaStorageKVector<T>.ZeroKVector(0),
                1 => GaStorageVector<T>.ZeroVector,
                2 => GaStorageBivector<T>.ZeroBivector,
                _ => GaStorageKVector<T>.ZeroKVector(grade)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageZeroKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.ZeroScalar,
                1 => GaStorageVector<T>.ZeroVector,
                2 => GaStorageBivector<T>.ZeroBivector,
                _ => GaStorageKVector<T>.ZeroKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageBasisBlade<T>(this IGaScalarProcessor<T> scalarProcessor, ulong id)
        {
            var (grade, index) = id.BasisBladeIdToGradeIndex();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalarProcessor.GetOneScalar()),
                1 => GaStorageVector<T>.Create(index, scalarProcessor.GetOneScalar()),
                2 => GaStorageBivector<T>.Create(index, scalarProcessor.GetOneScalar()),
                _ => GaStorageKVector<T>.Create(grade, index, scalarProcessor.GetOneScalar())
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageBasisBlade<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, ulong index)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalarProcessor.GetOneScalar()),
                1 => GaStorageVector<T>.Create(index, scalarProcessor.GetOneScalar()),
                2 => GaStorageBivector<T>.Create(index, scalarProcessor.GetOneScalar()),
                _ => GaStorageKVector<T>.Create(grade, index, scalarProcessor.GetOneScalar())
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this GaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndexRecord();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, GaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndexRecord();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(ulong id, T scalar)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, ulong id, T scalar)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, ulong index, T scalar)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, GaRecordKeyValue<T> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, GaRecordKeyValue<T> indexScalarPair)
        {
            var (index, scalar) = indexScalarPair;

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<T> scalarsList)
        {
            if (grade == 0)
            {
                var scalarsArray = 
                    scalarsList.Take(1).ToArray();

                return scalarsArray.Length == 1
                    ? GaStorageScalar<T>.Create(scalarsArray[0])
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(scalarsList),
                2 => GaStorageBivector<T>.Create(scalarsList),
                _ => GaStorageKVector<T>.Create(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaRecordKeyValue<T>> termsList)
        {
            var indexScalarDictionary = 
                termsList.CreateDictionary();

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaBasisTerm<T>> termsList)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );

            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> SumToStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaRecordKeyValue<T>> termsList)
        {
            return CreateStorageKVectorComposer(scalarProcessor)
                .SetTerms(termsList)
                .RemoveZeroTerms()
                .CreateStorageKVector(grade);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<T> scalarsList, uint grade)
        {
            if (grade == 0)
            {
                var scalarsArray = 
                    scalarsList.Take(1).ToArray();

                return scalarsArray.Length == 1
                    ? GaStorageScalar<T>.Create(scalarsArray[0])
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(scalarsList),
                2 => GaStorageBivector<T>.Create(scalarsList),
                _ => GaStorageKVector<T>.Create(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<GaRecordKeyValue<T>> termsList, uint grade)
        {
            var indexScalarDictionary = 
                termsList.CreateDictionary();
            
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaListEven<T> termsList, uint grade)
        {
            if (grade == 0)
            {
                return termsList.TryGetValue(0UL, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(termsList),
                2 => GaStorageBivector<T>.Create(termsList),
                _ => GaStorageKVector<T>.Create(grade, termsList)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this GaRecordGradeEvenList<T> gradeListRecord)
        {
            var (grade, termsList) = gradeListRecord;

            return termsList.CreateStorageKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<GaBasisTerm<T>> termsList, uint grade)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );
            
            if (grade == 0)
            {
                return indexScalarDictionary.TryGetValue(0UL, out var scalar)
                    ? GaStorageScalar<T>.Create(scalar)
                    : GaStorageScalar<T>.ZeroScalar;
            }

            return grade switch
            {
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }
    }
}