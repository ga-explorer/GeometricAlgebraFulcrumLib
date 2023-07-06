using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateIcoSphere-2
/// </summary>
public sealed class GrBabylonJsIcoSphere :
    GrBabylonJsMesh
{
    public sealed class IcoSphereOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsBooleanValue? Flat
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("flat");
            set => SetAttributeValue("flat", value);
        }

        public GrBabylonJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radius");
            set => SetAttributeValue("radius", value);
        }

        public GrBabylonJsFloat32Value? RadiusX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusX");
            set => SetAttributeValue("radiusX", value);
        }

        public GrBabylonJsFloat32Value? RadiusY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusY");
            set => SetAttributeValue("radiusY", value);
        }

        public GrBabylonJsFloat32Value? RadiusZ
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusZ");
            set => SetAttributeValue("radiusZ", value);
        }

        public GrBabylonJsInt32Value? Subdivisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisions");
            set => SetAttributeValue("subdivisions", value);
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

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }


        public IcoSphereOptions()
        {
        }

        public IcoSphereOptions(IcoSphereOptions options)
        {
            SetAttributeValues(options);
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateIcoSphere";

    public IcoSphereOptions Options { get; private set; }
        = new IcoSphereOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsIcoSphere(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsIcoSphere(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsIcoSphere SetOptions(IcoSphereOptions options)
    {
        Options = new IcoSphereOptions(options);

        return this;
    }

    public GrBabylonJsIcoSphere SetProperties(MeshProperties properties)
    {
        Properties = new MeshProperties(properties);

        return this;
    }
}