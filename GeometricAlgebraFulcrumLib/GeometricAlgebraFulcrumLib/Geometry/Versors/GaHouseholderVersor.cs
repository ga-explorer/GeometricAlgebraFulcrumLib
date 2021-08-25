using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public sealed class GaHouseholderVersor<T> 
        : IGaVersor<T>
    {
        public static GaHouseholderVersor<T> Create(IGaProcessor<T> processor, IGaStorageVector<T> unitVectorStorage)
        {
            return new GaHouseholderVersor<T>(processor, unitVectorStorage);
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaScalarsGridProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaStorageKVector<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaStorageVector<T> UnitVectorStorage { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaHouseholderVersor([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageVector<T> unitVectorStorage)
        {
            Processor = processor;
            UnitVectorStorage = unitVectorStorage;
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return this;
        }

        public IGaStorageVector<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateStorageBasisVector(index)
            );
        }

        public IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGaStorageVector<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateStorageBasisVector(index)
            );
        }

        public IGaStorageBivector<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateStorageBasisBivector(index1, index2)
            );
        }

        public IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateStorageBasisBivector(index1, index2)
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                Processor.CreateStorageBasisBlade(id)
            );
        }

        public IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapKVector(
                Processor.CreateStorageBasisBlade(grade, index)
            );
        }

        public IGaStorageScalar<T> MapScalar(IGaStorageScalar<T> storage)
        {
            return storage;
        }
        
        public IGaStorageVector<T> MapVector(IGaStorageVector<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.Negative(storage),
                UnitVectorStorage
            ).GetVectorPart();
        }

        public IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                storage,
                UnitVectorStorage
            ).GetBivectorPart();
        }

        public IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            ).GetKVectorPart(storage.Grade);
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorGraded<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivectorSparse<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }

        public IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }
    }
}