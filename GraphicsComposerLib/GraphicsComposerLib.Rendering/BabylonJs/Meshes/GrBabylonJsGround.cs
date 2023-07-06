using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateGround-2
    /// </summary>
    public sealed class GrBabylonJsGround :
        GrBabylonJsMesh
    {
        public sealed class GroundOptions :
            GrBabylonJsObjectOptions
        {
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

            public GrBabylonJsInt32Value? Subdivisions
            {
                get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisions");
                set => SetAttributeValue("subdivisions", value);
            }

            public GrBabylonJsInt32Value? SubdivisionsX
            {
                get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisionsX");
                set => SetAttributeValue("subdivisionsX", value);
            }

            public GrBabylonJsInt32Value? SubdivisionsY
            {
                get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("subdivisionsY");
                set => SetAttributeValue("subdivisionsY", value);
            }

            public GrBabylonJsBooleanValue? Updatable
            {
                get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("updatable");
                set => SetAttributeValue("updatable", value);
            }


            public GroundOptions()
            {
            }

            public GroundOptions(GroundOptions options)
            {
                SetAttributeValues(options);
            }
        }


        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateGround";

        public GroundOptions Options { get; private set; }
            = new GroundOptions();

        public override GrBabylonJsObjectOptions ObjectOptions 
            => Options;


        public GrBabylonJsGround(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsGround(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsGround SetOptions(GroundOptions options)
        {
            Options = new GroundOptions(options);

            return this;
        }

        public GrBabylonJsGround SetProperties(MeshProperties properties)
        {
            Properties = new MeshProperties(properties);

            return this;
        }
    }
}