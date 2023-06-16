using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
{
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
            public GrBabylonJsVector3ArrayValue? Path { get; set; }

            public GrBabylonJsMeshValue? Instance { get; set; }

            public GrBabylonJsFloat32Value? Arc { get; set; }

            public GrBabylonJsFloat32Value? Radius { get; set; }

            public GrBabylonJsCodeValue? RadiusFunction { get; set; }
    
            public GrBabylonJsInt32Value? Tessellation { get; set; }
    
            public GrBabylonJsMeshCapValue? Cap { get; set; }

            public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

            public GrBabylonJsVector4Value? FrontUVs { get; set; }

            public GrBabylonJsVector4Value? BackUVs { get; set; }
    
            public GrBabylonJsBooleanValue? InvertUV { get; set; }
    
            public GrBabylonJsBooleanValue? Updateable { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Path.GetNameValueCodePair("path");
                yield return Arc.GetNameValueCodePair("arc");
                yield return Radius.GetNameValueCodePair("radius");
                yield return RadiusFunction.GetNameValueCodePair("radiusFunction");
                yield return Instance.GetNameValueCodePair("instance");
                yield return Tessellation.GetNameValueCodePair("tessellation");
                yield return Cap.GetNameValueCodePair("cap");
                yield return SideOrientation.GetNameValueCodePair("sideOrientation");
                yield return FrontUVs.GetNameValueCodePair("frontUVs");
                yield return BackUVs.GetNameValueCodePair("backUVs");
                yield return Updateable.GetNameValueCodePair("updatable");
                yield return InvertUV.GetNameValueCodePair("invertUV");
            }
        }
    
        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateTube";

        public TubeOptions? Options { get; private set; }
            = new TubeOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions 
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
            Options = options;

            return this;
        }

        public GrBabylonJsTube SetProperties(MeshProperties properties)
        {
            Properties = properties;

            return this;
        }
    }
}