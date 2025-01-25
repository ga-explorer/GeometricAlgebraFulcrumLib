using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<ITriplet<Float64Scalar>>>
{
    public static GrBabylonJsVector3ArrayValue Create(IReadOnlyList<ITriplet<Float64Scalar>> value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }

    public static GrBabylonJsVector3ArrayValue Create(IPointsPath3D value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }


    public static implicit operator GrBabylonJsVector3ArrayValue(string valueText)
    {
        return new GrBabylonJsVector3ArrayValue(valueText);
    }

    public static implicit operator GrBabylonJsVector3ArrayValue(LinFloat64Vector3D[] value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }
    
    public static implicit operator GrBabylonJsVector3ArrayValue(ILinFloat64Vector3D[] value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }
        
    public static implicit operator GrBabylonJsVector3ArrayValue(ArrayPointsPath3D value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }


    private GrBabylonJsVector3ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrBabylonJsVector3ArrayValue(IReadOnlyList<ITriplet<Float64Scalar>> value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}