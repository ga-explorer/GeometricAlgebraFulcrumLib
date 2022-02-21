using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public interface IVectorFrame<T> :
        IReadOnlyList<Vector<T>>,
        IGeometricAlgebraElement<T>,
        IGeometricElement
    {

    }
}