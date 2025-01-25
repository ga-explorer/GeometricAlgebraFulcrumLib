using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs;

public sealed class GrBabylonJsSceneProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsMaterialValue? DefaultMaterial
    {
        get => GetAttributeValueOrNull<GrBabylonJsMaterialValue>("defaultMaterial");
        set => SetAttributeValue("defaultMaterial", value);
    }

    public GrBabylonJsColor3Value? AmbientColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("ambientColor");
        set => SetAttributeValue("ambientColor", value);
    }

    public GrBabylonJsColor4Value? ClearColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("clearColor");
        set => SetAttributeValue("clearColor", value);
    }

    public GrBabylonJsBooleanValue? FogEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("fogEnabled");
        set => SetAttributeValue("fogEnabled", value);
    }

    public GrBabylonJsFogModeValue? FogMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsFogModeValue>("fogMode");
        set => SetAttributeValue("fogMode", value);
    }

    public GrBabylonJsColor3Value? FogColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("fogColor");
        set => SetAttributeValue("fogColor", value);
    }

    public GrBabylonJsFloat32Value? FogDensity
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fogDensity");
        set => SetAttributeValue("fogDensity", value);
    }

    public GrBabylonJsFloat32Value? FogStart
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fogStart");
        set => SetAttributeValue("fogStart", value);
    }

    public GrBabylonJsFloat32Value? FogEnd
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fogEnd");
        set => SetAttributeValue("fogEnd", value);
    }

    public GrBabylonJsFloat32Value? AnimationTimeScale
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("animationTimeScale");
        set => SetAttributeValue("animationTimeScale", value);
    }

    public GrBabylonJsBooleanValue? AnimationsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("animationsEnabled");
        set => SetAttributeValue("animationsEnabled", value);
    }

    public GrBabylonJsBooleanValue? AudioEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("audioEnabled");
        set => SetAttributeValue("audioEnabled", value);
    }

    public GrBabylonJsFloat32Value? AudioPositioningRefreshRate
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("audioPositioningRefreshRate");
        set => SetAttributeValue("audioPositioningRefreshRate", value);
    }

    public GrBabylonJsBooleanValue? Headphone
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("headphone");
        set => SetAttributeValue("headphone", value);
    }

    public GrBabylonJsFloat32Value? DeltaTime
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("deltaTime");
        set => SetAttributeValue("deltaTime", value);
    }

    public GrBabylonJsFloat32Value? MinDeltaTime
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("minDeltaTime");
        set => SetAttributeValue("minDeltaTime", value);
    }

    public GrBabylonJsFloat32Value? MaxDeltaTime
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("maxDeltaTime");
        set => SetAttributeValue("maxDeltaTime", value);
    }

    public GrBabylonJsBooleanValue? UseConstantAnimationDeltaTime
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useConstantAnimationDeltaTime");
        set => SetAttributeValue("useConstantAnimationDeltaTime", value);
    }

    public GrBabylonJsFloat32Value? PointerX
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pointerX");
        set => SetAttributeValue("pointerX", value);
    }

    public GrBabylonJsFloat32Value? PointerY
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("pointerY");
        set => SetAttributeValue("pointerY", value);
    }

    public GrBabylonJsBooleanValue? UseOrderIndependentTransparency
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useOrderIndependentTransparency");
        set => SetAttributeValue("useOrderIndependentTransparency", value);
    }

    public GrBabylonJsBooleanValue? AutoClear
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoClear");
        set => SetAttributeValue("autoClear", value);
    }

    public GrBabylonJsBooleanValue? AutoClearDepthAndStencil
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("autoClearDepthAndStencil");
        set => SetAttributeValue("autoClearDepthAndStencil", value);
    }

    public GrBabylonJsBooleanValue? PhysicsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("physicsEnabled");
        set => SetAttributeValue("physicsEnabled", value);
    }

    public GrBabylonJsBooleanValue? ParticlesEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("particlesEnabled");
        set => SetAttributeValue("particlesEnabled", value);
    }

    public GrBabylonJsBooleanValue? CollisionsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("collisionsEnabled");
        set => SetAttributeValue("collisionsEnabled", value);
    }

    public GrBabylonJsVector3Value? Gravity
    {
        get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("gravity");
        set => SetAttributeValue("gravity", value);
    }

    public GrBabylonJsBooleanValue? ConstantlyUpdateMeshUnderPointer
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("constantlyUpdateMeshUnderPointer");
        set => SetAttributeValue("constantlyUpdateMeshUnderPointer", value);
    }

    public GrBabylonJsBooleanValue? ForceShowBoundingBoxes
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("forceShowBoundingBoxes");
        set => SetAttributeValue("forceShowBoundingBoxes", value);
    }

    public GrBabylonJsBooleanValue? ForcePointsCloud
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("forcePointsCloud");
        set => SetAttributeValue("forcePointsCloud", value);
    }

    public GrBabylonJsBooleanValue? ForceWireFrame
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("forceWireFrame");
        set => SetAttributeValue("forceWireFrame", value);
    }

    public GrBabylonJsBooleanValue? LightsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("lightsEnabled");
        set => SetAttributeValue("lightsEnabled", value);
    }

    public GrBabylonJsBooleanValue? ShadowsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("shadowsEnabled");
        set => SetAttributeValue("shadowsEnabled", value);
    }

    public GrBabylonJsBooleanValue? SkeletonsEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("skeletonsEnabled");
        set => SetAttributeValue("skeletonsEnabled", value);
    }

    public GrBabylonJsBooleanValue? SkipFrustumClipping
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("skipFrustumClipping");
        set => SetAttributeValue("skipFrustumClipping", value);
    }

    public GrBabylonJsBooleanValue? TexturesEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("texturesEnabled");
        set => SetAttributeValue("texturesEnabled", value);
    }

    public GrBabylonJsBooleanValue? UseRightHandedSystem
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useRightHandedSystem");
        set => SetAttributeValue("useRightHandedSystem", value);
    }

    public GrBabylonJsBooleanValue? RequireLightSorting
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("requireLightSorting");
        set => SetAttributeValue("requireLightSorting", value);
    }

    public GrBabylonJsBooleanValue? SpritesEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("spritesEnabled");
        set => SetAttributeValue("spritesEnabled", value);
    }

            
    public GrBabylonJsSceneProperties()
    {
        UseRightHandedSystem = true;
    }
            
    public GrBabylonJsSceneProperties(GrBabylonJsSceneProperties properties)
    {
        UseRightHandedSystem = true;
        SetAttributeValues(properties);
    }
}