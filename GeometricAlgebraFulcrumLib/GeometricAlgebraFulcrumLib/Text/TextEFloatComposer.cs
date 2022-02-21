using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextEFloatComposer
        : TextComposer<EFloat>
    {
        public static TextEFloatComposer DefaultComposer { get; }
            = new TextEFloatComposer();


        private TextEFloatComposer() 
            : base(ScalarAlgebraEFloatProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
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