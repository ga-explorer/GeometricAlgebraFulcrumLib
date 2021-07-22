using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public sealed class GaHouseholderVersor<T> 
        : IGaVersor<T>
    {
        public static GaHouseholderVersor<T> Create(IGaProcessor<T> processor, IGasVector<T> unitVectorStorage)
        {
            return new GaHouseholderVersor<T>(processor, unitVectorStorage);
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ulong MaxBasisBladeId { get; }

        public uint GradesCount { get; }

        public IEnumerable<uint> Grades { get; }

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public IGasKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGasVector<T> UnitVectorStorage { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaHouseholderVersor([NotNull] IGaProcessor<T> processor, [NotNull] IGasVector<T> unitVectorStorage)
        {
            Processor = processor;
            UnitVectorStorage = unitVectorStorage;
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return this;
        }

        public IGasVector<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateBasisVector(index)
            );
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateBasisVector(index)
            );
        }

        public IGasBivector<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateBasisBivector(index1, index2)
            );
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateBasisBivector(index1, index2)
            );
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            return MapTerm(
                Processor.CreateBasisBlade(id)
            );
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapTerm(
                GaStorageFactory.CreateBasisBlade(Processor, grade, index)
            );
        }

        public IGasScalar<T> MapScalar(IGasScalar<T> storage)
        {
            return storage;
        }

        public IGasKVector<T> MapTerm(IGasKVectorTerm<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage)
                .GetKVectorPart(storage.Grade);
        }

        public IGasVector<T> MapVector(IGasVector<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetNegativeVectorPart())
                .EGp(UnitVectorStorage)
                .GetVectorPart();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage)
                .EGp(UnitVectorStorage)
                .GetBivectorPart();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage)
                .GetKVectorPart(storage.Grade);
        }

        public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage);
        }

        public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage);
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> storage)
        {
            return UnitVectorStorage
                .EGp(storage.GetGradeInvolution())
                .EGp(UnitVectorStorage);
        }
    }
}