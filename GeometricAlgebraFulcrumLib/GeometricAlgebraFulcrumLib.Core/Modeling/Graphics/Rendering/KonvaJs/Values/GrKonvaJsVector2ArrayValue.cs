using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Meshes.PointsPath.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsVector2ArrayValue :
    SparseCodeAttributeValue<IReadOnlyList<IPair<Float64Scalar>>>
{
    public static GrKonvaJsVector2ArrayValue Create(IPair<Float64Scalar> vector1, IPair<Float64Scalar> vector2)
    {
        return new GrKonvaJsVector2ArrayValue(
            new []
            {
                vector1, 
                vector2
            }
        );
    }
    
    public static GrKonvaJsVector2ArrayValue Create(IReadOnlyList<IPair<Float64Scalar>> value)
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

    public static implicit operator GrKonvaJsVector2ArrayValue(LinFloat64Vector2D[] value)
    {
        return new GrKonvaJsVector2ArrayValue(value);
    }
    
    public static implicit operator GrKonvaJsVector2ArrayValue(ILinFloat64Vector2D[] value)
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

    private GrKonvaJsVector2ArrayValue(IReadOnlyList<IPair<Float64Scalar>> value)
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