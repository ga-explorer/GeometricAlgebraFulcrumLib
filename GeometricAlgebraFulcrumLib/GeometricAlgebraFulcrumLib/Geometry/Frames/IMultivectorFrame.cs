using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public interface IMultivectorFrame<T> :
        IReadOnlyList<Multivector<T>>,
        IGeometricAlgebraElement<T>
    {

    }
}