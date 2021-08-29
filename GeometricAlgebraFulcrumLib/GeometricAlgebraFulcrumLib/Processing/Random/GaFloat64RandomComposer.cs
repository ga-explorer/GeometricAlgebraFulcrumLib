using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Random
{
    public sealed class GaFloat64RandomComposer : 
        GaRandomComposer<double>
    {
        public GaFloat64RandomComposer(uint vSpaceDimension, int seed) 
            : base(Float64ScalarProcessor.DefaultProcessor, vSpaceDimension, new System.Random(seed))
        {
        }

        public GaFloat64RandomComposer(IScalarProcessor<double> scalarProcessor, uint vSpaceDimension) : base(scalarProcessor, vSpaceDimension)
        {
        }
    }
}