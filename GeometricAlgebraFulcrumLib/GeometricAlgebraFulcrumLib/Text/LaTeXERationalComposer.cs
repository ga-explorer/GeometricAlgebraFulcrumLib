using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXERationalComposer
        : LaTeXComposer<ERational>
    {
        public static LaTeXERationalComposer DefaultComposer { get; }
            = new LaTeXERationalComposer();

        
        private LaTeXERationalComposer()
            : base(ScalarProcessorOfERational.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
        {
            var angleText = GetScalarText(angle.Degrees);

            return $"{angleText}^{{\\circ}}";
        }

        public override string GetScalarText(ERational scalar)
        {
            return scalar.ToString();
        }
    }
}