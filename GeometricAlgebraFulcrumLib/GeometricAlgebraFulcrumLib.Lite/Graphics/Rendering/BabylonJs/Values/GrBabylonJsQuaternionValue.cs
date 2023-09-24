using DataStructuresLib.AttributeSet;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsQuaternionValue :
        SparseCodeAttributeValue<Float64Quaternion>
    {
        internal static GrBabylonJsQuaternionValue Create(Float64Quaternion value)
        {
            return new GrBabylonJsQuaternionValue(value);
        }


        public static implicit operator GrBabylonJsQuaternionValue(string valueText)
        {
            return new GrBabylonJsQuaternionValue(valueText);
        }

        public static implicit operator GrBabylonJsQuaternionValue(Float64Quaternion value)
        {
            return new GrBabylonJsQuaternionValue(value);
        }


        private GrBabylonJsQuaternionValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsQuaternionValue(Float64Quaternion value)
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