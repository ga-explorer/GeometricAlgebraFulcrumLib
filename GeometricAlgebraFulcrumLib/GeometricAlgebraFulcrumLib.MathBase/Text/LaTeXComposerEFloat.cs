using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class LaTeXComposerEFloat
        : LaTeXComposer<EFloat>
    {
        public static LaTeXComposerEFloat DefaultComposer { get; }
            = new LaTeXComposerEFloat();

        
        private LaTeXComposerEFloat()
            : base(ScalarProcessorEFloat.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
        {
            var angleText = GetScalarText(angle.Degrees);

            return $"{angleText}^{{\\circ}}";
        }

        public override string GetScalarText(EFloat scalar)
        {
            return scalar.ToString();
        }
    }
}