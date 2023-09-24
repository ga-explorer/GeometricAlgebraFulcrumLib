using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsVector4ArrayValue :
        SparseCodeAttributeValue<IReadOnlyList<IQuad<double>>>
    {
        internal static GrBabylonJsVector4ArrayValue Create(IReadOnlyList<IQuad<double>> value)
        {
            return new GrBabylonJsVector4ArrayValue(value);
        }


        public static implicit operator GrBabylonJsVector4ArrayValue(string valueText)
        {
            return new GrBabylonJsVector4ArrayValue(valueText);
        }

        public static implicit operator GrBabylonJsVector4ArrayValue(Float64Vector4D[] value)
        {
            return new GrBabylonJsVector4ArrayValue(value);
        }


        private GrBabylonJsVector4ArrayValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsVector4ArrayValue(IReadOnlyList<IQuad<double>> value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.GetBabylonJsCode() 
                : ValueText;
        }
    }
}