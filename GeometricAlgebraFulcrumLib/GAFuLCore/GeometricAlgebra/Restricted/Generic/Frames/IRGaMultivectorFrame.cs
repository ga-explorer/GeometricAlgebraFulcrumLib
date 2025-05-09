using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaMultivectorFrame<T> :
    IReadOnlyList<RGaMultivector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}