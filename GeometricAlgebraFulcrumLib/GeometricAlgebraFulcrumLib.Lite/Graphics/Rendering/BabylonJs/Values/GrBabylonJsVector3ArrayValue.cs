using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

public sealed class GrBabylonJsVector3ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<ITriplet<double>>>
{
    public static GrBabylonJsVector3ArrayValue Create(IReadOnlyList<ITriplet<double>> value)
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

    public static implicit operator GrBabylonJsVector3ArrayValue(Float64Vector3D[] value)
    {
        return new GrBabylonJsVector3ArrayValue(value);
    }
    
    public static implicit operator GrBabylonJsVector3ArrayValue(IFloat64Vector3D[] value)
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

    private GrBabylonJsVector3ArrayValue(IReadOnlyList<ITriplet<double>> value)
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