using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public sealed class GaFactoredVectorsLinearMap<T>
        : IGaOutermorphism<T>
    {
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

        public IReadOnlyList<IGaOutermorphism<T>> LinearMapsList { get; }


        public bool IsValid
            => true;

        public bool IsInvalid
            => false;


        public IGaOutermorphism<T> GetAdjoint()
        {
            throw new System.NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(int index)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<IGasVector<T>> GetMappedBasisVectors()
        {
            throw new System.NotImplementedException();
        }

        public IGasVector<T> MapBasisVector(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGasBivector<T> MapBasisBivector(int index1, int index2)
        {
            throw new System.NotImplementedException();
        }

        public IGasBivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            throw new System.NotImplementedException();
        }

        public IGasKVector<T> MapBasisBlade(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public IGasKVector<T> MapBasisBlade(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public IGasScalar<T> MapScalar(IGasScalar<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasKVector<T> MapTerm(IGasKVectorTerm<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasVector<T> MapVector(IGasVector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasBivector<T> MapBivector(IGasBivector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasKVector<T> MapKVector(IGasKVector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasMultivector<T> MapMultivector(IGasGradedMultivector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasMultivector<T> MapMultivector(IGasTermsMultivector<T> storage)
        {
            throw new System.NotImplementedException();
        }

        public IGasMultivector<T> MapMultivector(IGasMultivector<T> storage)
        {
            throw new System.NotImplementedException();
        }
    }
}