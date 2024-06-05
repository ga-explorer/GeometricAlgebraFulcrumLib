using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic
{
    public interface ILinearAlgebraElement<T> :
        IScalarAlgebraElement<T>,
        ILinearAlgebraElement
    {

    }
}
