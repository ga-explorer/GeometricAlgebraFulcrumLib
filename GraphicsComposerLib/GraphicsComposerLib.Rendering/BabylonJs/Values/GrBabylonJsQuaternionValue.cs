using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsQuaternionValue :
        GrBabylonJsValue<Float64Quaternion>
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