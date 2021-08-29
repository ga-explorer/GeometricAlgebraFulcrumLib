using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class AngouriMathTextComposer
        : TextComposerBase<Entity>
    {
        public static AngouriMathTextComposer DefaultComposer { get; }
            = new AngouriMathTextComposer();
        
        
        private AngouriMathTextComposer() 
            : base(AngouriMathScalarProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Entity scalar)
        {
            return scalar.ToString();
        }
    }
}