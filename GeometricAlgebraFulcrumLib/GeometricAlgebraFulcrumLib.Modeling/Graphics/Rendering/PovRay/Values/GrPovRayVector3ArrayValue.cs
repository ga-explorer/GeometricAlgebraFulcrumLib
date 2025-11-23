using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayVector3ArrayValue :
    GrPovRayValue<IReadOnlyList<LinFloat64Vector3D>>
{
    public static GrPovRayVector3ArrayValue Create(IReadOnlyList<LinFloat64Vector3D> value)
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
        
    public static implicit operator GrPovRayVector3ArrayValue(ArrayPointsPath3D value)
    {
        return new GrPovRayVector3ArrayValue(value);
    }


    private GrPovRayVector3ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayVector3ArrayValue(IReadOnlyList<LinFloat64Vector3D> value)
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