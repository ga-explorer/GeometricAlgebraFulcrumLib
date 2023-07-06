using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class LaTeXComposerERational
        : LaTeXComposer<ERational>
    {
        public static LaTeXComposerERational DefaultComposer { get; }
            = new LaTeXComposerERational();

        
        private LaTeXComposerERational()
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