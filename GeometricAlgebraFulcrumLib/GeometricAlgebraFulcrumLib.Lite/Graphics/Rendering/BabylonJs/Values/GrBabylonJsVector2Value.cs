using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsVector2Value :
        SparseCodeAttributeValue<IPair<double>>
    {
        internal static GrBabylonJsVector2Value Create(IPair<double> value)
        {
            return new GrBabylonJsVector2Value(value);
        }


        public static implicit operator GrBabylonJsVector2Value(string valueText)
        {
            return new GrBabylonJsVector2Value(valueText);
        }

        public static implicit operator GrBabylonJsVector2Value(Float64Vector2D value)
        {
            return new GrBabylonJsVector2Value(value);
        }


        private GrBabylonJsVector2Value(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsVector2Value(IPair<double> value)
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