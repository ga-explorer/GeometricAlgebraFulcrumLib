using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public sealed class GaProjector<T>
        : IGaProjector<T>
    {
        public IGaSpace Space => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaKVectorStorage<T> UnitBladeStorage { get; }

        public ILaProcessor<T> ScalarsGridProcessor 
            => Processor;

        public IGaKVectorStorage<T> MappedPseudoScalar { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        public IGaOutermorphism<T> GetAdjoint()
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(int index1, int index2)
        {
            throw new System.NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapTerm(IGaKVectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorSparseStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage)
        {
            return Processor.ELcp(
                Processor.ELcp(storage, UnitBladeStorage), 
                UnitBladeStorage
            );
        }
    }
}