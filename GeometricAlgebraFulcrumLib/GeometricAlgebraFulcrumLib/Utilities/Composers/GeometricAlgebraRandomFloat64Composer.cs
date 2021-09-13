using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class GeometricAlgebraRandomFloat64Composer : 
        GeometricAlgebraRandomComposer<double>
    {
        public GeometricAlgebraRandomFloat64Composer(uint vSpaceDimension, int seed) 
            : base(ScalarAlgebraFloat64Processor.DefaultProcessor, vSpaceDimension, new System.Random(seed))
        {
        }

        public GeometricAlgebraRandomFloat64Composer(IScalarAlgebraProcessor<double> scalarProcessor, uint vSpaceDimension) : base(scalarProcessor, vSpaceDimension)
        {
        }
    }
}