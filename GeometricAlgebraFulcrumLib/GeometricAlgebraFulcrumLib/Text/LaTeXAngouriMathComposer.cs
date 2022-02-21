using System.Runtime.CompilerServices;
using AngouriMath;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXAngouriMathComposer
        : LaTeXComposer<Entity>
    {
        public static LaTeXAngouriMathComposer DefaultComposer { get; }
            = new LaTeXAngouriMathComposer();
        

        private LaTeXAngouriMathComposer()
            : base(ScalarAlgebraAngouriMathProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{angle.Degrees}^{{\\circ}}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Entity scalar)
        {
            return scalar.Latexise();
        }
    }
}