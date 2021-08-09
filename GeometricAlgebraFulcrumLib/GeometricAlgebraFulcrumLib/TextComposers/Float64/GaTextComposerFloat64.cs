using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.TextComposers.Float64
{
    public sealed class GaTextComposerFloat64
        : GaTextComposer<double>
    {
        public int RoundingDecimals { get; set; }
            = 15;

        public static GaTextComposerFloat64 DefaultComposer { get; }
            = new GaTextComposerFloat64();


        public GaTextComposerFloat64() 
            : base(GaScalarProcessorFloat64.DefaultProcessor)
        {
        }


        public override string GetScalarText(double scalar)
        {
            return Math.Round(scalar, RoundingDecimals).ToString("G");
        }
    }
}