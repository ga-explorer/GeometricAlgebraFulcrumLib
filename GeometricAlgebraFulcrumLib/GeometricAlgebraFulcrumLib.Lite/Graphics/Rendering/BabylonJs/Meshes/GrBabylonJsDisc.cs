using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Meshes
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateDisc-2
    /// </summary>
    public sealed class GrBabylonJsDisc :
        GrBabylonJsMesh
    {
        public sealed class DiscOptions :
            GrBabylonJsObjectOptions
        {
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

            public GrBabylonJsInt32Value? Tessellation
            {
                get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("tessellation");
                set => SetAttributeValue("tessellation", value);
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


            public DiscOptions()
            {
            }

            public DiscOptions(DiscOptions options)
            {
                SetAttributeValues(options);
            }
        }


        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateDisc";

        public DiscOptions Options { get; private set; }
            = new DiscOptions();

        public override GrBabylonJsObjectOptions ObjectOptions 
            => Options;


        public GrBabylonJsDisc(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsDisc(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsDisc SetOptions(DiscOptions options)
        {
            Options = new DiscOptions(options);

            return this;
        }

        public GrBabylonJsDisc SetProperties(MeshProperties properties)
        {
            Properties = new MeshProperties(properties);

            return this;
        }
    }
}