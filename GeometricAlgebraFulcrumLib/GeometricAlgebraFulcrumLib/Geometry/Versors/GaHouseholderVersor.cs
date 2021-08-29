using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public sealed class GaHouseholderVersor<T> 
        : IGaVersor<T>
    {
        public static GaHouseholderVersor<T> Create(IGaProcessor<T> processor, IGaVectorStorage<T> unitVectorStorage)
        {
            return new GaHouseholderVersor<T>(processor, unitVectorStorage);
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public ILaProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

        public IGaProcessor<T> Processor { get; }

        public IGaVectorStorage<T> UnitVectorStorage { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        private GaHouseholderVersor([NotNull] IGaProcessor<T> processor, [NotNull] IGaVectorStorage<T> unitVectorStorage)
        {
            Processor = processor;
            UnitVectorStorage = unitVectorStorage;
        }
        

        public IGaOutermorphism<T> GetAdjoint()
        {
            return this;
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return MapVector(
                Processor.CreateGaVectorStorage(index)
            );
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return MapVector(
                Processor.CreateGaVectorStorage(index)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            return MapBivector(
                Processor.CreateBivectorStorage(index1, index2)
            );
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return MapBivector(
                Processor.CreateBivectorStorage(index1, index2)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                Processor.CreateKVectorStorage(id)
            );
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapKVector(
                Processor.CreateKVectorStorage(grade, index)
            );
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            return storage;
        }
        
        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.Negative(storage),
                UnitVectorStorage
            ).GetVectorPart();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                storage,
                UnitVectorStorage
            ).GetBivectorPart();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            ).GetKVectorPart(storage.Grade);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorSparseStorage<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return Processor.EGp(
                UnitVectorStorage, 
                Processor.GradeInvolution(storage),
                UnitVectorStorage
            );
        }
    }
}