using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Colors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.AttributeSet;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsColorLinearGradientListValue :
    SparseCodeAttributeValue<GrColorLinearGradientList>
{
    public static GrKonvaJsColorLinearGradientListValue Create(string valueText)
    {
        return new GrKonvaJsColorLinearGradientListValue(valueText);
    }
    
    public static GrKonvaJsColorLinearGradientListValue Create(Color color)
    {
        return new GrKonvaJsColorLinearGradientListValue(
            GrColorLinearGradientList.Create(color)
        );
    }
    
    public static GrKonvaJsColorLinearGradientListValue Create(Color color1, Color color2)
    {
        return new GrKonvaJsColorLinearGradientListValue(
            GrColorLinearGradientList.Create(color1, color2)
        );
    }
    
    public static GrKonvaJsColorLinearGradientListValue Create(params Color[] colorArray)
    {
        return new GrKonvaJsColorLinearGradientListValue(
            GrColorLinearGradientList.Create(colorArray)
        );
    }

    public static GrKonvaJsColorLinearGradientListValue Create(GrColorLinearGradientList colorList)
    {
        return new GrKonvaJsColorLinearGradientListValue(colorList);
    }


    public static implicit operator GrKonvaJsColorLinearGradientListValue(string valueText)
    {
        return new GrKonvaJsColorLinearGradientListValue(valueText);
    }
    
    public static implicit operator GrKonvaJsColorLinearGradientListValue(Color value)
    {
        return Create(value);
    }
    
    public static implicit operator GrKonvaJsColorLinearGradientListValue(System.Drawing.Color value)
    {
        return Create(value.ToImageSharpColor());
    }

    public static implicit operator GrKonvaJsColorLinearGradientListValue(GrColorLinearGradientList value)
    {
        return new GrKonvaJsColorLinearGradientListValue(value);
    }
    
    //public static implicit operator GrKonvaJsColorGradientValue(GrKonvaJsNamedColor3 value)
    //{
    //    return new GrKonvaJsColorGradientValue(value.GetKonvaJsCode());
    //}


    private GrKonvaJsColorLinearGradientListValue(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsColorLinearGradientListValue(GrColorLinearGradientList value)
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