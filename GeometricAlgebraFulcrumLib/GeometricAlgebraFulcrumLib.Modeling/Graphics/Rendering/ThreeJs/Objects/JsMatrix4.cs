using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsMatrix4Constructor :
    JsTypeConstructor
{
        


    internal JsMatrix4Constructor()
    {
            
    }

    public override string GetJsCode()
    {
        return $"new THREE.Matrix4()";
    }
}
    
public partial class JsMatrix4 :
    JsObjectType
{
    public static implicit operator JsMatrix4(string jsTextCode)
    {
        return new JsMatrix4(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsMatrix4 value)
    {
        return value.GetJsCode();
    }


    private readonly JsMatrix4 _jsVariableValue;
    public JsMatrix4 JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsType _elements;
    public JsType Elements
    {
        get => _elements ?? throw new InvalidOperationException();
        set
        {
            if (_elements is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.elements = {valueCode};");
        }
    }

    internal JsMatrix4(JsTypeConstructor jsCodeSource, JsMatrix4 jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _elements = $"{variableName}.elements".AsJsTypeVariable();
    }

    public JsMatrix4()
        : base(new JsMatrix4Constructor())
    {
    }

    public JsMatrix4 Set(JsType argN11 = null, JsType argN12 = null, JsType argN13 = null, JsType argN14 = null, JsType argN21 = null, JsType argN22 = null, JsType argN23 = null, JsType argN24 = null, JsType argN31 = null, JsType argN32 = null, JsType argN33 = null, JsType argN34 = null, JsType argN41 = null, JsType argN42 = null, JsType argN43 = null, JsType argN44 = null)
    {
        CallMethodVoid("set", argN11 ?? new JsObject(), argN12 ?? new JsObject(), argN13 ?? new JsObject(), argN14 ?? new JsObject(), argN21 ?? new JsObject(), argN22 ?? new JsObject(), argN23 ?? new JsObject(), argN24 ?? new JsObject(), argN31 ?? new JsObject(), argN32 ?? new JsObject(), argN33 ?? new JsObject(), argN34 ?? new JsObject(), argN41 ?? new JsObject(), argN42 ?? new JsObject(), argN43 ?? new JsObject(), argN44 ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 Identity()
    {
        CallMethodVoid("identity");
            
        return this;
    }
        
    public JsType Clone()
    {
        return CallMethod("clone");
    }
        
    public JsMatrix4 Copy(JsType argM = null)
    {
        CallMethodVoid("copy", argM ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 CopyPosition(JsType argM = null)
    {
        CallMethodVoid("copyPosition", argM ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 SetFromMatrix3(JsType argM = null)
    {
        CallMethodVoid("setFromMatrix3", argM ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 ExtractBasis(JsType argXAxis = null, JsType argYAxis = null, JsType argZAxis = null)
    {
        CallMethodVoid("extractBasis", argXAxis ?? new JsObject(), argYAxis ?? new JsObject(), argZAxis ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeBasis(JsType argXAxis = null, JsType argYAxis = null, JsType argZAxis = null)
    {
        CallMethodVoid("makeBasis", argXAxis ?? new JsObject(), argYAxis ?? new JsObject(), argZAxis ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 ExtractRotation(JsType argM = null)
    {
        CallMethodVoid("extractRotation", argM ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeRotationFromEuler(JsType argEuler = null)
    {
        CallMethodVoid("makeRotationFromEuler", argEuler ?? new JsObject());
            
        return this;
    }
        
    public JsType MakeRotationFromQuaternion(JsType argQ = null)
    {
        return CallMethod("makeRotationFromQuaternion", argQ ?? new JsObject());
    }
        
    public JsMatrix4 LookAt(JsType argEye = null, JsType argTarget = null, JsType argUp = null)
    {
        CallMethodVoid("lookAt", argEye ?? new JsObject(), argTarget ?? new JsObject(), argUp ?? new JsObject());
            
        return this;
    }
        
    public JsType Multiply(JsType argM = null, JsType argN = null)
    {
        return CallMethod("multiply", argM ?? new JsObject(), argN ?? new JsObject());
    }
        
    public JsType Premultiply(JsType argM = null)
    {
        return CallMethod("premultiply", argM ?? new JsObject());
    }
        
    public JsMatrix4 MultiplyMatrices(JsType argA = null, JsType argB = null)
    {
        CallMethodVoid("multiplyMatrices", argA ?? new JsObject(), argB ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MultiplyScalar(JsType argS = null)
    {
        CallMethodVoid("multiplyScalar", argS ?? new JsObject());
            
        return this;
    }
        
    public JsType Determinant()
    {
        return CallMethod("determinant");
    }
        
    public JsMatrix4 Transpose()
    {
        CallMethodVoid("transpose");
            
        return this;
    }
        
    public JsMatrix4 SetPosition(JsType argX = null, JsType argY = null, JsType argZ = null)
    {
        CallMethodVoid("setPosition", argX ?? new JsObject(), argY ?? new JsObject(), argZ ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 Invert()
    {
        CallMethodVoid("invert");
            
        return this;
    }
        
    public JsMatrix4 Scale(JsType argV = null)
    {
        CallMethodVoid("scale", argV ?? new JsObject());
            
        return this;
    }
        
    public JsType GetMaxScaleOnAxis()
    {
        return CallMethod("getMaxScaleOnAxis");
    }
        
    public JsMatrix4 MakeTranslation(JsType argX = null, JsType argY = null, JsType argZ = null)
    {
        CallMethodVoid("makeTranslation", argX ?? new JsObject(), argY ?? new JsObject(), argZ ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeRotationX(JsType argTheta = null)
    {
        CallMethodVoid("makeRotationX", argTheta ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeRotationY(JsType argTheta = null)
    {
        CallMethodVoid("makeRotationY", argTheta ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeRotationZ(JsType argTheta = null)
    {
        CallMethodVoid("makeRotationZ", argTheta ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeRotationAxis(JsType argAxis = null, JsType argAngle = null)
    {
        CallMethodVoid("makeRotationAxis", argAxis ?? new JsObject(), argAngle ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeScale(JsType argX = null, JsType argY = null, JsType argZ = null)
    {
        CallMethodVoid("makeScale", argX ?? new JsObject(), argY ?? new JsObject(), argZ ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeShear(JsType argXy = null, JsType argXz = null, JsType argYx = null, JsType argYz = null, JsType argZx = null, JsType argZy = null)
    {
        CallMethodVoid("makeShear", argXy ?? new JsObject(), argXz ?? new JsObject(), argYx ?? new JsObject(), argYz ?? new JsObject(), argZx ?? new JsObject(), argZy ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 Compose(JsType argPosition = null, JsType argQuaternion = null, JsType argScale = null)
    {
        CallMethodVoid("compose", argPosition ?? new JsObject(), argQuaternion ?? new JsObject(), argScale ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 Decompose(JsType argPosition = null, JsType argQuaternion = null, JsType argScale = null)
    {
        CallMethodVoid("decompose", argPosition ?? new JsObject(), argQuaternion ?? new JsObject(), argScale ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakePerspective(JsType argLeft = null, JsType argRight = null, JsType argTop = null, JsType argBottom = null, JsType argNear = null, JsType argFar = null)
    {
        CallMethodVoid("makePerspective", argLeft ?? new JsObject(), argRight ?? new JsObject(), argTop ?? new JsObject(), argBottom ?? new JsObject(), argNear ?? new JsObject(), argFar ?? new JsObject());
            
        return this;
    }
        
    public JsMatrix4 MakeOrthographic(JsType argLeft = null, JsType argRight = null, JsType argTop = null, JsType argBottom = null, JsType argNear = null, JsType argFar = null)
    {
        CallMethodVoid("makeOrthographic", argLeft ?? new JsObject(), argRight ?? new JsObject(), argTop ?? new JsObject(), argBottom ?? new JsObject(), argNear ?? new JsObject(), argFar ?? new JsObject());
            
        return this;
    }
        
    public JsType Equals(JsType argMatrix = null)
    {
        return CallMethod("equals", argMatrix ?? new JsObject());
    }
        
    public JsMatrix4 FromArray(JsType argArray = null, JsNumber argOffset = null)
    {
        CallMethodVoid("fromArray", argArray ?? new JsObject(), argOffset ?? (0).AsJsNumber());
            
        return this;
    }
        
    public JsArray ToArray(JsArray argArray = null, JsNumber argOffset = null)
    {
        return CallMethod("toArray", argArray ?? new JsArray(), argOffset ?? (0).AsJsNumber());
    }
        
        
}