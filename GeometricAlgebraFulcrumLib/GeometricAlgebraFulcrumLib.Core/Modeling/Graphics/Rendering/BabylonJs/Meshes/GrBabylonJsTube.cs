using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateTube-2
/// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/param/tube
/// </summary>
public sealed class GrBabylonJsTube :
    GrBabylonJsMesh
{
    public sealed class TubeOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsVector3ArrayValue? Path
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3ArrayValue>("path");
            set => SetAttributeValue("path", value);
        }

        public GrBabylonJsMeshValue? Instance
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshValue>("instance");
            set => SetAttributeValue("instance", value);
        }

        public GrBabylonJsFloat32Value? Arc
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("arc");
            set => SetAttributeValue("arc", value);
        }

        public GrBabylonJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radius");
            set => SetAttributeValue("radius", value);
        }

        public GrBabylonJsCodeValue? RadiusFunction
        {
            get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("radiusFunction");
            set => SetAttributeValue("radiusFunction", value);
        }

        public GrBabylonJsInt32Value? Tessellation
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("tessellation");
            set => SetAttributeValue("tessellation", value);
        }

        public GrBabylonJsMeshCapValue? Cap
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshCapValue>("cap");
            set => SetAttributeValue("cap", value);
        }

        public GrBabylonJsMeshOrientationValue? SideOrientation
        {
            get => GetAttributeValueOrNull<GrBabylonJsMeshOrientationValue>("sideOrientation");
            set => SetAttributeValue("sideOrientation", value);
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


        public TubeOptions()
        {
        }

        public TubeOptions(TubeOptions options)
        {
            SetAttributeValues(options);
        }
    }

    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateTube";

    public TubeOptions Options { get; private set; }
        = new TubeOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsTube(string constName)
        : base(constName)
    {
        UseLetDeclaration = true;
    }

    public GrBabylonJsTube(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
        UseLetDeclaration = true;
    }


    public GrBabylonJsTube SetOptions(TubeOptions options)
    {
        Options = new TubeOptions(options);

        return this;
    }

    public GrBabylonJsTube SetProperties(MeshProperties properties)
    {
        Properties = new MeshProperties(properties);

        return this;
    }
}