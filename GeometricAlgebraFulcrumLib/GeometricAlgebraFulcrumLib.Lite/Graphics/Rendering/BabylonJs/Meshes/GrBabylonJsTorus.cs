using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Meshes
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateTorus-2
    /// https://doc.babylonjs.com/features/featuresDeepDive/mesh/creation/set/torus
    /// </summary>
    public sealed class GrBabylonJsTorus :
        GrBabylonJsMesh
    {
        public sealed class TorusOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsFloat32Value? Diameter
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("diameter");
                set => SetAttributeValue("diameter", value);
            }

            public GrBabylonJsFloat32Value? Thickness
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("thickness");
                set => SetAttributeValue("thickness", value);
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


            public TorusOptions()
            {
            }

            public TorusOptions(TorusOptions options)
            {
                SetAttributeValues(options);
            }
        }
    
        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateTorus";

        public TorusOptions Options { get; private set; }
            = new TorusOptions();

        public override GrBabylonJsObjectOptions ObjectOptions 
            => Options;


        public GrBabylonJsTorus(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsTorus(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsTorus SetOptions(TorusOptions options)
        {
            Options = new TorusOptions(options);

            return this;
        }

        public GrBabylonJsTorus SetProperties(MeshProperties properties)
        {
            Properties = new MeshProperties(properties);

            return this;
        }
    }
}