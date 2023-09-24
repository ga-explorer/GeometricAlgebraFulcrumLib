using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values
{
    public sealed class GrKonvaJsFloat32Value :
        SparseCodeAttributeValue<float>
    {
        public static implicit operator GrKonvaJsFloat32Value(string valueText)
        {
            return new GrKonvaJsFloat32Value(valueText);
        }

        public static implicit operator GrKonvaJsFloat32Value(float value)
        {
            return new GrKonvaJsFloat32Value(value);
        }

        public static implicit operator GrKonvaJsFloat32Value(double value)
        {
            return new GrKonvaJsFloat32Value((float) value);
        }
        
        public static implicit operator GrKonvaJsFloat32Value(Float64Scalar value)
        {
            return new GrKonvaJsFloat32Value((float) value);
        }


        private GrKonvaJsFloat32Value(string valueText)
            : base(valueText)
        {
        }

        private GrKonvaJsFloat32Value(float value)
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