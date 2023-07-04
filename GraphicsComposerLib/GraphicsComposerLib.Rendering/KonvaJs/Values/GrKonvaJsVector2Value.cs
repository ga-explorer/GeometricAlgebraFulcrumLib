using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values
{
    public sealed class GrKonvaJsVector2Value :
        SparseCodeAttributeValue<IPair<double>>
    {
        internal static GrKonvaJsVector2Value Create(IPair<double> value)
        {
            return new GrKonvaJsVector2Value(value);
        }


        public static implicit operator GrKonvaJsVector2Value(string valueText)
        {
            return new GrKonvaJsVector2Value(valueText);
        }

        public static implicit operator GrKonvaJsVector2Value(Float64Vector2D value)
        {
            return new GrKonvaJsVector2Value(value);
        }


        private GrKonvaJsVector2Value(string valueText)
            : base(valueText)
        {
        }

        private GrKonvaJsVector2Value(IPair<double> value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.GetKonvaJsCode() 
                : ValueText;
        }
    }
}