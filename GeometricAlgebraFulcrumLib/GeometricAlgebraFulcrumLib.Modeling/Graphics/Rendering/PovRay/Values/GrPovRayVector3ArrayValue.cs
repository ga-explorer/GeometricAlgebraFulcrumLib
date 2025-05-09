using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector3ArrayValue :
    GrPovRayValue<IReadOnlyList<ITriplet<Float64Scalar>>>
{
    public static GrPovRayVector3ArrayValue Create(IReadOnlyList<ITriplet<Float64Scalar>> value)
    {
        return new GrPovRayVector3ArrayValue(value);
    }

    public static GrPovRayVector3ArrayValue Create(IPointsPath3D value)
    {
        return new GrPovRayVector3ArrayValue(value);
    }


    public static implicit operator GrPovRayVector3ArrayValue(string valueText)
    {
        return new GrPovRayVector3ArrayValue(valueText);
    }

    public static implicit operator GrPovRayVector3ArrayValue(LinFloat64Vector3D[] value)
    {
        return new GrPovRayVector3ArrayValue(value);
    }
    
    public static implicit operator GrPovRayVector3ArrayValue(ILinFloat64Vector3D[] value)
    {
        return new GrPovRayVector3ArrayValue(value);
    }
        
    public static implicit operator GrPovRayVector3ArrayValue(ArrayPointsPath3D value)
    {
        return new GrPovRayVector3ArrayValue(value);
    }


    private GrPovRayVector3ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector3ArrayValue(IReadOnlyList<ITriplet<Float64Scalar>> value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCode() 
            : ValueText;
    }
}