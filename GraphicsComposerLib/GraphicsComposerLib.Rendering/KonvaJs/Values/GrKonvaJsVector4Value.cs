using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values
{
    public sealed class GrKonvaJsVector4Value :
        SparseCodeAttributeValue<IQuad<double>>
    {
        internal static GrKonvaJsVector4Value Create(IQuad<double> value)
        {
            return new GrKonvaJsVector4Value(value);
        }


        public static implicit operator GrKonvaJsVector4Value(string valueText)
        {
            return new GrKonvaJsVector4Value(valueText);
        }

        public static implicit operator GrKonvaJsVector4Value(Float64Vector4D value)
        {
            return new GrKonvaJsVector4Value(value);
        }


        private GrKonvaJsVector4Value(string valueText)
            : base(valueText)
        {
        }

        private GrKonvaJsVector4Value(IQuad<double> value)
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