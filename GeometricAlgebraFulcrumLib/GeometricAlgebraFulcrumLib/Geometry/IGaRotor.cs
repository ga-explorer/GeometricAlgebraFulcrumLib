using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaRotor<T> : 
        IGaAutomorphism<T>
    {
        IGasMultivector<T> Rotor { get; }

        IGasMultivector<T> RotorReverse { get; }
    }
}