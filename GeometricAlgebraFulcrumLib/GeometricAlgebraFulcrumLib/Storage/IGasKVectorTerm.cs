using GeometricAlgebraFulcrumLib.Algebra.Basis;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGasKVectorTerm<T>
        : IGasKVector<T>, IGasTermsMultivector<T>
    {
        ulong Id { get; }

        ulong Index { get; }

        IGaBasisBlade BasisBlade { get; }

        T Scalar { get; set; }
    }
}