using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsCubicInterpolantConstructor :
    JsTypeConstructor
{
    public JsType ParameterPositions { get; }
        
    public JsType SampleValues { get; }
        
    public JsType SampleSize { get; }
        
    public JsType ResultBuffer { get; }
        
        


    internal JsCubicInterpolantConstructor(JsType argParameterPositions, JsType argSampleValues, JsType argSampleSize, JsType argResultBuffer)
    {
        ParameterPositions = argParameterPositions ?? new JsObject();
        SampleValues = argSampleValues ?? new JsObject();
        SampleSize = argSampleSize ?? new JsObject();
        ResultBuffer = argResultBuffer ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.CubicInterpolant({ParameterPositions.GetJsCode()}, {SampleValues.GetJsCode()}, {SampleSize.GetJsCode()}, {ResultBuffer.GetJsCode()})";
    }
}
    
public partial class JsCubicInterpolant :
    JsInterpolant
{
    public static implicit operator JsCubicInterpolant(string jsTextCode)
    {
        return new JsCubicInterpolant(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsCubicInterpolant value)
    {
        return value.GetJsCode();
    }


    private readonly JsCubicInterpolant _jsVariableValue;
    public JsCubicInterpolant JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsType _defaultSettings_;
    public JsType DefaultSettings_
    {
        get => _defaultSettings_ ?? throw new InvalidOperationException();
        set
        {
            if (_defaultSettings_ is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.DefaultSettings_ = {valueCode};");
        }
    }

    internal JsCubicInterpolant(JsTypeConstructor jsCodeSource, JsCubicInterpolant jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _defaultSettings_ = $"{variableName}.DefaultSettings_".AsJsTypeVariable();
    }

    public JsCubicInterpolant(JsType argParameterPositions = null, JsType argSampleValues = null, JsType argSampleSize = null, JsType argResultBuffer = null)
        : base(new JsCubicInterpolantConstructor(argParameterPositions, argSampleValues, argSampleSize, argResultBuffer))
    {
    }

    public JsType IntervalChanged_(JsType argI1 = null, JsType argT0 = null, JsType argT1 = null)
    {
        return CallMethod("intervalChanged_", argI1 ?? new JsObject(), argT0 ?? new JsObject(), argT1 ?? new JsObject());
    }
        
    public JsType Interpolate_(JsType argI1 = null, JsType argT0 = null, JsType argT = null, JsType argT1 = null)
    {
        return CallMethod("interpolate_", argI1 ?? new JsObject(), argT0 ?? new JsObject(), argT ?? new JsObject(), argT1 ?? new JsObject());
    }
        
        
}