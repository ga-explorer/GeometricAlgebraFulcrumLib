using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using NumericalGeometryLib.BasicMath;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Text
{
    public sealed class TextERationalComposer
        : TextComposer<ERational>
    {
        public static TextERationalComposer DefaultComposer { get; }
            = new TextERationalComposer();


        private TextERationalComposer() 
            : base(ScalarAlgebraERationalProcessor.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(PlanarAngle angle)
        {
            return $"{GetScalarText(ERational.FromDouble(angle.Degrees))} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetScalarText(ERational scalar)
        {
            return scalar.ToString();
        }
    }
}