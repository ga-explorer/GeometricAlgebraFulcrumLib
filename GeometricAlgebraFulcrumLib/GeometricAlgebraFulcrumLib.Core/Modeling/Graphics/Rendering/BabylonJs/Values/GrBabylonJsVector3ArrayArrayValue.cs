using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsMesh.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3ArrayArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<IReadOnlyList<ITriplet<Float64Scalar>>>>
{
    public static GrBabylonJsVector3ArrayArrayValue Create(IReadOnlyList<IReadOnlyList<ITriplet<Float64Scalar>>> value)
    {
        return new GrBabylonJsVector3ArrayArrayValue(value);
    }

    public static GrBabylonJsVector3ArrayArrayValue Create(IPointsMesh3D value)
    {
        var rowList =
            value.Count1.GetRange(j => 
                value.GetSlicePathAt(1, j)
            ).ToImmutableArray();

        return Create(rowList);
    }


    public static implicit operator GrBabylonJsVector3ArrayArrayValue(string valueText)
    {
        return new GrBabylonJsVector3ArrayArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsVector3ArrayArrayValue(LinFloat64Vector3D[][] value)
    {
        return new GrBabylonJsVector3ArrayArrayValue(value);
    }
        
    public static implicit operator GrBabylonJsVector3ArrayArrayValue(ILinFloat64Vector3D[][] value)
    {
        return new GrBabylonJsVector3ArrayArrayValue(value);
    }
        
    public static implicit operator GrBabylonJsVector3ArrayArrayValue(ArrayPointsMesh3D value)
    {
        return Create(value);
    }


    private GrBabylonJsVector3ArrayArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector3ArrayArrayValue(IReadOnlyList<IReadOnlyList<ITriplet<Float64Scalar>>> value)
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