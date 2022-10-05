using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public interface IVectorFrame<T> :
        IReadOnlyList<GaVector<T>>,
        IGeometricAlgebraElement<T>,
        IGeometricElement
    {

    }
}