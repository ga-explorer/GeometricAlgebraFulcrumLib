using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class LaTeXComposerEDecimal
        : LaTeXComposer<EDecimal>
    {
        public static LaTeXComposerEDecimal DefaultComposer { get; }
            = new LaTeXComposerEDecimal();

        
        private LaTeXComposerEDecimal()
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