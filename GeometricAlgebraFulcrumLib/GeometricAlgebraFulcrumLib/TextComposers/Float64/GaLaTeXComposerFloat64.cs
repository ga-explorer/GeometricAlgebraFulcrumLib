using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.TextComposers.Float64
{
    public sealed class GaLaTeXComposerFloat64
        : GaLaTeXComposer<double>
    {
        public static GaLaTeXComposerFloat64 DefaultComposer { get; }
            = new GaLaTeXComposerFloat64();


        public int ScalarDecimals { get; set; }
            = 7;

        public GaLaTeXComposerFloat64()
            : base(GaScalarProcessorFloat64.DefaultProcessor)
        {
        }

        public override string GetScalarText(double scalar)
        {
            var valueText = scalar.ToString("G");

            if (valueText.Contains("E"))
            {
                var ePosition = valueText.IndexOf('E');

                var magnitude = double.Parse(valueText.Substring(0, ePosition));
                var magnitudeText = Math.Round(magnitude, ScalarDecimals).ToString("G");
                var exponentText = valueText.Substring(ePosition + 1);

                return $@"{magnitudeText} \times 10^{{{exponentText}}}";
            }

            return Math.Round(scalar, ScalarDecimals).ToString("G");
        }
    }
}