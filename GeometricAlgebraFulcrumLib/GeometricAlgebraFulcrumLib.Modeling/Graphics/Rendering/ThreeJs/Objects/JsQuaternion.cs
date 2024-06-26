using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsQuaternionConstructor :
    JsTypeConstructor
{
    public JsNumber X { get; }
        
    public JsNumber Y { get; }
        
    public JsNumber Z { get; }
        
    public JsNumber W { get; }
        
        


    internal JsQuaternionConstructor(JsNumber argX, JsNumber argY, JsNumber argZ, JsNumber argW)
    {
        X = argX ?? (0).AsJsNumber();
        Y = argY ?? (0).AsJsNumber();
        Z = argZ ?? (0).AsJsNumber();
        W = argW ?? (1).AsJsNumber();
    }

    public override string GetJsCode()
    {
        return $"new THREE.Quaternion({X.GetJsCode()}, {Y.GetJsCode()}, {Z.GetJsCode()}, {W.GetJsCode()})";
    }
}
    
public partial class JsQuaternion :
    JsObjectType
{
    public static implicit operator JsQuaternion(string jsTextCode)
    {
        return new JsQuaternion(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsQuaternion value)
    {
        return value.GetJsCode();
    }


    private readonly JsQuaternion _jsVariableValue;
    public JsQuaternion JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsType _x;
    public JsType X
    {
        get => _x ?? throw new InvalidOperationException();
        set
        {
            if (_x is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.x = {valueCode};");
        }
    }
        
    private readonly JsType _y;
    public JsType Y
    {
        get => _y ?? throw new InvalidOperationException();
        set
        {
            if (_y is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.y = {valueCode};");
        }
    }
        
    private readonly JsType _z;
    public JsType Z
    {
        get => _z ?? throw new InvalidOperationException();
        set
        {
            if (_z is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.z = {valueCode};");
        }
    }
        
    private readonly JsType _w;
    public JsType W
    {
        get => _w ?? throw new InvalidOperationException();
        set
        {
            if (_w is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.w = {valueCode};");
        }
    }

    internal JsQuaternion(JsTypeConstructor jsCodeSource, JsQuaternion jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _x = $"{variableName}.x".AsJsTypeVariable();
        _y = $"{variableName}.y".AsJsTypeVariable();
        _z = $"{variableName}.z".AsJsTypeVariable();
        _w = $"{variableName}.w".AsJsTypeVariable();
    }

    public JsQuaternion(JsNumber argX = null, JsNumber argY = null, JsNumber argZ = null, JsNumber argW = null)
        : base(new JsQuaternionConstructor(argX, argY, argZ, argW))
    {
    }

    public JsQuaternion Set(JsType argX = null, JsType argY = null, JsType argZ = null, JsType argW = null)
    {
        CallMethodVoid("set", argX ?? new JsObject(), argY ?? new JsObject(), argZ ?? new JsObject(), argW ?? new JsObject());
            
        return this;
    }
        
    public JsType Clone()
    {
        return CallMethod("clone");
    }
        
    public JsQuaternion Copy(JsType argQuaternion = null)
    {
        CallMethodVoid("copy", argQuaternion ?? new JsObject());
            
        return this;
    }
        
    public JsQuaternion SetFromEuler(JsType argEuler = null, JsType argUpdate = null)
    {
        CallMethodVoid("setFromEuler", argEuler ?? new JsObject(), argUpdate ?? new JsObject());
            
        return this;
    }
        
    public JsQuaternion SetFromAxisAngle(JsType argAxis = null, JsType argAngle = null)
    {
        CallMethodVoid("setFromAxisAngle", argAxis ?? new JsObject(), argAngle ?? new JsObject());
            
        return this;
    }
        
    public JsQuaternion SetFromRotationMatrix(JsType argM = null)
    {
        CallMethodVoid("setFromRotationMatrix", argM ?? new JsObject());
            
        return this;
    }
        
    public JsType SetFromUnitVectors(JsType argVFrom = null, JsType argVTo = null)
    {
        return CallMethod("setFromUnitVectors", argVFrom ?? new JsObject(), argVTo ?? new JsObject());
    }
        
    public JsType AngleTo(JsType argQ = null)
    {
        return CallMethod("angleTo", argQ ?? new JsObject());
    }
        
    public JsQuaternion RotateTowards(JsType argQ = null, JsType argStep = null)
    {
        CallMethodVoid("rotateTowards", argQ ?? new JsObject(), argStep ?? new JsObject());
            
        return this;
    }
        
    public JsType Identity()
    {
        return CallMethod("identity");
    }
        
    public JsType Invert()
    {
        return CallMethod("invert");
    }
        
    public JsQuaternion Conjugate()
    {
        CallMethodVoid("conjugate");
            
        return this;
    }
        
    public JsType Dot(JsType argV = null)
    {
        return CallMethod("dot", argV ?? new JsObject());
    }
        
    public JsType LengthSq()
    {
        return CallMethod("lengthSq");
    }
        
    public JsType Length()
    {
        return CallMethod("length");
    }
        
    public JsQuaternion Normalize()
    {
        CallMethodVoid("normalize");
            
        return this;
    }
        
    public JsType Multiply(JsType argQ = null, JsType argP = null)
    {
        return CallMethod("multiply", argQ ?? new JsObject(), argP ?? new JsObject());
    }
        
    public JsType Premultiply(JsType argQ = null)
    {
        return CallMethod("premultiply", argQ ?? new JsObject());
    }
        
    public JsQuaternion MultiplyQuaternions(JsType argA = null, JsType argB = null)
    {
        CallMethodVoid("multiplyQuaternions", argA ?? new JsObject(), argB ?? new JsObject());
            
        return this;
    }
        
    public JsQuaternion Slerp(JsType argQb = null, JsType argT = null)
    {
        CallMethodVoid("slerp", argQb ?? new JsObject(), argT ?? new JsObject());
            
        return this;
    }
        
    public JsType SlerpQuaternions(JsType argQa = null, JsType argQb = null, JsType argT = null)
    {
        return CallMethod("slerpQuaternions", argQa ?? new JsObject(), argQb ?? new JsObject(), argT ?? new JsObject());
    }
        
    public JsType Random()
    {
        return CallMethod("random");
    }
        
    public JsType Equals(JsType argQuaternion = null)
    {
        return CallMethod("equals", argQuaternion ?? new JsObject());
    }
        
    public JsQuaternion FromArray(JsType argArray = null, JsNumber argOffset = null)
    {
        CallMethodVoid("fromArray", argArray ?? new JsObject(), argOffset ?? (0).AsJsNumber());
            
        return this;
    }
        
    public JsArray ToArray(JsArray argArray = null, JsNumber argOffset = null)
    {
        return CallMethod("toArray", argArray ?? new JsArray(), argOffset ?? (0).AsJsNumber());
    }
        
    public JsQuaternion FromBufferAttribute(JsType argAttribute = null, JsType argIndex = null)
    {
        CallMethodVoid("fromBufferAttribute", argAttribute ?? new JsObject(), argIndex ?? new JsObject());
            
        return this;
    }
        
    public JsQuaternion _onChange(JsType argCallback = null)
    {
        CallMethodVoid("_onChange", argCallback ?? new JsObject());
            
        return this;
    }
        
    public JsType _onChangeCallback()
    {
        return CallMethod("_onChangeCallback");
    }
        
        
}