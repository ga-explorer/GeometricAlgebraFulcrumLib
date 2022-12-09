using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Projectors
{
    public interface IProjector<T> : 
        IGaOutermorphism<T>
    {
        GaKVector<T> Blade { get; }
    }
}