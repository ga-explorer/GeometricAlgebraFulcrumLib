using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Objects;

internal sealed partial class JsMeshPhongMaterialConstructor :
    JsTypeConstructor
{
    public JsType Parameters { get; }
        
        


    internal JsMeshPhongMaterialConstructor(JsType argParameters)
    {
        Parameters = argParameters ?? new JsObject();
    }

    public override string GetJsCode()
    {
        return $"new THREE.MeshPhongMaterial({Parameters.GetJsCode()})";
    }
}
    
public partial class JsMeshPhongMaterial :
    JsMaterial
{
    public static implicit operator JsMeshPhongMaterial(string jsTextCode)
    {
        return new JsMeshPhongMaterial(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsMeshPhongMaterial value)
    {
        return value.GetJsCode();
    }


    private readonly JsMeshPhongMaterial _jsVariableValue;
    public JsMeshPhongMaterial JsValue 
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
        
            var valueCode = value?.GetJsCode() ?? "\"MeshPhongMaterial\"";
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
        
    private readonly JsType _specular;
    public JsType Specular
    {
        get => _specular ?? throw new InvalidOperationException();
        set
        {
            if (_specular is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.specular = {valueCode};");
        }
    }
        
    private readonly JsNumber _shininess;
    public JsNumber Shininess
    {
        get => _shininess ?? throw new InvalidOperationException();
        set
        {
            if (_shininess is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "30";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.shininess = {valueCode};");
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
        
    private readonly JsType _specularMap;
    public JsType SpecularMap
    {
        get => _specularMap ?? throw new InvalidOperationException();
        set
        {
            if (_specularMap is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "{}";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.specularMap = {valueCode};");
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
        
    private readonly JsNumber _combine;
    public JsNumber Combine
    {
        get => _combine ?? throw new InvalidOperationException();
        set
        {
            if (_combine is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "0";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.combine = {valueCode};");
        }
    }
        
    private readonly JsNumber _reflectivity;
    public JsNumber Reflectivity
    {
        get => _reflectivity ?? throw new InvalidOperationException();
        set
        {
            if (_reflectivity is null)
                throw new InvalidOperationException();
        
            var valueCode = value?.GetJsCode() ?? "1";
            JavaScriptCodeComposer.DefaultComposer.CodeLine($"{VariableName}.reflectivity = {valueCode};");
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

    internal JsMeshPhongMaterial(JsTypeConstructor jsCodeSource, JsMeshPhongMaterial jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
        var variableName = TypeConstructor.GetJsCode();

        _type = $"{variableName}.type".AsJsStringVariable();
        _color = $"{variableName}.color".AsJsTypeVariable();
        _specular = $"{variableName}.specular".AsJsTypeVariable();
        _shininess = $"{variableName}.shininess".AsJsNumberVariable();
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
        _specularMap = $"{variableName}.specularMap".AsJsTypeVariable();
        _alphaMap = $"{variableName}.alphaMap".AsJsTypeVariable();
        _envMap = $"{variableName}.envMap".AsJsTypeVariable();
        _combine = $"{variableName}.combine".AsJsNumberVariable();
        _reflectivity = $"{variableName}.reflectivity".AsJsNumberVariable();
        _refractionRatio = $"{variableName}.refractionRatio".AsJsNumberVariable();
        _wireframe = $"{variableName}.wireframe".AsJsBooleanVariable();
        _wireframeLinewidth = $"{variableName}.wireframeLinewidth".AsJsNumberVariable();
        _wireframeLinecap = $"{variableName}.wireframeLinecap".AsJsStringVariable();
        _wireframeLinejoin = $"{variableName}.wireframeLinejoin".AsJsStringVariable();
        _flatShading = $"{variableName}.flatShading".AsJsBooleanVariable();
    }

    public JsMeshPhongMaterial(JsType argParameters = null)
        : base(new JsMeshPhongMaterialConstructor(argParameters))
    {
    }

    public JsMeshPhongMaterial Copy(JsType argSource = null)
    {
        CallMethodVoid("copy", argSource ?? new JsObject());
            
        return this;
    }
        
        
}