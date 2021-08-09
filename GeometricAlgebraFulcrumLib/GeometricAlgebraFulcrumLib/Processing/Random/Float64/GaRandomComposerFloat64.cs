using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Processing.Random.Float64
{
    public sealed class GaRandomComposerFloat64 : 
        GaRandomComposer<double>
    {
        public GaRandomComposerFloat64(uint vSpaceDimension, int seed) 
            : base(GaScalarProcessorFloat64.DefaultProcessor, vSpaceDimension, new System.Random(seed))
        {
        }

        public GaRandomComposerFloat64(IGaScalarProcessor<double> scalarProcessor, uint vSpaceDimension) : base(scalarProcessor, vSpaceDimension)
        {
        }
    }
}