using DataStructuresLib.AttributeSet;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector2ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<IPair<double>>>
{
    public static GrKonvaJsVector2ArrayValue Create(IPair<double> vector1, IPair<double> vector2)
    {
        return new GrKonvaJsVector2ArrayValue(
            new []
            {
                vector1, 
                vector2
            }
        );
    }
    
    public static GrKonvaJsVector2ArrayValue Create(IReadOnlyList<IPair<double>> value)
    {
        return new GrKonvaJsVector2ArrayValue(value);
    }

    public static GrKonvaJsVector2ArrayValue Create(IPointsPath2D value)
    {
        return new GrKonvaJsVector2ArrayValue(value);
    }


    public static implicit operator GrKonvaJsVector2ArrayValue(string valueText)
    {
        return new GrKonvaJsVector2ArrayValue(valueText);
    }

    public static implicit operator GrKonvaJsVector2ArrayValue(Float64Vector2D[] value)
    {
        return new GrKonvaJsVector2ArrayValue(value);
    }
    
    public static implicit operator GrKonvaJsVector2ArrayValue(IFloat64Vector2D[] value)
    {
        return new GrKonvaJsVector2ArrayValue(value);
    }
        
    public static implicit operator GrKonvaJsVector2ArrayValue(ArrayPointsPath2D value)
    {
        return new GrKonvaJsVector2ArrayValue(value);
    }


    private GrKonvaJsVector2ArrayValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsVector2ArrayValue(IReadOnlyList<IPair<double>> value)
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