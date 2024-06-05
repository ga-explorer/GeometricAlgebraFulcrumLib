using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateSphere-2
/// </summary>
public sealed class GrBabylonJsSphere :
    GrBabylonJsMesh
{
    public sealed class SphereOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsFloat32Value? Arc
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("arc");
            set => SetAttributeValue("arc", value);
        }

        public GrBabylonJsFloat32Value? Slice
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("slice");
            set => SetAttributeValue("slice", value);
        }

        public GrBabylonJsFloat32Value? Diameter
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameter");
            set => SetAttributeValue("diameter", value);
        }

        public GrBabylonJsFloat32Value? DiameterX
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameterX");
            set => SetAttributeValue("diameterX", value);
        }

        public GrBabylonJsFloat32Value? DiameterY
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameterY");
            set => SetAttributeValue("diameterY", value);
        }

        public GrBabylonJsFloat32Value? DiameterZ
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameterZ");
            set => SetAttributeValue("diameterZ", value);
        }

        public GrBabylonJsInt32Value? Segments
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("segments");
            set => SetAttributeValue("segments", value);
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


        public SphereOptions()
        {
        }

        public SphereOptions(SphereOptions options)
        {
            SetAttributeValues(options);
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateSphere";

    public SphereOptions Options { get; private set; }
        = new SphereOptions();

    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;


    public GrBabylonJsSphere(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsSphere(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsSphere SetOptions(SphereOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsSphere SetProperties(MeshProperties properties)
    {
        Properties = properties;

        return this;
    }
}