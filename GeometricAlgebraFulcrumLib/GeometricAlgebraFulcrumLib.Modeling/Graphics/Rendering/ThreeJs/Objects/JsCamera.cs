using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsCameraConstructor :
    JsTypeConstructor
{
        


    internal JsCameraConstructor()
    {
            
    }

    public override string GetJsCode()
    {
        return $"new THREE.Camera()";
    }
}
    
public partial class JsCamera :
    JsObject3D
{
    public static implicit operator JsCamera(string jsTextCode)
    {
        return new JsCamera(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsCamera value)
    {
        return value.GetJsCode();
    }


    private readonly JsCamera _jsVariableValue;
    public JsCamera JsValue 
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
        
            var valueCode = value?.GetJsCode() ?? "\"Camera\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.type = {valueCode};");
        }
    }
        
    private readonly JsType _matrixWorldInverse;
    public JsType MatrixWorldInverse
    {
        get => _matrixWorldInverse ?? throw new InvalidOperationException();
        set
        {
            if (_matrixWorldInverse is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.matrixWorldInverse = {valueCode};");
        }
    }
        
    private readonly JsType _projectionMatrix;
    public JsType ProjectionMatrix
    {
        get => _projectionMatrix ?? throw new InvalidOperationException();
        set
        {
            if (_projectionMatrix is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.projectionMatrix = {valueCode};");
        }
    }
        
    private readonly JsType _projectionMatrixInverse;
    public JsType ProjectionMatrixInverse
    {
        get => _projectionMatrixInverse ?? throw new InvalidOperationException();
        set
        {
            if (_projectionMatrixInverse is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.projectionMatrixInverse = {valueCode};");
        }
    }

    internal JsCamera(JsTypeConstructor jsCodeSource, JsCamera jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _type = $"{variableName}.type".AsJsStringVariable();
        _matrixWorldInverse = $"{variableName}.matrixWorldInverse".AsJsTypeVariable();
        _projectionMatrix = $"{variableName}.projectionMatrix".AsJsTypeVariable();
        _projectionMatrixInverse = $"{variableName}.projectionMatrixInverse".AsJsTypeVariable();
    }

    public JsCamera()
        : base(new JsCameraConstructor())
    {
    }

    public JsCamera Copy(JsType argSource = null, JsType argRecursive = null)
    {
        CallMethodVoid("copy", argSource ?? new JsObject(), argRecursive ?? new JsObject());
            
        return this;
    }
        
    public JsType GetWorldDirection(JsType argTarget = null)
    {
        return CallMethod("getWorldDirection", argTarget ?? new JsObject());
    }
        
    public JsType UpdateMatrixWorld(JsType argForce = null)
    {
        return CallMethod("updateMatrixWorld", argForce ?? new JsObject());
    }
        
    public JsType UpdateWorldMatrix(JsType argUpdateParents = null, JsType argUpdateChildren = null)
    {
        return CallMethod("updateWorldMatrix", argUpdateParents ?? new JsObject(), argUpdateChildren ?? new JsObject());
    }
        
    public JsType Clone()
    {
        return CallMethod("clone");
    }
        
        
}