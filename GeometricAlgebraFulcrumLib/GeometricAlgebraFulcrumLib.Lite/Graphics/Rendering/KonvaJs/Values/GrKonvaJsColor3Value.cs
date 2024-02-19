using DataStructuresLib.AttributeSet;
using WebComposerLib.Colors;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.KonvaJs.Values;

public sealed class GrKonvaJsColor3Value :
    SparseCodeAttributeValue<Color>
{
    public static implicit operator GrKonvaJsColor3Value(string valueText)
    {
        return new GrKonvaJsColor3Value(valueText);
    }
    
    public static implicit operator GrKonvaJsColor3Value(System.Drawing.Color value)
    {
        return new GrKonvaJsColor3Value(value.ToImageSharpColor());
    }

    public static implicit operator GrKonvaJsColor3Value(Color value)
    {
        return new GrKonvaJsColor3Value(value);
    }
    
    //public static implicit operator GrKonvaJsColor3Value(GrKonvaJsNamedColor3 value)
    //{
    //    return new GrKonvaJsColor3Value(value.GetKonvaJsCode());
    //}


    private GrKonvaJsColor3Value(string valueText)
        : base(valueText)
    {
    }

    private GrKonvaJsColor3Value(Color value)
        : base(value)
    {
    }


    public override string GetCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetKonvaJsCode(false) 
            : ValueText;
    }
}