using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsCubeCameraConstructor :
    JsTypeConstructor
{
    public JsType Near { get; }
        
    public JsType Far { get; }
        
    public JsType RenderTarget { get; }
        
        


    internal JsCubeCameraConstructor(JsType argNear, JsType argFar, JsType argRenderTarget)
    {
        Near = argNear ?? new JsObject();
        Far = argFar ?? new JsObject();
        RenderTarget = argRenderTarget ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.CubeCamera({Near.GetJsCode()}, {Far.GetJsCode()}, {RenderTarget.GetJsCode()})";
    }
}
    
public partial class JsCubeCamera :
    JsObject3D
{
    public static implicit operator JsCubeCamera(string jsTextCode)
    {
        return new JsCubeCamera(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsCubeCamera value)
    {
        return value.GetJsCode();
    }


    private readonly JsCubeCamera _jsVariableValue;
    public JsCubeCamera JsValue 
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
        
            var valueCode = value?.GetJsCode() ?? "\"CubeCamera\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.type = {valueCode};");
        }
    }
        
    private readonly JsType _renderTarget;
    public JsType RenderTarget
    {
        get => _renderTarget ?? throw new InvalidOperationException();
        set
        {
            if (_renderTarget is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.renderTarget = {valueCode};");
        }
    }

    internal JsCubeCamera(JsTypeConstructor jsCodeSource, JsCubeCamera jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _type = $"{variableName}.type".AsJsStringVariable();
        _renderTarget = $"{variableName}.renderTarget".AsJsTypeVariable();
    }

    public JsCubeCamera(JsType argNear = null, JsType argFar = null, JsType argRenderTarget = null)
        : base(new JsCubeCameraConstructor(argNear, argFar, argRenderTarget))
    {
    }

    public JsType Update(JsType argRenderer = null, JsType argScene = null)
    {
        return CallMethod("update", argRenderer ?? new JsObject(), argScene ?? new JsObject());
    }
        
        
}