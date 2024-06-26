using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsWebXRManagerConstructor :
    JsTypeConstructor
{
    public JsType Renderer { get; }
        
    public JsType Gl { get; }
        
        


    internal JsWebXRManagerConstructor(JsType argRenderer, JsType argGl)
    {
        Renderer = argRenderer ?? new JsObject();
        Gl = argGl ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.WebXRManager({Renderer.GetJsCode()}, {Gl.GetJsCode()})";
    }
}
    
public partial class JsWebXRManager :
    JsEventDispatcher
{
    public static implicit operator JsWebXRManager(string jsTextCode)
    {
        return new JsWebXRManager(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsWebXRManager value)
    {
        return value.GetJsCode();
    }


    private readonly JsWebXRManager _jsVariableValue;
    public JsWebXRManager JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsBoolean _cameraAutoUpdate;
    public JsBoolean CameraAutoUpdate
    {
        get => _cameraAutoUpdate ?? throw new InvalidOperationException();
        set
        {
            if (_cameraAutoUpdate is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "true";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.cameraAutoUpdate = {valueCode};");
        }
    }
        
    private readonly JsBoolean _enabled;
    public JsBoolean Enabled
    {
        get => _enabled ?? throw new InvalidOperationException();
        set
        {
            if (_enabled is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "false";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.enabled = {valueCode};");
        }
    }
        
    private readonly JsBoolean _isPresenting;
    public JsBoolean IsPresenting
    {
        get => _isPresenting ?? throw new InvalidOperationException();
        set
        {
            if (_isPresenting is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "false";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.isPresenting = {valueCode};");
        }
    }
        
    private readonly JsType _getController;
    public JsType GetController
    {
        get => _getController ?? throw new InvalidOperationException();
        set
        {
            if (_getController is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getController = {valueCode};");
        }
    }
        
    private readonly JsType _getControllerGrip;
    public JsType GetControllerGrip
    {
        get => _getControllerGrip ?? throw new InvalidOperationException();
        set
        {
            if (_getControllerGrip is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getControllerGrip = {valueCode};");
        }
    }
        
    private readonly JsType _getHand;
    public JsType GetHand
    {
        get => _getHand ?? throw new InvalidOperationException();
        set
        {
            if (_getHand is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getHand = {valueCode};");
        }
    }
        
    private readonly JsType _setFramebufferScaleFactor;
    public JsType SetFramebufferScaleFactor
    {
        get => _setFramebufferScaleFactor ?? throw new InvalidOperationException();
        set
        {
            if (_setFramebufferScaleFactor is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.setFramebufferScaleFactor = {valueCode};");
        }
    }
        
    private readonly JsType _setReferenceSpaceType;
    public JsType SetReferenceSpaceType
    {
        get => _setReferenceSpaceType ?? throw new InvalidOperationException();
        set
        {
            if (_setReferenceSpaceType is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.setReferenceSpaceType = {valueCode};");
        }
    }
        
    private readonly JsType _getReferenceSpace;
    public JsType GetReferenceSpace
    {
        get => _getReferenceSpace ?? throw new InvalidOperationException();
        set
        {
            if (_getReferenceSpace is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getReferenceSpace = {valueCode};");
        }
    }
        
    private readonly JsType _getBaseLayer;
    public JsType GetBaseLayer
    {
        get => _getBaseLayer ?? throw new InvalidOperationException();
        set
        {
            if (_getBaseLayer is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getBaseLayer = {valueCode};");
        }
    }
        
    private readonly JsType _getBinding;
    public JsType GetBinding
    {
        get => _getBinding ?? throw new InvalidOperationException();
        set
        {
            if (_getBinding is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getBinding = {valueCode};");
        }
    }
        
    private readonly JsType _getFrame;
    public JsType GetFrame
    {
        get => _getFrame ?? throw new InvalidOperationException();
        set
        {
            if (_getFrame is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getFrame = {valueCode};");
        }
    }
        
    private readonly JsType _getSession;
    public JsType GetSession
    {
        get => _getSession ?? throw new InvalidOperationException();
        set
        {
            if (_getSession is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getSession = {valueCode};");
        }
    }
        
    private readonly JsType _setSession;
    public JsType SetSession
    {
        get => _setSession ?? throw new InvalidOperationException();
        set
        {
            if (_setSession is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.setSession = {valueCode};");
        }
    }
        
    private readonly JsType _updateCamera;
    public JsType UpdateCamera
    {
        get => _updateCamera ?? throw new InvalidOperationException();
        set
        {
            if (_updateCamera is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.updateCamera = {valueCode};");
        }
    }
        
    private readonly JsType _getCamera;
    public JsType GetCamera
    {
        get => _getCamera ?? throw new InvalidOperationException();
        set
        {
            if (_getCamera is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getCamera = {valueCode};");
        }
    }
        
    private readonly JsType _getFoveation;
    public JsType GetFoveation
    {
        get => _getFoveation ?? throw new InvalidOperationException();
        set
        {
            if (_getFoveation is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.getFoveation = {valueCode};");
        }
    }
        
    private readonly JsType _setFoveation;
    public JsType SetFoveation
    {
        get => _setFoveation ?? throw new InvalidOperationException();
        set
        {
            if (_setFoveation is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.setFoveation = {valueCode};");
        }
    }
        
    private readonly JsType _setAnimationLoop;
    public JsType SetAnimationLoop
    {
        get => _setAnimationLoop ?? throw new InvalidOperationException();
        set
        {
            if (_setAnimationLoop is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.setAnimationLoop = {valueCode};");
        }
    }
        
    private readonly JsType _dispose;
    public JsType Dispose
    {
        get => _dispose ?? throw new InvalidOperationException();
        set
        {
            if (_dispose is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.dispose = {valueCode};");
        }
    }

    internal JsWebXRManager(JsTypeConstructor jsCodeSource, JsWebXRManager jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _cameraAutoUpdate = $"{variableName}.cameraAutoUpdate".AsJsBooleanVariable();
        _enabled = $"{variableName}.enabled".AsJsBooleanVariable();
        _isPresenting = $"{variableName}.isPresenting".AsJsBooleanVariable();
        _getController = $"{variableName}.getController".AsJsTypeVariable();
        _getControllerGrip = $"{variableName}.getControllerGrip".AsJsTypeVariable();
        _getHand = $"{variableName}.getHand".AsJsTypeVariable();
        _setFramebufferScaleFactor = $"{variableName}.setFramebufferScaleFactor".AsJsTypeVariable();
        _setReferenceSpaceType = $"{variableName}.setReferenceSpaceType".AsJsTypeVariable();
        _getReferenceSpace = $"{variableName}.getReferenceSpace".AsJsTypeVariable();
        _getBaseLayer = $"{variableName}.getBaseLayer".AsJsTypeVariable();
        _getBinding = $"{variableName}.getBinding".AsJsTypeVariable();
        _getFrame = $"{variableName}.getFrame".AsJsTypeVariable();
        _getSession = $"{variableName}.getSession".AsJsTypeVariable();
        _setSession = $"{variableName}.setSession".AsJsTypeVariable();
        _updateCamera = $"{variableName}.updateCamera".AsJsTypeVariable();
        _getCamera = $"{variableName}.getCamera".AsJsTypeVariable();
        _getFoveation = $"{variableName}.getFoveation".AsJsTypeVariable();
        _setFoveation = $"{variableName}.setFoveation".AsJsTypeVariable();
        _setAnimationLoop = $"{variableName}.setAnimationLoop".AsJsTypeVariable();
        _dispose = $"{variableName}.dispose".AsJsTypeVariable();
    }

    public JsWebXRManager(JsType argRenderer = null, JsType argGl = null)
        : base(new JsWebXRManagerConstructor(argRenderer, argGl))
    {
    }

        
}