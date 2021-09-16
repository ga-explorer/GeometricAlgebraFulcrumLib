using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
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
        public override string GetScalarText(Entity scalar)
        {
            return scalar.Latexise();
        }
    }
}