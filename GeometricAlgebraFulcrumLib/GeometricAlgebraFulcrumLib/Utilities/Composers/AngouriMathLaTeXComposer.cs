using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class AngouriMathLaTeXComposer
        : LaTeXComposer<Entity>
    {
        public static AngouriMathLaTeXComposer DefaultComposer { get; }
            = new AngouriMathLaTeXComposer();
        

        private AngouriMathLaTeXComposer()
            : base(AngouriMathScalarProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Entity scalar)
        {
            return scalar.Latexise();
        }
    }
}