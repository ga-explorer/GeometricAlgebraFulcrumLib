using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed
{
    /// <summary>
    /// This class represents an outermorphism defined by a square diagonal vector mapping matrix.
    /// Only the scalars on the diagonal of the vector mapping matrix are stored, all other basis
    /// mappings are computed as needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaOmComputedDiagonal<T> : 
        IGaOutermorphism<T>
    {
        public IReadOnlyList<T> DiagonalScalars { get; }

        public int DomainVSpaceDimension 
            => DiagonalScalars.Count;

        public ulong DomainGaSpaceDimension 
            => DiagonalScalars.Count.ToGaSpaceDimension();

        public IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        public IGaScalarProcessor<T> ScalarProcessor 
            => MultivectorProcessor.ScalarProcessor;


        private GaOmComputedDiagonal([NotNull] IGaMultivectorProcessor<T> multivectorProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            MultivectorProcessor = multivectorProcessor;
            DiagonalScalars = basisVectorsSignatures;
        }


        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            return Enumerable
                .Range(0, DomainVSpaceDimension)
                .Select(MapBasisVector)
                .ToArray();
        }

        public T GetDeterminant()
        {
            var mappedPseudoScalar =
                GaKVectorTermStorage<T>.Create(
                    ScalarProcessor, 
                    (1UL << DomainVSpaceDimension) - 1,
                    ScalarProcessor.Times(DiagonalScalars)
                );

            return MultivectorProcessor.Sp(
                mappedPseudoScalar, 
                MultivectorProcessor.PseudoScalarInverse
            );
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            storage.SetTerm((ulong)index, DiagonalScalars[index]);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            storage.SetTerm(index, DiagonalScalars[(int)index]);

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            id.BasisBladeGradeIndex(out var grade, out var index);

            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            storage.SetTerm(index, scalar);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, grade);

            storage.SetTerm(index, scalar);

            storage.RemoveZeroTerms();

            return storage.GetKVectorStorage();
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, 1);

            foreach (var (index, scalar) in vector.GetIndexScalarPairs())
                storage.SetTerm(
                    index, 
                    ScalarProcessor.Times(scalar, DiagonalScalars[(int)index])
                );

            storage.RemoveZeroTerms();

            return storage.GetVectorStorage();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            var storage = new GaKVectorStorageComposer<T>(ScalarProcessor, kVector.Grade);

            foreach (var (index, scalar) in kVector.GetIndexScalarPairs())
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

            return storage.GetKVectorStorage();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            var storage = new GaMultivectorGradedStorageComposer<T>(ScalarProcessor);

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

            return storage.GetCompactStorage();
        }
    }
}