using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames
{
    public interface IMultivectorFrame<T> :
        IReadOnlyList<GaMultivector<T>>,
        IGeometricAlgebraElement<T>
    {

    }
}