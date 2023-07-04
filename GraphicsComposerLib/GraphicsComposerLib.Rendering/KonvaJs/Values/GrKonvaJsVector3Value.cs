using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.KonvaJs.Values
{
    public sealed class GrKonvaJsVector3Value :
        SparseCodeAttributeValue<ITriplet<double>>
    {
        internal static GrKonvaJsVector3Value Create(ITriplet<double> value)
        {
            return new GrKonvaJsVector3Value(value);
        }


        public static implicit operator GrKonvaJsVector3Value(string valueText)
        {
            return new GrKonvaJsVector3Value(valueText);
        }

        public static implicit operator GrKonvaJsVector3Value(Float64Vector3D value)
        {
            return new GrKonvaJsVector3Value(value);
        }


        private GrKonvaJsVector3Value(string valueText)
            : base(valueText)
        {
        }

        private GrKonvaJsVector3Value(ITriplet<double> value)
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