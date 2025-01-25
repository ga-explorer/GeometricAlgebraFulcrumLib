using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsBoundingBoxValue :
    SparseCodeAttributeValue<Float64BoundingBox2D>
{
    internal static GrKonvaJsBoundingBoxValue Create(Float64BoundingBox2D value)
    {
        return new GrKonvaJsBoundingBoxValue(value);
    }


    public static implicit operator GrKonvaJsBoundingBoxValue(string valueText)
    {
        return new GrKonvaJsBoundingBoxValue(valueText);
    }
    

    private GrKonvaJsBoundingBoxValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsBoundingBoxValue(Float64BoundingBox2D value)
        : base(value)
    {
    }


    public override string GetAttributeValueCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode() 
            : ValueText;
    }
}