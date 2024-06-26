using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsWebGL1RendererConstructor :
    JsTypeConstructor
{
        


    internal JsWebGL1RendererConstructor()
    {
            
    }

    public override string GetJsCode()
    {
        return $"new THREE.WebGL1Renderer()";
    }
}
    
public partial class JsWebGL1Renderer :
    JsWebGLRenderer
{
    public static implicit operator JsWebGL1Renderer(string jsTextCode)
    {
        return new JsWebGL1Renderer(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsWebGL1Renderer value)
    {
        return value.GetJsCode();
    }


    private readonly JsWebGL1Renderer _jsVariableValue;
    public JsWebGL1Renderer JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

        

    internal JsWebGL1Renderer(JsTypeConstructor jsCodeSource, JsWebGL1Renderer jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

            
    }

    public JsWebGL1Renderer()
        : base(new JsWebGL1RendererConstructor())
    {
    }

        
}