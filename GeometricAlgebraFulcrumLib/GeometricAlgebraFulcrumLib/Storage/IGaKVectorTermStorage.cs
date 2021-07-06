using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGaKVectorTermStorage<TScalar>
        : IGaKVectorStorage<TScalar>, IGaMultivectorTermsStorage<TScalar>
    {
        ulong Id { get; }

        ulong Index { get; }

        IGaBasisBlade BasisBlade { get; }

        TScalar Scalar { get; set; }

        IGaMultivectorStorage<TScalar> Add(IGaKVectorTermStorage<TScalar> mv2);

        IGaMultivectorStorage<TScalar> Subtract(IGaKVectorTermStorage<TScalar> mv2);

        IGaKVectorTermStorage<TScalar> Op(IGaKVectorTermStorage<TScalar> mv2);
    }
}