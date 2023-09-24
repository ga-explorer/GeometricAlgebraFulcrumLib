using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Meshes
{
    public sealed class GrBabylonJsPlane :
        GrBabylonJsMesh
    {
        /// <summary>
        /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreatePlane-2
        /// </summary>
        public sealed class PlaneOptions :
            GrBabylonJsObjectOptions
        {
            //sourcePlane?: Plane; 
            public GrBabylonJsFloat32Value? Size
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("size");
                set => SetAttributeValue("size", value);
            }

            public GrBabylonJsFloat32Value? Height
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("height");
                set => SetAttributeValue("height", value);
            }

            public GrBabylonJsFloat32Value? Width
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("width");
                set => SetAttributeValue("width", value);
            }

            public GrBabylonJsCodeValue? SourcePlane
            {
                get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("sourcePlane");
                set => SetAttributeValue("sourcePlane", value);
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


            public PlaneOptions()
            {
            }

            public PlaneOptions(PlaneOptions options)
            {
                SetAttributeValues(options);
            }
        }
    
        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreatePlane";

        public PlaneOptions Options { get; private set; }
            = new PlaneOptions();

        public override GrBabylonJsObjectOptions ObjectOptions 
            => Options;


        public GrBabylonJsPlane(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsPlane(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsPlane SetOptions(PlaneOptions options)
        {
            Options = new PlaneOptions(options);

            return this;
        }

        public GrBabylonJsPlane SetProperties(MeshProperties properties)
        {
            Properties = new MeshProperties(properties);

            return this;
        }
    }
}