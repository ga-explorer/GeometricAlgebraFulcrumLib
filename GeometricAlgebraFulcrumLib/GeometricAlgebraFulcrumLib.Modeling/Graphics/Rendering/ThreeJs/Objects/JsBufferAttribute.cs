using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsBufferAttributeConstructor :
    JsTypeConstructor
{
    public JsType Array { get; }
        
    public JsType ItemSize { get; }
        
    public JsType Normalized { get; }
        
        


    internal JsBufferAttributeConstructor(JsType argArray, JsType argItemSize, JsType argNormalized)
    {
        Array = argArray ?? new JsObject();
        ItemSize = argItemSize ?? new JsObject();
        Normalized = argNormalized ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.BufferAttribute({Array.GetJsCode()}, {ItemSize.GetJsCode()}, {Normalized.GetJsCode()})";
    }
}
    
public partial class JsBufferAttribute :
    JsObjectType
{
    public static implicit operator JsBufferAttribute(string jsTextCode)
    {
        return new JsBufferAttribute(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsBufferAttribute value)
    {
        return value.GetJsCode();
    }


    private readonly JsBufferAttribute _jsVariableValue;
    public JsBufferAttribute JsValue 
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
        
    private readonly JsType _array;
    public JsType Array
    {
        get => _array ?? throw new InvalidOperationException();
        set
        {
            if (_array is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.array = {valueCode};");
        }
    }
        
    private readonly JsType _itemSize;
    public JsType ItemSize
    {
        get => _itemSize ?? throw new InvalidOperationException();
        set
        {
            if (_itemSize is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.itemSize = {valueCode};");
        }
    }
        
    private readonly JsType _count;
    public JsType Count
    {
        get => _count ?? throw new InvalidOperationException();
        set
        {
            if (_count is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.count = {valueCode};");
        }
    }
        
    private readonly JsType _normalized;
    public JsType Normalized
    {
        get => _normalized ?? throw new InvalidOperationException();
        set
        {
            if (_normalized is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.normalized = {valueCode};");
        }
    }
        
    private readonly JsNumber _usage;
    public JsNumber Usage
    {
        get => _usage ?? throw new InvalidOperationException();
        set
        {
            if (_usage is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "35044";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.usage = {valueCode};");
        }
    }
        
    private readonly JsType _updateRange;
    public JsType UpdateRange
    {
        get => _updateRange ?? throw new InvalidOperationException();
        set
        {
            if (_updateRange is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.updateRange = {valueCode};");
        }
    }
        
    private readonly JsNumber _version;
    public JsNumber Version
    {
        get => _version ?? throw new InvalidOperationException();
        set
        {
            if (_version is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.version = {valueCode};");
        }
    }
        
    private readonly JsType _onUploadCallback;
    public JsType OnUploadCallback
    {
        get => _onUploadCallback ?? throw new InvalidOperationException();
        set
        {
            if (_onUploadCallback is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.onUploadCallback = {valueCode};");
        }
    }

    internal JsBufferAttribute(JsTypeConstructor jsCodeSource, JsBufferAttribute jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _name = $"{variableName}.name".AsJsStringVariable();
        _array = $"{variableName}.array".AsJsTypeVariable();
        _itemSize = $"{variableName}.itemSize".AsJsTypeVariable();
        _count = $"{variableName}.count".AsJsTypeVariable();
        _normalized = $"{variableName}.normalized".AsJsTypeVariable();
        _usage = $"{variableName}.usage".AsJsNumberVariable();
        _updateRange = $"{variableName}.updateRange".AsJsTypeVariable();
        _version = $"{variableName}.version".AsJsNumberVariable();
        _onUploadCallback = $"{variableName}.onUploadCallback".AsJsTypeVariable();
    }

    public JsBufferAttribute(JsType argArray = null, JsType argItemSize = null, JsType argNormalized = null)
        : base(new JsBufferAttributeConstructor(argArray, argItemSize, argNormalized))
    {
    }

    public JsType CallOnUploadCallback()
    {
        return CallMethod("onUploadCallback");
    }
        
    public JsBufferAttribute SetUsage(JsType argValue = null)
    {
        CallMethodVoid("setUsage", argValue ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute Copy(JsType argSource = null)
    {
        CallMethodVoid("copy", argSource ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute CopyAt(JsType argIndex1 = null, JsType argAttribute = null, JsType argIndex2 = null)
    {
        CallMethodVoid("copyAt", argIndex1 ?? new JsObject(), argAttribute ?? new JsObject(), argIndex2 ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute CopyArray(JsType argArray = null)
    {
        CallMethodVoid("copyArray", argArray ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute CopyColorsArray(JsType argColors = null)
    {
        CallMethodVoid("copyColorsArray", argColors ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute CopyVector2sArray(JsType argVectors = null)
    {
        CallMethodVoid("copyVector2sArray", argVectors ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute CopyVector3sArray(JsType argVectors = null)
    {
        CallMethodVoid("copyVector3sArray", argVectors ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute CopyVector4sArray(JsType argVectors = null)
    {
        CallMethodVoid("copyVector4sArray", argVectors ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute ApplyMatrix3(JsType argM = null)
    {
        CallMethodVoid("applyMatrix3", argM ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute ApplyMatrix4(JsType argM = null)
    {
        CallMethodVoid("applyMatrix4", argM ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute ApplyNormalMatrix(JsType argM = null)
    {
        CallMethodVoid("applyNormalMatrix", argM ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute TransformDirection(JsType argM = null)
    {
        CallMethodVoid("transformDirection", argM ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute Set(JsType argValue = null, JsNumber argOffset = null)
    {
        CallMethodVoid("set", argValue ?? new JsObject(), argOffset ?? (0).AsJsNumber());
            
        return this;
    }
        
    public JsType GetX(JsType argIndex = null)
    {
        return CallMethod("getX", argIndex ?? new JsObject());
    }
        
    public JsBufferAttribute SetX(JsType argIndex = null, JsType argX = null)
    {
        CallMethodVoid("setX", argIndex ?? new JsObject(), argX ?? new JsObject());
            
        return this;
    }
        
    public JsType GetY(JsType argIndex = null)
    {
        return CallMethod("getY", argIndex ?? new JsObject());
    }
        
    public JsBufferAttribute SetY(JsType argIndex = null, JsType argY = null)
    {
        CallMethodVoid("setY", argIndex ?? new JsObject(), argY ?? new JsObject());
            
        return this;
    }
        
    public JsType GetZ(JsType argIndex = null)
    {
        return CallMethod("getZ", argIndex ?? new JsObject());
    }
        
    public JsBufferAttribute SetZ(JsType argIndex = null, JsType argZ = null)
    {
        CallMethodVoid("setZ", argIndex ?? new JsObject(), argZ ?? new JsObject());
            
        return this;
    }
        
    public JsType GetW(JsType argIndex = null)
    {
        return CallMethod("getW", argIndex ?? new JsObject());
    }
        
    public JsBufferAttribute SetW(JsType argIndex = null, JsType argW = null)
    {
        CallMethodVoid("setW", argIndex ?? new JsObject(), argW ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute SetXY(JsType argIndex = null, JsType argX = null, JsType argY = null)
    {
        CallMethodVoid("setXY", argIndex ?? new JsObject(), argX ?? new JsObject(), argY ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute SetXYZ(JsType argIndex = null, JsType argX = null, JsType argY = null, JsType argZ = null)
    {
        CallMethodVoid("setXYZ", argIndex ?? new JsObject(), argX ?? new JsObject(), argY ?? new JsObject(), argZ ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute SetXYZW(JsType argIndex = null, JsType argX = null, JsType argY = null, JsType argZ = null, JsType argW = null)
    {
        CallMethodVoid("setXYZW", argIndex ?? new JsObject(), argX ?? new JsObject(), argY ?? new JsObject(), argZ ?? new JsObject(), argW ?? new JsObject());
            
        return this;
    }
        
    public JsBufferAttribute OnUpload(JsType argCallback = null)
    {
        CallMethodVoid("onUpload", argCallback ?? new JsObject());
            
        return this;
    }
        
    public JsType Clone()
    {
        return CallMethod("clone");
    }
        
    public JsObject ToJSON()
    {
        return CallMethod("toJSON");
    }
        
        
}