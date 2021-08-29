using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class Float64LaTeXComposer
        : LaTeXComposer<double>
    {
        public static Float64LaTeXComposer DefaultComposer { get; }
            = new Float64LaTeXComposer();


        public int ScalarDecimals { get; set; }
            = 7;


        private Float64LaTeXComposer()
            : base(Float64ScalarProcessor.DefaultProcessor)
        {
        }


        public override string GetScalarText(double scalar)
        {
            var valueText = scalar.ToString("G");

            if (!valueText.Contains("E")) 
                return Math.Round(scalar, ScalarDecimals).ToString("G");

            var ePosition = valueText.IndexOf('E');

            var magnitude = double.Parse(valueText.Substring(0, ePosition));
            var magnitudeText = Math.Round(magnitude, ScalarDecimals).ToString("G");
            var exponentText = valueText.Substring(ePosition + 1);

            return $@"{magnitudeText} \times 10^{{{exponentText}}}";
        }
    }
}