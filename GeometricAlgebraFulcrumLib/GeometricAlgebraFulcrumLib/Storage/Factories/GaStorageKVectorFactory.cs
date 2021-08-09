using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageKVectorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerKVector<T> CreateStorageComposerKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade)
        {
            return new GaStorageComposerKVector<T>(
                scalarProcessor,
                grade
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerKVector<T> CreateStorageComposerKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IReadOnlyDictionary<ulong, T> indexScalarsDictionary) 
        {
            return new GaStorageComposerKVector<T>(
                scalarProcessor,
                grade,
                indexScalarsDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerKVector<T> CreateStorageComposerKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs) 
        {
            return new GaStorageComposerKVector<T>(
                scalarProcessor,
                grade,
                indexScalarPairs
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerKVector<T> CreateStorageComposerKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples)
        {
            return new GaStorageComposerKVector<T>(
                scalarProcessor,
                grade,
                indexScalarTuples
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CopyToStorageKVector<T>(this IReadOnlyDictionary<ulong, T> indexScalarDictionary, uint grade)
        {
            var evenDictionary = indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(evenDictionary),
                1 => GaStorageVector<T>.Create(evenDictionary),
                2 => GaStorageBivector<T>.Create(evenDictionary),
                _ => GaStorageKVector<T>.Create(grade, evenDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStoragePseudoScalar<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var index = vSpaceDimension.PseudoScalarIndex();
            var scalar = scalarProcessor.OneScalar;

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
            var index = vSpaceDimension.PseudoScalarIndex();
            var scalar = 
                vSpaceDimension.GradeHasNegativeReverse()
                    ? scalarProcessor.MinusOneScalar 
                    : scalarProcessor.OneScalar;

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
            var (grade, index) = id.BasisBladeGradeIndex();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalarProcessor.OneScalar),
                1 => GaStorageVector<T>.Create(index, scalarProcessor.OneScalar),
                2 => GaStorageBivector<T>.Create(index, scalarProcessor.OneScalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalarProcessor.OneScalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageBasisBlade<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, ulong index)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalarProcessor.OneScalar),
                1 => GaStorageVector<T>.Create(index, scalarProcessor.OneScalar),
                2 => GaStorageBivector<T>.Create(index, scalarProcessor.OneScalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalarProcessor.OneScalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndex();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaBasisBlade basisBlade, T scalar)
        {
            var (grade, index) = basisBlade.GetGradeIndex();

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
            id.BasisBladeGradeIndex(out var grade, out var index);

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
            id.BasisBladeGradeIndex(out var grade, out var index);

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
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, KeyValuePair<ulong, T> idScalarPair)
        {
            var (id, scalar) = idScalarPair;

            id.BasisBladeGradeIndex(out var grade, out var index);

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, KeyValuePair<ulong, T> indexScalarPair)
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
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalarsList),
                1 => GaStorageVector<T>.Create(scalarsList),
                2 => GaStorageBivector<T>.Create(scalarsList),
                _ => GaStorageKVector<T>.Create(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<Tuple<ulong, T>> termsList)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.Item1,
                pair => pair.Item2
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaTerm<T>> termsList)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> SumToStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            return scalarProcessor
                .CreateStorageComposerKVector(grade, termsList)
                .RemoveZeroTerms()
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> SumToStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<Tuple<ulong, T>> termsList)
        {
            return scalarProcessor
                .CreateStorageComposerKVector(grade, termsList)
                .RemoveZeroTerms()
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> SumToStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IEnumerable<GaTerm<T>> termsList)
        {
            return scalarProcessor
                .CreateStorageComposerKVector(grade)
                .AddTerms(termsList)
                .RemoveZeroTerms()
                .GetKVector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, Dictionary<ulong, T> indexScalarDictionary)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<T> scalarsList, uint grade)
        {
            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalarsList),
                1 => GaStorageVector<T>.Create(scalarsList),
                2 => GaStorageBivector<T>.Create(scalarsList),
                _ => GaStorageKVector<T>.Create(grade, scalarsList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList, uint grade)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<Tuple<ulong, T>> termsList, uint grade)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.Item1,
                pair => pair.Item2
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> CreateStorageKVector<T>(this IEnumerable<GaTerm<T>> termsList, uint grade)
        {
            var indexScalarDictionary = termsList.ToDictionary(
                pair => pair.BasisBlade.Index,
                pair => pair.Scalar
            );

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(indexScalarDictionary),
                1 => GaStorageVector<T>.Create(indexScalarDictionary),
                2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
            };
        }
    }
}