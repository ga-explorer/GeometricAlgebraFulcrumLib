using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.MathBase.Text
{
    public sealed class TextComposerERational
        : TextComposer<ERational>
    {
        public static TextComposerERational DefaultComposer { get; }
            = new TextComposerERational();


        private TextComposerERational() 
            : base(ScalarProcessorOfERational.DefaultProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string GetAngleText(Float64PlanarAngle angle)
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