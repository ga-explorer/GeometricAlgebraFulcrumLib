using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXEFloatComposer
        : LaTeXComposer<EFloat>
    {
        public static LaTeXEFloatComposer DefaultComposer { get; }
            = new LaTeXEFloatComposer();

        
        private LaTeXEFloatComposer()
            : base(ScalarAlgebraEFloatProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
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