using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class TextComposerEFloat
        : TextComposer<EFloat>
    {
        public static TextComposerEFloat DefaultComposer { get; }
            = new TextComposerEFloat();


        private TextComposerEFloat() 
            : base(ScalarProcessorOfEFloat.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
        {
            return $"{GetScalarText(EFloat.FromDouble(angle.Degrees))} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(EFloat scalar)
        {
            return scalar.ToString();
        }
    }
}