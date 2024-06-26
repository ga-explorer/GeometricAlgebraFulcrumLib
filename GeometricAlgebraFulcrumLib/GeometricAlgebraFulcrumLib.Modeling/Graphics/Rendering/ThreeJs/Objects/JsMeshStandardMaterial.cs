using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsMeshStandardMaterialConstructor :
    JsTypeConstructor
{
    public JsType Parameters { get; }
        
        


    internal JsMeshStandardMaterialConstructor(JsType argParameters)
    {
        Parameters = argParameters ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.MeshStandardMaterial({Parameters.GetJsCode()})";
    }
}
    
public partial class JsMeshStandardMaterial :
    JsMaterial
{
    public static implicit operator JsMeshStandardMaterial(string jsTextCode)
    {
        return new JsMeshStandardMaterial(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsMeshStandardMaterial value)
    {
        return value.GetJsCode();
    }


    private readonly JsMeshStandardMaterial _jsVariableValue;
    public JsMeshStandardMaterial JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;

    private readonly JsType _defines;
    public JsType Defines
    {
        get => _defines ?? throw new InvalidOperationException();
        set
        {
            if (_defines is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.defines = {valueCode};");
        }
    }
        
    private readonly JsString _type;
    public JsString Type
    {
        get => _type ?? throw new InvalidOperationException();
        set
        {
            if (_type is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "\"MeshStandardMaterial\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.type = {valueCode};");
        }
    }
        
    private readonly JsType _color;
    public JsType Color
    {
        get => _color ?? throw new InvalidOperationException();
        set
        {
            if (_color is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.color = {valueCode};");
        }
    }
        
    private readonly JsNumber _roughness;
    public JsNumber Roughness
    {
        get => _roughness ?? throw new InvalidOperationException();
        set
        {
            if (_roughness is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.roughness = {valueCode};");
        }
    }
        
    private readonly JsNumber _metalness;
    public JsNumber Metalness
    {
        get => _metalness ?? throw new InvalidOperationException();
        set
        {
            if (_metalness is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.metalness = {valueCode};");
        }
    }
        
    private readonly JsType _map;
    public JsType Map
    {
        get => _map ?? throw new InvalidOperationException();
        set
        {
            if (_map is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.map = {valueCode};");
        }
    }
        
    private readonly JsType _lightMap;
    public JsType LightMap
    {
        get => _lightMap ?? throw new InvalidOperationException();
        set
        {
            if (_lightMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.lightMap = {valueCode};");
        }
    }
        
    private readonly JsNumber _lightMapIntensity;
    public JsNumber LightMapIntensity
    {
        get => _lightMapIntensity ?? throw new InvalidOperationException();
        set
        {
            if (_lightMapIntensity is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.lightMapIntensity = {valueCode};");
        }
    }
        
    private readonly JsType _aoMap;
    public JsType AoMap
    {
        get => _aoMap ?? throw new InvalidOperationException();
        set
        {
            if (_aoMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.aoMap = {valueCode};");
        }
    }
        
    private readonly JsNumber _aoMapIntensity;
    public JsNumber AoMapIntensity
    {
        get => _aoMapIntensity ?? throw new InvalidOperationException();
        set
        {
            if (_aoMapIntensity is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.aoMapIntensity = {valueCode};");
        }
    }
        
    private readonly JsType _emissive;
    public JsType Emissive
    {
        get => _emissive ?? throw new InvalidOperationException();
        set
        {
            if (_emissive is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.emissive = {valueCode};");
        }
    }
        
    private readonly JsNumber _emissiveIntensity;
    public JsNumber EmissiveIntensity
    {
        get => _emissiveIntensity ?? throw new InvalidOperationException();
        set
        {
            if (_emissiveIntensity is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.emissiveIntensity = {valueCode};");
        }
    }
        
    private readonly JsType _emissiveMap;
    public JsType EmissiveMap
    {
        get => _emissiveMap ?? throw new InvalidOperationException();
        set
        {
            if (_emissiveMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.emissiveMap = {valueCode};");
        }
    }
        
    private readonly JsType _bumpMap;
    public JsType BumpMap
    {
        get => _bumpMap ?? throw new InvalidOperationException();
        set
        {
            if (_bumpMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.bumpMap = {valueCode};");
        }
    }
        
    private readonly JsNumber _bumpScale;
    public JsNumber BumpScale
    {
        get => _bumpScale ?? throw new InvalidOperationException();
        set
        {
            if (_bumpScale is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.bumpScale = {valueCode};");
        }
    }
        
    private readonly JsType _normalMap;
    public JsType NormalMap
    {
        get => _normalMap ?? throw new InvalidOperationException();
        set
        {
            if (_normalMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.normalMap = {valueCode};");
        }
    }
        
    private readonly JsNumber _normalMapType;
    public JsNumber NormalMapType
    {
        get => _normalMapType ?? throw new InvalidOperationException();
        set
        {
            if (_normalMapType is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.normalMapType = {valueCode};");
        }
    }
        
    private readonly JsType _normalScale;
    public JsType NormalScale
    {
        get => _normalScale ?? throw new InvalidOperationException();
        set
        {
            if (_normalScale is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.normalScale = {valueCode};");
        }
    }
        
    private readonly JsType _displacementMap;
    public JsType DisplacementMap
    {
        get => _displacementMap ?? throw new InvalidOperationException();
        set
        {
            if (_displacementMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.displacementMap = {valueCode};");
        }
    }
        
    private readonly JsNumber _displacementScale;
    public JsNumber DisplacementScale
    {
        get => _displacementScale ?? throw new InvalidOperationException();
        set
        {
            if (_displacementScale is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.displacementScale = {valueCode};");
        }
    }
        
    private readonly JsNumber _displacementBias;
    public JsNumber DisplacementBias
    {
        get => _displacementBias ?? throw new InvalidOperationException();
        set
        {
            if (_displacementBias is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.displacementBias = {valueCode};");
        }
    }
        
    private readonly JsType _roughnessMap;
    public JsType RoughnessMap
    {
        get => _roughnessMap ?? throw new InvalidOperationException();
        set
        {
            if (_roughnessMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.roughnessMap = {valueCode};");
        }
    }
        
    private readonly JsType _metalnessMap;
    public JsType MetalnessMap
    {
        get => _metalnessMap ?? throw new InvalidOperationException();
        set
        {
            if (_metalnessMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.metalnessMap = {valueCode};");
        }
    }
        
    private readonly JsType _alphaMap;
    public JsType AlphaMap
    {
        get => _alphaMap ?? throw new InvalidOperationException();
        set
        {
            if (_alphaMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.alphaMap = {valueCode};");
        }
    }
        
    private readonly JsType _envMap;
    public JsType EnvMap
    {
        get => _envMap ?? throw new InvalidOperationException();
        set
        {
            if (_envMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.envMap = {valueCode};");
        }
    }
        
    private readonly JsNumber _envMapIntensity;
    public JsNumber EnvMapIntensity
    {
        get => _envMapIntensity ?? throw new InvalidOperationException();
        set
        {
            if (_envMapIntensity is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.envMapIntensity = {valueCode};");
        }
    }
        
    private readonly JsNumber _refractionRatio;
    public JsNumber RefractionRatio
    {
        get => _refractionRatio ?? throw new InvalidOperationException();
        set
        {
            if (_refractionRatio is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0.98";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.refractionRatio = {valueCode};");
        }
    }
        
    private readonly JsBoolean _wireframe;
    public JsBoolean Wireframe
    {
        get => _wireframe ?? throw new InvalidOperationException();
        set
        {
            if (_wireframe is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "false";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.wireframe = {valueCode};");
        }
    }
        
    private readonly JsNumber _wireframeLinewidth;
    public JsNumber WireframeLinewidth
    {
        get => _wireframeLinewidth ?? throw new InvalidOperationException();
        set
        {
            if (_wireframeLinewidth is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.wireframeLinewidth = {valueCode};");
        }
    }
        
    private readonly JsString _wireframeLinecap;
    public JsString WireframeLinecap
    {
        get => _wireframeLinecap ?? throw new InvalidOperationException();
        set
        {
            if (_wireframeLinecap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "\"round\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.wireframeLinecap = {valueCode};");
        }
    }
        
    private readonly JsString _wireframeLinejoin;
    public JsString WireframeLinejoin
    {
        get => _wireframeLinejoin ?? throw new InvalidOperationException();
        set
        {
            if (_wireframeLinejoin is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "\"round\"";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.wireframeLinejoin = {valueCode};");
        }
    }
        
    private readonly JsBoolean _flatShading;
    public JsBoolean FlatShading
    {
        get => _flatShading ?? throw new InvalidOperationException();
        set
        {
            if (_flatShading is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "false";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.flatShading = {valueCode};");
        }
    }

    internal JsMeshStandardMaterial(JsTypeConstructor jsCodeSource, JsMeshStandardMaterial jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _defines = $"{variableName}.defines".AsJsTypeVariable();
        _type = $"{variableName}.type".AsJsStringVariable();
        _color = $"{variableName}.color".AsJsTypeVariable();
        _roughness = $"{variableName}.roughness".AsJsNumberVariable();
        _metalness = $"{variableName}.metalness".AsJsNumberVariable();
        _map = $"{variableName}.map".AsJsTypeVariable();
        _lightMap = $"{variableName}.lightMap".AsJsTypeVariable();
        _lightMapIntensity = $"{variableName}.lightMapIntensity".AsJsNumberVariable();
        _aoMap = $"{variableName}.aoMap".AsJsTypeVariable();
        _aoMapIntensity = $"{variableName}.aoMapIntensity".AsJsNumberVariable();
        _emissive = $"{variableName}.emissive".AsJsTypeVariable();
        _emissiveIntensity = $"{variableName}.emissiveIntensity".AsJsNumberVariable();
        _emissiveMap = $"{variableName}.emissiveMap".AsJsTypeVariable();
        _bumpMap = $"{variableName}.bumpMap".AsJsTypeVariable();
        _bumpScale = $"{variableName}.bumpScale".AsJsNumberVariable();
        _normalMap = $"{variableName}.normalMap".AsJsTypeVariable();
        _normalMapType = $"{variableName}.normalMapType".AsJsNumberVariable();
        _normalScale = $"{variableName}.normalScale".AsJsTypeVariable();
        _displacementMap = $"{variableName}.displacementMap".AsJsTypeVariable();
        _displacementScale = $"{variableName}.displacementScale".AsJsNumberVariable();
        _displacementBias = $"{variableName}.displacementBias".AsJsNumberVariable();
        _roughnessMap = $"{variableName}.roughnessMap".AsJsTypeVariable();
        _metalnessMap = $"{variableName}.metalnessMap".AsJsTypeVariable();
        _alphaMap = $"{variableName}.alphaMap".AsJsTypeVariable();
        _envMap = $"{variableName}.envMap".AsJsTypeVariable();
        _envMapIntensity = $"{variableName}.envMapIntensity".AsJsNumberVariable();
        _refractionRatio = $"{variableName}.refractionRatio".AsJsNumberVariable();
        _wireframe = $"{variableName}.wireframe".AsJsBooleanVariable();
        _wireframeLinewidth = $"{variableName}.wireframeLinewidth".AsJsNumberVariable();
        _wireframeLinecap = $"{variableName}.wireframeLinecap".AsJsStringVariable();
        _wireframeLinejoin = $"{variableName}.wireframeLinejoin".AsJsStringVariable();
        _flatShading = $"{variableName}.flatShading".AsJsBooleanVariable();
    }

    public JsMeshStandardMaterial(JsType argParameters = null)
        : base(new JsMeshStandardMaterialConstructor(argParameters))
    {
    }

    public JsMeshStandardMaterial Copy(JsType argSource = null)
    {
        CallMethodVoid("copy", argSource ?? new JsObject());
            
        return this;
    }
        
        
}