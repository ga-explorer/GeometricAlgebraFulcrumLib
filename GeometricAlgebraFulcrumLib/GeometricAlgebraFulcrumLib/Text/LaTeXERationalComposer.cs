using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXERationalComposer
        : LaTeXComposer<ERational>
    {
        public static LaTeXERationalComposer DefaultComposer { get; }
            = new LaTeXERationalComposer();

        
        private LaTeXERationalComposer()
            : base(ScalarAlgebraERationalProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
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