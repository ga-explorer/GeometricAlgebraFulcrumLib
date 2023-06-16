using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXEDecimalComposer
        : LaTeXComposer<EDecimal>
    {
        public static LaTeXEDecimalComposer DefaultComposer { get; }
            = new LaTeXEDecimalComposer();

        
        private LaTeXEDecimalComposer()
            : base(ScalarProcessorEDecimal.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
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