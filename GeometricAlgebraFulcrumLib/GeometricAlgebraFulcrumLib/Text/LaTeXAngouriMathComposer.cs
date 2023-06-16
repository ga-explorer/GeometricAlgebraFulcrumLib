using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class LaTeXAngouriMathComposer
        : MathBase.Text.LaTeXComposer<Entity>
    {
        public static LaTeXAngouriMathComposer DefaultComposer { get; }
            = new LaTeXAngouriMathComposer();
        

        private LaTeXAngouriMathComposer()
            : base(ScalarAlgebraAngouriMathProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
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