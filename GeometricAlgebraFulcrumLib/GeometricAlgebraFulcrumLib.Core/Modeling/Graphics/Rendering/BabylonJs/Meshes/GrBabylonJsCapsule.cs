using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateCapsule-2
/// </summary>
public sealed class GrBabylonJsCapsule :
    GrBabylonJsMesh
{
    public sealed class CapsuleOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsInt32Value? BottomCapSubdivisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("bottomCapSubdivisions");
            set => SetAttributeValue("bottomCapSubdivisions", value);
        }

        public GrBabylonJsInt32Value? TopCapSubdivisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("topCapSubdivisions");
            set => SetAttributeValue("topCapSubdivisions", value);
        }

        public GrBabylonJsInt32Value? CapSubdivisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("capSubdivisions");
            set => SetAttributeValue("capSubdivisions", value);
        }

        public GrBabylonJsInt32Value? Subdivisions
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisions");
            set => SetAttributeValue("subdivisions", value);
        }

        public GrBabylonJsInt32Value? Tessellation
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("tessellation");
            set => SetAttributeValue("tessellation", value);
        }

        public GrBabylonJsFloat32Value? Height
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
            set => SetAttributeValue("height", value);
        }

        public GrBabylonJsFloat32Value? Radius
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radius");
            set => SetAttributeValue("radius", value);
        }

        public GrBabylonJsFloat32Value? RadiusBottom
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusBottom");
            set => SetAttributeValue("radiusBottom", value);
        }

        public GrBabylonJsFloat32Value? RadiusTop
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("radiusTop");
            set => SetAttributeValue("radiusTop", value);
        }

        public GrBabylonJsVector3Value? Orientation
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector3Value>("orientation");
            set => SetAttributeValue("orientation", value);
        }

        public GrBabylonJsBooleanValue? Updatable
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
            set => SetAttributeValue("updatable", value);
        }


        public CapsuleOptions()
        {
        }

        public CapsuleOptions(CapsuleOptions options)
        {
            SetAttributeValues(options);
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateCapsule";

    public CapsuleOptions Options { get; private set; }
        = new CapsuleOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsCapsule(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsCapsule(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsCapsule SetOptions(CapsuleOptions options)
    {
        Options = new CapsuleOptions(options);

        return this;
    }

    public GrBabylonJsCapsule SetProperties(MeshProperties properties)
    {
        Properties = new MeshProperties(properties);

        return this;
    }
}