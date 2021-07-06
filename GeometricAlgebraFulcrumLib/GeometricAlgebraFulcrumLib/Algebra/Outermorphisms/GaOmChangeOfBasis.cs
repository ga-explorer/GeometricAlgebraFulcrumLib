using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    /// <summary>
    /// This class holds a Change-of-Basis Outermorphism stored as a pair of
    /// outermorphisms which are the inverse to each other.
    /// </summary>
    public sealed class GaOmChangeOfBasis<T> 
        : IGaOutermorphism<T>
    {
        public IGaOutermorphism<T> ForwardOutermorphism { get; }

        public IGaOutermorphism<T> BackwardOutermorphism { get; }

        public int DomainVSpaceDimension 
            => ForwardOutermorphism.DomainVSpaceDimension;

        public ulong DomainGaSpaceDimension 
            => ForwardOutermorphism.DomainGaSpaceDimension;

        public IGaMultivectorProcessor<T> MultivectorProcessor 
            => ForwardOutermorphism.MultivectorProcessor;

        public IGaScalarProcessor<T> ScalarProcessor 
            => ForwardOutermorphism.ScalarProcessor;

        public IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors()
        {
            return ForwardOutermorphism.GetMappedBasisVectors();
        }

        public T GetDeterminant()
        {
            return ForwardOutermorphism.GetDeterminant();
        }

        public IGaVectorStorage<T> MapBasisVector(int index)
        {
            return ForwardOutermorphism.MapBasisVector(index);
        }

        public IGaVectorStorage<T> MapBasisVector(ulong index)
        {
            return ForwardOutermorphism.MapBasisVector(index);
        }

        public IGaKVectorStorage<T> MapBasisBlade(ulong id)
        {
            return ForwardOutermorphism.MapBasisBlade(id);
        }

        public IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index)
        {
            return ForwardOutermorphism.MapBasisBlade(grade, index);
        }

        public IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector)
        {
            return ForwardOutermorphism.MapVector(vector);
        }

        public IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector)
        {
            return ForwardOutermorphism.MapKVector(kVector);
        }

        public IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv)
        {
            return ForwardOutermorphism.MapMultivector(mv);
        }


        internal GaOmChangeOfBasis(IGaOutermorphism<T> derivedToBaseCba, IGaOutermorphism<T> baseToDerivedCba)
        {
            ForwardOutermorphism = derivedToBaseCba;
            BackwardOutermorphism = baseToDerivedCba;
        }
    }
}
