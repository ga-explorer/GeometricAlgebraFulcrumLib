using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class LaTeXFloat64Composer
        : LaTeXComposer<double>
    {
        public static LaTeXFloat64Composer DefaultComposer { get; }
            = new LaTeXFloat64Composer();


        public int ScalarDecimals { get; set; }
            = 7;


        private LaTeXFloat64Composer()
            : base(ScalarAlgebraFloat64Processor.DefaultProcessor)
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