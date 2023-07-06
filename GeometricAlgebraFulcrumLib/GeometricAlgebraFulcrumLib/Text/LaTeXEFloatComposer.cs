using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXEFloatComposer
        : LaTeXComposer<EFloat>
    {
        public static LaTeXEFloatComposer DefaultComposer { get; }
            = new LaTeXEFloatComposer();

        
        private LaTeXEFloatComposer()
            : base(ScalarProcessorOfEFloat.DefaultProcessor)
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