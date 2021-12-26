using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public class GeometricAlgebraProjectiveProcessor<T> :
        GeometricAlgebraOrthonormalProcessor<T>
    {
        internal GeometricAlgebraProjectiveProcessor(IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension) 
            : base(
                scalarProcessor, 
                BasisBladeSet.CreateProjective(vSpaceDimension)
            )
        {
        }
    }
}