using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames
{
    public interface IKVectorFrame<T> :
        IReadOnlyList<GaKVector<T>>,
        IGeometricAlgebraElement<T>
    {

    }
}