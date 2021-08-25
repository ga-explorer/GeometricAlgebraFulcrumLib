using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Binary;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

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


        public IGaScalarsGridProcessor<T> ScalarsGridProcessor { get; }

        public IGaStorageKVector<T> MappedPseudoScalar { get; }


        internal GaOmComputedDiagonal([NotNull] IGaScalarsGridProcessor<T> arrayProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            VSpaceDimension = (uint) basisVectorsSignatures.Count;
            ScalarsGridProcessor = arrayProcessor;
            DiagonalScalars = basisVectorsSignatures;

            var id = (VSpaceDimension.ToGaSpaceDimension()) - 1;
            var scalar = ScalarsGridProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            MappedPseudoScalar = ScalarsGridProcessor.CreateStorageKVector(id, scalar);
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
            return ScalarsGridProcessor.CreateStorageVector(
                index, 
                DiagonalScalars[(int) index]
            );
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GaStorageBivector<T>.ZeroBivector;

            return ScalarsGridProcessor.CreateStorageBivector(
                index1, 
                index2, 
                ScalarsGridProcessor.Times(
                    DiagonalScalars[(int) index1], 
                    DiagonalScalars[(int) index2]
                )
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            var scalar = ScalarsGridProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarsGridProcessor.CreateStorageKVector(id, scalar);
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            var scalar = ScalarsGridProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarsGridProcessor.CreateStorageKVector(grade, index, scalar);
        }

        public IGaStorageVector<T> MapVector(IGaStorageVector<T> vector)
        {
            var storage = 
                ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in vector.IndexScalarList.GetKeyValueRecords())
                storage.SetTerm(
                    index, 
                    ScalarsGridProcessor.Times(scalar, DiagonalScalars[(int)index])
                );

            storage.RemoveZeroTerms();

            return storage.CreateStorageVector();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector)
        {
            var storage = 
                ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in bivector.IndexScalarList.GetKeyValueRecords())
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

            return storage.CreateStorageBivector();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector)
        {
            var storage = 
                ScalarsGridProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in kVector.IndexScalarList.GetKeyValueRecords())
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

            return storage.CreateStorageKVector(kVector.Grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv)
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

            return storage.CreateStorageSparseMultivector();
        }
    }
}