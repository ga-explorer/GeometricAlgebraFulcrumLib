using System.Collections.Immutable;
using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsVector3ArrayArrayValue :
        SparseCodeAttributeValue<IReadOnlyList<IReadOnlyList<ITriplet<double>>>>
    {
        public static GrBabylonJsVector3ArrayArrayValue Create(IReadOnlyList<IReadOnlyList<ITriplet<double>>> value)
        {
            return new GrBabylonJsVector3ArrayArrayValue(value);
        }

        public static GrBabylonJsVector3ArrayArrayValue Create(IPointsMesh3D value)
        {
            var rowList =
                value.Count1.GetRange(j => 
                    value.GetSlicePathAt(1, j)
                ).ToImmutableArray();

            return GrBabylonJsVector3ArrayArrayValue.Create(rowList);
        }


        public static implicit operator GrBabylonJsVector3ArrayArrayValue(string valueText)
        {
            return new GrBabylonJsVector3ArrayArrayValue(valueText);
        }

        public static implicit operator GrBabylonJsVector3ArrayArrayValue(Float64Vector3D[][] value)
        {
            return new GrBabylonJsVector3ArrayArrayValue(value);
        }
        
        public static implicit operator GrBabylonJsVector3ArrayArrayValue(IFloat64Vector3D[][] value)
        {
            return new GrBabylonJsVector3ArrayArrayValue(value);
        }
        
        public static implicit operator GrBabylonJsVector3ArrayArrayValue(ArrayPointsMesh3D value)
        {
            return GrBabylonJsVector3ArrayArrayValue.Create(value);
        }


        private GrBabylonJsVector3ArrayArrayValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsVector3ArrayArrayValue(IReadOnlyList<IReadOnlyList<ITriplet<double>>> value)
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