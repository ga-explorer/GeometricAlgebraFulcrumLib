using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXEFloatComposer
        : LaTeXComposer<EFloat>
    {
        public static LaTeXEFloatComposer DefaultComposer { get; }
            = new LaTeXEFloatComposer();

        
        private LaTeXEFloatComposer()
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