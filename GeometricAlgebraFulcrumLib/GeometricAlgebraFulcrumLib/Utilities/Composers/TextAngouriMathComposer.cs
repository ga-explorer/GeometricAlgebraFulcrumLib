using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class TextAngouriMathComposer
        : TextComposerBase<Entity>
    {
        public static TextAngouriMathComposer DefaultComposer { get; }
            = new TextAngouriMathComposer();
        
        
        private TextAngouriMathComposer() 
            : base(ScalarAlgebraAngouriMathProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Entity scalar)
        {
            return scalar.ToString();
        }
    }
}