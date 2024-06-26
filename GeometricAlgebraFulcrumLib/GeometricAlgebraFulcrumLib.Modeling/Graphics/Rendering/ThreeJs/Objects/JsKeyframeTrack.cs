using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsKeyframeTrackConstructor :
    JsTypeConstructor
{
    public JsType Name { get; }
        
    public JsType Times { get; }
        
    public JsType Values { get; }
        
    public JsType Interpolation { get; }
        
        


    internal JsKeyframeTrackConstructor(JsType argName, JsType argTimes, JsType argValues, JsType argInterpolation)
    {
        Name = argName ?? new JsObject();
        Times = argTimes ?? new JsObject();
        Values = argValues ?? new JsObject();
        Interpolation = argInterpolation ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.KeyframeTrack({Name.GetJsCode()}, {Times.GetJsCode()}, {Values.GetJsCode()}, {Interpolation.GetJsCode()})";
    }
}
    
public partial class JsKeyframeTrack :
    JsObjectType
{
    public static implicit operator JsKeyframeTrack(string jsTextCode)
    {
        return new JsKeyframeTrack(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsKeyframeTrack value)
    {
        return value.GetJsCode();
    }


    private readonly JsKeyframeTrack _jsVariableValue;
    public JsKeyframeTrack JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsType _name;
    public JsType Name
    {
        get => _name ?? throw new InvalidOperationException();
        set
        {
            if (_name is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.name = {valueCode};");
        }
    }
        
    private readonly JsType _times;
    public JsType Times
    {
        get => _times ?? throw new InvalidOperationException();
        set
        {
            if (_times is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.times = {valueCode};");
        }
    }
        
    private readonly JsType _values;
    public JsType Values
    {
        get => _values ?? throw new InvalidOperationException();
        set
        {
            if (_values is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.values = {valueCode};");
        }
    }

    internal JsKeyframeTrack(JsTypeConstructor jsCodeSource, JsKeyframeTrack jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _name = $"{variableName}.name".AsJsTypeVariable();
        _times = $"{variableName}.times".AsJsTypeVariable();
        _values = $"{variableName}.values".AsJsTypeVariable();
    }

    public JsKeyframeTrack(JsType argName = null, JsType argTimes = null, JsType argValues = null, JsType argInterpolation = null)
        : base(new JsKeyframeTrackConstructor(argName, argTimes, argValues, argInterpolation))
    {
    }

    public JsType InterpolantFactoryMethodDiscrete(JsType argResult = null)
    {
        return CallMethod("InterpolantFactoryMethodDiscrete", argResult ?? new JsObject());
    }
        
    public JsType InterpolantFactoryMethodLinear(JsType argResult = null)
    {
        return CallMethod("InterpolantFactoryMethodLinear", argResult ?? new JsObject());
    }
        
    public JsType InterpolantFactoryMethodSmooth(JsType argResult = null)
    {
        return CallMethod("InterpolantFactoryMethodSmooth", argResult ?? new JsObject());
    }
        
    public JsKeyframeTrack SetInterpolation(JsType argInterpolation = null)
    {
        CallMethodVoid("setInterpolation", argInterpolation ?? new JsObject());
            
        return this;
    }
        
    public JsNumber GetInterpolation()
    {
        return CallMethod("getInterpolation");
    }
        
    public JsType GetValueSize()
    {
        return CallMethod("getValueSize");
    }
        
    public JsKeyframeTrack Shift(JsType argTimeOffset = null)
    {
        CallMethodVoid("shift", argTimeOffset ?? new JsObject());
            
        return this;
    }
        
    public JsKeyframeTrack Scale(JsType argTimeScale = null)
    {
        CallMethodVoid("scale", argTimeScale ?? new JsObject());
            
        return this;
    }
        
    public JsKeyframeTrack Trim(JsType argStartTime = null, JsType argEndTime = null)
    {
        CallMethodVoid("trim", argStartTime ?? new JsObject(), argEndTime ?? new JsObject());
            
        return this;
    }
        
    public JsType Validate()
    {
        return CallMethod("validate");
    }
        
    public JsKeyframeTrack Optimize()
    {
        CallMethodVoid("optimize");
            
        return this;
    }
        
    public JsKeyframeTrack Clone()
    {
        return CallMethod("clone");
    }
        
        
}