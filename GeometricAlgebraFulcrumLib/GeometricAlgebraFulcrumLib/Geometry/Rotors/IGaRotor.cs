using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Rotors
{
    public interface IGaRotor<T> : 
        IGaAutomorphism<T>
    {
        IGaMultivectorStorage<T> Multivector { get; }

        IGaMultivectorStorage<T> MultivectorReverse { get; }
    }
}