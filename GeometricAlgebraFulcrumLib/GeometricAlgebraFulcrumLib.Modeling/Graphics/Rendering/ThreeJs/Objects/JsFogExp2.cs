using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsFogExp2Constructor :
    JsTypeConstructor
{
    public JsType Color { get; }
        
    public JsNumber Density { get; }
        
        


    internal JsFogExp2Constructor(JsType argColor, JsNumber argDensity)
    {
        Color = argColor ?? new JsObject();
        Density = argDensity ?? (0.00025).AsJsNumber();
    }

    public override string GetJsCode()
    {
        return $"new THREE.FogExp2({Color.GetJsCode()}, {Density.GetJsCode()})";
    }
}
    
public partial class JsFogExp2 :
    JsObjectType
{
    public static implicit operator JsFogExp2(string jsTextCode)
    {
        return new JsFogExp2(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsFogExp2 value)
    {
        return value.GetJsCode();
    }


    private readonly JsFogExp2 _jsVariableValue;
    public JsFogExp2 JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsString _name;
    public JsString Name
    {
        get => _name ?? throw new InvalidOperationException();
        set
        {
            if (_name is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "\"\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.name = {valueCode};");
        }
    }
        
    private readonly JsType _color;
    public JsType Color
    {
        get => _color ?? throw new InvalidOperationException();
        set
        {
            if (_color is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.color = {valueCode};");
        }
    }
        
    private readonly JsNumber _density;
    public JsNumber Density
    {
        get => _density ?? throw new InvalidOperationException();
        set
        {
            if (_density is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0.00025";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.density = {valueCode};");
        }
    }

    internal JsFogExp2(JsTypeConstructor jsCodeSource, JsFogExp2 jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _name = $"{variableName}.name".AsJsStringVariable();
        _color = $"{variableName}.color".AsJsTypeVariable();
        _density = $"{variableName}.density".AsJsNumberVariable();
    }

    public JsFogExp2(JsType argColor = null, JsNumber argDensity = null)
        : base(new JsFogExp2Constructor(argColor, argDensity))
    {
    }

    public JsType Clone()
    {
        return CallMethod("clone");
    }
        
    public JsType ToJSON()
    {
        return CallMethod("toJSON");
    }
        
        
}