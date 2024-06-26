using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsLine3Constructor :
    JsTypeConstructor
{
    public JsVector3 Start { get; }
        
    public JsVector3 End { get; }
        
        


    internal JsLine3Constructor(JsVector3 argStart, JsVector3 argEnd)
    {
        Start = argStart ?? new JsVector3();
        End = argEnd ?? new JsVector3();
    }

    public override string GetJsCode()
    {
        return $"new THREE.Line3({Start.GetJsCode()}, {End.GetJsCode()})";
    }
}
    
public partial class JsLine3 :
    JsObjectType
{
    public static implicit operator JsLine3(string jsTextCode)
    {
        return new JsLine3(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsLine3 value)
    {
        return value.GetJsCode();
    }


    private readonly JsLine3 _jsVariableValue;
    public JsLine3 JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsVector3 _start;
    public JsVector3 Start
    {
        get => _start ?? throw new InvalidOperationException();
        set
        {
            if (_start is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.start = {valueCode};");
        }
    }
        
    private readonly JsVector3 _end;
    public JsVector3 End
    {
        get => _end ?? throw new InvalidOperationException();
        set
        {
            if (_end is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.end = {valueCode};");
        }
    }

    internal JsLine3(JsTypeConstructor jsCodeSource, JsLine3 jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _start = $"{variableName}.start".AsJsVector3Variable();
        _end = $"{variableName}.end".AsJsVector3Variable();
    }

    public JsLine3(JsVector3 argStart = null, JsVector3 argEnd = null)
        : base(new JsLine3Constructor(argStart, argEnd))
    {
    }

    public JsLine3 Set(JsType argStart = null, JsType argEnd = null)
    {
        CallMethodVoid("set", argStart ?? new JsObject(), argEnd ?? new JsObject());
            
        return this;
    }
        
    public JsLine3 Copy(JsType argLine = null)
    {
        CallMethodVoid("copy", argLine ?? new JsObject());
            
        return this;
    }
        
    public JsType GetCenter(JsType argTarget = null)
    {
        return CallMethod("getCenter", argTarget ?? new JsObject());
    }
        
    public JsType Delta(JsType argTarget = null)
    {
        return CallMethod("delta", argTarget ?? new JsObject());
    }
        
    public JsType DistanceSq()
    {
        return CallMethod("distanceSq");
    }
        
    public JsType Distance()
    {
        return CallMethod("distance");
    }
        
    public JsType At(JsType argT = null, JsType argTarget = null)
    {
        return CallMethod("at", argT ?? new JsObject(), argTarget ?? new JsObject());
    }
        
    public JsType ClosestPointToPointParameter(JsType argPoint = null, JsType argClampToLine = null)
    {
        return CallMethod("closestPointToPointParameter", argPoint ?? new JsObject(), argClampToLine ?? new JsObject());
    }
        
    public JsType ClosestPointToPoint(JsType argPoint = null, JsType argClampToLine = null, JsType argTarget = null)
    {
        return CallMethod("closestPointToPoint", argPoint ?? new JsObject(), argClampToLine ?? new JsObject(), argTarget ?? new JsObject());
    }
        
    public JsLine3 ApplyMatrix4(JsType argMatrix = null)
    {
        CallMethodVoid("applyMatrix4", argMatrix ?? new JsObject());
            
        return this;
    }
        
    public JsType Equals(JsType argLine = null)
    {
        return CallMethod("equals", argLine ?? new JsObject());
    }
        
    public JsType Clone()
    {
        return CallMethod("clone");
    }
        
        
}