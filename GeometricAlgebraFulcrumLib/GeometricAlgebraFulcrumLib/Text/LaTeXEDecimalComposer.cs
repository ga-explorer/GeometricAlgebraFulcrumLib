using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXEDecimalComposer
        : LaTeXComposer<EDecimal>
    {
        public static LaTeXEDecimalComposer DefaultComposer { get; }
            = new LaTeXEDecimalComposer();

        
        private LaTeXEDecimalComposer()
            : base(ScalarAlgebraEDecimalProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            var angleText = GetScalarText(EDecimal.FromDouble(angle.Degrees));

            return $"{angleText}^{{\\circ}}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(EDecimal scalar)
        {
            return scalar.ToString();
        }
    }
}