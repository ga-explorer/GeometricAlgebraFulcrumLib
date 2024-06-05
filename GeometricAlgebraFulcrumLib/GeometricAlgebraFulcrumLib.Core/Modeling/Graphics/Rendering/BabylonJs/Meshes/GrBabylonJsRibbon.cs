using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateRibbon-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/ribbon
/// </summary>
public sealed class GrBabylonJsRibbon :
    GrBabylonJsMesh
{
    public sealed class RibbonOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayArrayValue? PathArray
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayArrayValue>("pathArray");
            set => SetAttributeValue("pathArray", value);
        }

        public GrBabylonJsMeshValue? Instance
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshValue>("instance");
            set => SetAttributeValue("instance", value);
        }

        public GrBabylonJsInt32Value? Offset
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("offset");
            set => SetAttributeValue("offset", value);
        }

        public GrBabylonJsColor4ArrayValue? Colors
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("colors");
            set => SetAttributeValue("colors", value);
        }

        public GrBabylonJsBooleanValue? CloseArray
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closeArray");
            set => SetAttributeValue("closeArray", value);
        }

        public GrBabylonJsBooleanValue? ClosePath
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("closePath");
            set => SetAttributeValue("closePath", value);
        }

        public GrBabylonJsMeshOrientationValue? SideOrientation
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
            set => SetAttributeValue("sideOrientation", value);
        }

        public GrBabylonJsVector2Value? UVs
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("uvs");
            set => SetAttributeValue("uvs", value);
        }

        public GrBabylonJsVector4Value? FrontUVs
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector4Value>("frontUVs");
            set => SetAttributeValue("frontUVs", value);
        }

        public GrBabylonJsVector4Value? BackUVs
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector4Value>("backUVs");
            set => SetAttributeValue("backUVs", value);
        }

        public GrBabylonJsBooleanValue? InvertUv
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertUV");
            set => SetAttributeValue("invertUV", value);
        }

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }


        public RibbonOptions()
        {
        }

        public RibbonOptions(RibbonOptions options)
        {
            SetAttributeValues(options);
        }
    }

    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateRibbon";

    public RibbonOptions Options { get; private set; }
        = new RibbonOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsRibbon(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsRibbon(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsRibbon SetOptions(RibbonOptions options)
    {
        Options = new RibbonOptions(options);

        return this;
    }

    public GrBabylonJsRibbon SetProperties(MeshProperties properties)
    {
        Properties = new MeshProperties(properties);

        return this;
    }
}