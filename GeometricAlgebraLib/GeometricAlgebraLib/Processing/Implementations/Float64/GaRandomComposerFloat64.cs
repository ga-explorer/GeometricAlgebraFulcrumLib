using GeometricAlgebraLib.Processing.Random;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Processing.Implementations.Float64
{
    public sealed class GaRandomComposerFloat64 : 
        GaRandomComposer<double>
    {
        public GaRandomComposerFloat64(int vSpaceDimension, int seed) 
            : base(GaScalarProcessorFloat64.DefaultProcessor, vSpaceDimension, new System.Random(seed))
        {
        }

        public GaRandomComposerFloat64(IGaScalarProcessor<double> scalarProcessor, int vSpaceDimension) : base(scalarProcessor, vSpaceDimension)
        {
        }
    }
}