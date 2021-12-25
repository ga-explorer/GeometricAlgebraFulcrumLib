using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextFloat64Composer
        : TextComposerBase<double>
    {
        public static TextFloat64Composer DefaultComposer { get; }
            = new TextFloat64Composer();


        public int RoundingDecimals { get; set; }
            = 15;


        private TextFloat64Composer() 
            : base(ScalarAlgebraFloat64Processor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{GetScalarText(angle.Degrees)} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(double scalar)
        {
            return Math.Round(scalar, RoundingDecimals).ToString("G");
        }
    }
}