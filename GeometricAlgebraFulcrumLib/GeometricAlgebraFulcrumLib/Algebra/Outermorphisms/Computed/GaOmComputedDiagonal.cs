﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
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

        public IGasKVector<T> MappedPseudoScalar { get; }


        internal GaOmComputedDiagonal([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] IReadOnlyList<T> basisVectorsSignatures)
        {
            VSpaceDimension = (uint) basisVectorsSignatures.Count;
            ScalarProcessor = scalarProcessor;
            DiagonalScalars = basisVectorsSignatures;

            var id = (1UL << (int) VSpaceDimension) - 1;
            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            MappedPseudoScalar = ScalarProcessor.CreateKVector(id, scalar);
        }


        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            return VSpaceDimension
                .GetRange()
                .Select(index => MapBasisVector(index))
                .ToArray();
        }
        

        public IGasVector<T> MapBasisVector(ulong index)
        {
            return ScalarProcessor.CreateVector(
                index, 
                DiagonalScalars[(int) index]
            );
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return ScalarProcessor.CreateZeroBivector();

            return ScalarProcessor.CreateBivector(
                index1, 
                index2, 
                ScalarProcessor.Times(
                    DiagonalScalars[(int) index1], 
                    DiagonalScalars[(int) index2]
                )
            );
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarProcessor.CreateKVector(id, scalar);
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = GaBasisUtils.BasisBladeId(grade, index);

            var scalar = ScalarProcessor.Times(
                DiagonalScalars.PickItemsUsingPattern(id)
            );

            return ScalarProcessor.CreateKVector(grade, index, scalar);
        }

        public IGasVector<T> MapVector(IGasVector<T> vector)
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

        public IGasBivector<T> MapBivector(IGasBivector<T> bivector)
        {
            var storage = new GaBivectorStorageComposer<T>(ScalarProcessor);

            foreach (var (index, scalar) in bivector.GetIndexScalarPairs())
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

            return storage.GetBivectorStorage();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> kVector)
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

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> mv)
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

            return storage.GetCompactMultivector();
        }
    }
}