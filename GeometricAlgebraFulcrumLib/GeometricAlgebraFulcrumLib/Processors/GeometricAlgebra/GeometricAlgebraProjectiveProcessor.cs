using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public class GeometricAlgebraProjectiveProcessor<T> :
        GeometricAlgebraOrthonormalProcessor<T>
    {
        internal GeometricAlgebraProjectiveProcessor(IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension) 
            : base(
                scalarProcessor, 
                GeometricAlgebraSignatureFactory.CreateProjective(vSpaceDimension)
            )
        {
        }
    }
}