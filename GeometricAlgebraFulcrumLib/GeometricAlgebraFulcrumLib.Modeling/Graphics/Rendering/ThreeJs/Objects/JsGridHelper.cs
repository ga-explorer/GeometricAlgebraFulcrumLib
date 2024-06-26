using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsGridHelperConstructor :
    JsTypeConstructor
{
    public JsNumber Size { get; }
        
    public JsNumber Divisions { get; }
        
    public JsNumber Color1 { get; }
        
    public JsNumber Color2 { get; }
        
        


    internal JsGridHelperConstructor(JsNumber argSize, JsNumber argDivisions, JsNumber argColor1, JsNumber argColor2)
    {
        Size = argSize ?? (10).AsJsNumber();
        Divisions = argDivisions ?? (10).AsJsNumber();
        Color1 = argColor1 ?? (4473924).AsJsNumber();
        Color2 = argColor2 ?? (8947848).AsJsNumber();
    }

    public override string GetJsCode()
    {
        return $"new THREE.GridHelper({Size.GetJsCode()}, {Divisions.GetJsCode()}, {Color1.GetJsCode()}, {Color2.GetJsCode()})";
    }
}
    
public partial class JsGridHelper :
    JsLineSegments
{
    public static implicit operator JsGridHelper(string jsTextCode)
    {
        return new JsGridHelper(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsGridHelper value)
    {
        return value.GetJsCode();
    }


    private readonly JsGridHelper _jsVariableValue;
    public JsGridHelper JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsString _type;
    public JsString Type
    {
        get => _type ?? throw new InvalidOperationException();
        set
        {
            if (_type is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "\"GridHelper\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.type = {valueCode};");
        }
    }

    internal JsGridHelper(JsTypeConstructor jsCodeSource, JsGridHelper jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _type = $"{variableName}.type".AsJsStringVariable();
    }

    public JsGridHelper(JsNumber argSize = null, JsNumber argDivisions = null, JsNumber argColor1 = null, JsNumber argColor2 = null)
        : base(new JsGridHelperConstructor(argSize, argDivisions, argColor1, argColor2))
    {
    }

        
}