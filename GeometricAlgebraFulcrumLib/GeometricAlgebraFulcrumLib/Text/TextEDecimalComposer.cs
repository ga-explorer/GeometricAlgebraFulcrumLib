using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextEDecimalComposer
        : TextComposer<EDecimal>
    {
        public static TextEDecimalComposer DefaultComposer { get; }
            = new TextEDecimalComposer();


        private TextEDecimalComposer() 
            : base(ScalarAlgebraEDecimalProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{GetScalarText(EDecimal.FromDouble(angle.Degrees))} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(EDecimal scalar)
        {
            return scalar.ToString();
        }
    }
}