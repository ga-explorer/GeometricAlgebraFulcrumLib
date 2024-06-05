using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

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


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetBabylonJsCode() 
            : ValueText;
    }
}