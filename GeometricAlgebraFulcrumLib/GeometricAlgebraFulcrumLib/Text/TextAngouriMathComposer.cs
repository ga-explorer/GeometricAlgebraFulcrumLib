using System.Runtime.CompilerServices;
using AngouriMath;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextAngouriMathComposer
        : TextComposer<Entity>
    {
        public static TextAngouriMathComposer DefaultComposer { get; }
            = new TextAngouriMathComposer();
        
        
        private TextAngouriMathComposer() 
            : base(ScalarAlgebraAngouriMathProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{angle.Degrees} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(Entity scalar)
        {
            return scalar.ToString();
        }
    }
}