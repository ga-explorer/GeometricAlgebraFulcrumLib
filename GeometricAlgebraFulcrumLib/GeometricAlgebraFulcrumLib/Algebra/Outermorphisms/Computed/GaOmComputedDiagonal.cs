using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

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

        public IGaSpace Space { get; }
        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();


        public ILaProcessor<T> ScalarsGridProcessor { get; }

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }


        internal GaOmComputedDiagonal([NotNull] ILaProcessor<T> arrayProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            VSpaceDimension = (uint) basisVectorsSignatures.Count;
            ScalarsGridProcessor = arrayProcessor;
            DiagonalScalars = basisVectorsSignatures;

            var id = (VSpaceDimension.ToGaSpaceDimension()) - 1;
            var scalar = ScalarsGridProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            MappedPseudoScalar = ScalarsGridProcessor.CreateKVectorStorage(id, scalar);
        }


        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            return VSpaceDimension
                .GetRange()
                .Select(index => MapBasisVector(index))
                .ToArray();
        }
        

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return ScalarsGridProcessor.CreateGaVectorStorage(
                index, 
                DiagonalScalars[(int) index]
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaBivectorStorage<T>.ZeroBivector;

            return ScalarsGridProcessor.CreateBivectorStorage(
                index1, 
                index2, 
                ScalarsGridProcessor.Times(
                    DiagonalScalars[(int) index1], 
                    DiagonalScalars[(int) index2]
                )
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            var scalar = ScalarsGridProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarsGridProcessor.CreateKVectorStorage(id, scalar);
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            var scalar = ScalarsGridProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarsGridProcessor.CreateKVectorStorage(grade, index, scalar);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            var storage = 
                ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in vector.IndexScalarList.GetIndexScalarRecords())
                storage.SetTerm(
                    index, 
                    ScalarsGridProcessor.Times(scalar, DiagonalScalars[(int)index])
                );

            storage.RemoveZeroTerms();

            return storage.CreateGaVectorStorage();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> bivector)
        {
            var storage = 
                ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in bivector.IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisBivectorIndexToId();

                var newScalar = ScalarsGridProcessor.Times(
                    scalar,
                    ScalarsGridProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateGaBivectorStorage();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            var storage = 
                ScalarsGridProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in kVector.IndexScalarList.GetIndexScalarRecords())
            {
                var id = index.BasisBladeIndexToId(kVector.Grade);

                var newScalar = ScalarsGridProcessor.Times(
                    scalar,
                    ScalarsGridProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(index, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateGaKVectorStorage(kVector.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            var storage = 
                ScalarsGridProcessor.CreateStorageGradedMultivectorComposer();

            foreach (var (id, scalar) in mv.GetIdScalarRecords())
            {
                var newScalar = ScalarsGridProcessor.Times(
                    scalar,
                    ScalarsGridProcessor.Times(
                        DiagonalScalars.PickItemsUsingPattern(id)
                    )
                );

                storage.SetTerm(id, newScalar);
            }

            storage.RemoveZeroTerms();

            return storage.CreateGaMultivectorSparseStorage();
        }
    }
}