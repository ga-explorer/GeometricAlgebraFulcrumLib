using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextEFloatComposer
        : TextComposer<EFloat>
    {
        public static TextEFloatComposer DefaultComposer { get; }
            = new TextEFloatComposer();


        private TextEFloatComposer() 
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