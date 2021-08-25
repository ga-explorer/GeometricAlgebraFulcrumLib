using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Rotors
{
    public interface IGaRotor<T> : 
        IGaAutomorphism<T>
    {
        IGaStorageMultivector<T> Multivector { get; }

        IGaStorageMultivector<T> MultivectorReverse { get; }
    }
}