using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class TextComposerEFloat
        : TextComposer<EFloat>
    {
        public static TextComposerEFloat DefaultComposer { get; }
            = new TextComposerEFloat();


        private TextComposerEFloat() 
            : base(ScalarProcessorEFloat.DefaultProcessor)
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