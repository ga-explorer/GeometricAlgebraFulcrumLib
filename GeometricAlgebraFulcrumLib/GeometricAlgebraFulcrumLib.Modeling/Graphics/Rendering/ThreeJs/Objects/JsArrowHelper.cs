using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsArrowHelperConstructor :
    JsTypeConstructor
{
    public JsVector3 Dir { get; }
        
    public JsVector3 Origin { get; }
        
    public JsNumber Length { get; }
        
    public JsNumber Color { get; }
        
    public JsType HeadLength { get; }
        
    public JsType HeadWidth { get; }
        
        


    internal JsArrowHelperConstructor(JsVector3 argDir, JsVector3 argOrigin, JsNumber argLength, JsNumber argColor, JsType argHeadLength, JsType argHeadWidth)
    {
        Dir = argDir ?? new JsVector3((0).AsJsNumber(), (0).AsJsNumber(), (1).AsJsNumber());
        Origin = argOrigin ?? new JsVector3((0).AsJsNumber(), (0).AsJsNumber(), (0).AsJsNumber());
        Length = argLength ?? (1).AsJsNumber();
        Color = argColor ?? (16776960).AsJsNumber();
        HeadLength = argHeadLength ?? new JsObject();
        HeadWidth = argHeadWidth ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.ArrowHelper({Dir.GetJsCode()}, {Origin.GetJsCode()}, {Length.GetJsCode()}, {Color.GetJsCode()}, {HeadLength.GetJsCode()}, {HeadWidth.GetJsCode()})";
    }
}
    
public partial class JsArrowHelper :
    JsObject3D
{
    public static implicit operator JsArrowHelper(string jsTextCode)
    {
        return new JsArrowHelper(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsArrowHelper value)
    {
        return value.GetJsCode();
    }


    private readonly JsArrowHelper _jsVariableValue;
    public JsArrowHelper JsValue 
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
        
            var valueCode = value?.GetJsCode() ?? "\"ArrowHelper\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.type = {valueCode};");
        }
    }
        
    private readonly JsType _line;
    public JsType Line
    {
        get => _line ?? throw new InvalidOperationException();
        set
        {
            if (_line is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.line = {valueCode};");
        }
    }
        
    private readonly JsType _cone;
    public JsType Cone
    {
        get => _cone ?? throw new InvalidOperationException();
        set
        {
            if (_cone is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.cone = {valueCode};");
        }
    }

    internal JsArrowHelper(JsTypeConstructor jsCodeSource, JsArrowHelper jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _type = $"{variableName}.type".AsJsStringVariable();
        _line = $"{variableName}.line".AsJsTypeVariable();
        _cone = $"{variableName}.cone".AsJsTypeVariable();
    }

    public JsArrowHelper(JsVector3 argDir = null, JsVector3 argOrigin = null, JsNumber argLength = null, JsNumber argColor = null, JsType argHeadLength = null, JsType argHeadWidth = null)
        : base(new JsArrowHelperConstructor(argDir, argOrigin, argLength, argColor, argHeadLength, argHeadWidth))
    {
    }

    public JsType SetDirection(JsType argDir = null)
    {
        return CallMethod("setDirection", argDir ?? new JsObject());
    }
        
    public JsType SetLength(JsType argLength = null, JsType argHeadLength = null, JsType argHeadWidth = null)
    {
        return CallMethod("setLength", argLength ?? new JsObject(), argHeadLength ?? new JsObject(), argHeadWidth ?? new JsObject());
    }
        
    public JsType SetColor(JsType argColor = null)
    {
        return CallMethod("setColor", argColor ?? new JsObject());
    }
        
    public JsArrowHelper Copy(JsType argSource = null)
    {
        CallMethodVoid("copy", argSource ?? new JsObject());
            
        return this;
    }
        
        
}