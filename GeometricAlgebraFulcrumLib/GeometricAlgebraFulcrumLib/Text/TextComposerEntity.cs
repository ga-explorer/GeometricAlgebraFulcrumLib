using System.Runtime.CompilerServices;
using AngouriMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextComposerEntity
        : MathBase.Text.TextComposer<Entity>
    {
        public static TextComposerEntity DefaultComposer { get; }
            = new TextComposerEntity();
        
        
        private TextComposerEntity() 
            : base(ScalarAlgebraAngouriMathProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
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