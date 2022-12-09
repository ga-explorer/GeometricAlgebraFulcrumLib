using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames
{
    public interface IVectorFrame<T> :
        IReadOnlyList<GaVector<T>>,
        IGeometricAlgebraElement<T>,
        IGeometricElement
    {

    }
}