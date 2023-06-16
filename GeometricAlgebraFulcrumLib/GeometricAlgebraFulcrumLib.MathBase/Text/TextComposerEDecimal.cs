using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class TextComposerEDecimal
        : TextComposer<EDecimal>
    {
        public static TextComposerEDecimal DefaultComposer { get; }
            = new TextComposerEDecimal();


        private TextComposerEDecimal() 
            : base(ScalarProcessorEDecimal.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
        {
            return $"{GetScalarText(EDecimal.FromDouble(angle.Degrees))} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(EDecimal scalar)
        {
            return scalar.ToString();
        }
    }
}