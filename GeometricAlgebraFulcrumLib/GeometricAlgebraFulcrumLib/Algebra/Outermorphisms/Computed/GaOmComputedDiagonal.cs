using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaOmComputedDiagonal<T> : 
        IGaAutomorphism<T>
    {
        public IReadOnlyList<T> DiagonalScalars { get; }

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGaStorageKVector<T> MappedPseudoScalar { get; }


        internal GaOmComputedDiagonal([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            VSpaceDimension = (uint) basisVectorsSignatures.Count;
            ScalarProcessor = scalarProcessor;
            DiagonalScalars = basisVectorsSignatures;

            var id = (1UL << (int) VSpaceDimension) - 1;
            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            MappedPseudoScalar = ScalarProcessor.CreateStorageKVector(id, scalar);
        }


        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            return VSpaceDimension
                .GetRange()
                .Select(index => MapBasisVector(index))
                .ToArray();
        }
        

        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            return ScalarProcessor.CreateStorageVector(
                index, 
                DiagonalScalars[(int) index]
            );
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaStorageBivector<T>.ZeroBivector;

            return ScalarProcessor.CreateStorageBivector(
                index1, 
                index2, 
                ScalarProcessor.Times(
                    DiagonalScalars[(int) index1], 
                    DiagonalScalars[(int) index2]
                )
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarProcessor.CreateStorageKVector(id, scalar);
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarProcessor.CreateStorageKVector(grade, index, scalar);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> vector)
        {
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.IndexScalarDictionary)
                storage.SetTerm(
                    index, 
                    ScalarProcessor.Times(scalar, DiagonalScalars[(int)index])
                );

            storage.RemoveZeroTerms();

            return storage.GetVector();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector)
        {
            var storage = new GaStorageComposerBivector<T>(ScalarProcessor);

            foreach (var (index, scalar) in bivector.IndexScalarDictionary)
            {
                var id = GaBasisUtils.BasisBladeId(2, index);

                var newScalar = ScalarProcessor.Times(
                    scalar,
                    ScalarProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.GetBivector();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector)
        {
            var storage = new GaStorageComposerKVector<T>(ScalarProcessor, kVector.Grade);

            foreach (var (index, scalar) in kVector.IndexScalarDictionary)
            {
                var id = GaBasisUtils.BasisBladeId(kVector.Grade, index);

                var newScalar = ScalarProcessor.Times(
                    scalar,
                    ScalarProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.GetKVector();
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
        {
            var storage = new GaStorageComposerMultivectorGraded<T>(ScalarProcessor);

            foreach (var (id, scalar) in mv.GetIdScalarPairs())
            {
                var newScalar = ScalarProcessor.Times(
                    scalar,
                    ScalarProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(id, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.GetMultivector();
        }
    }
}