using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateLines-2
    /// </summary>
    public sealed class GrBabylonJsLines :
        GrBabylonJsLinesMesh
    {
        public sealed class LinesOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsVector3ArrayValue? Points { get; set; }

            public GrBabylonJsColor4ArrayValue? Colors { get; set; }

            public GrBabylonJsMaterialValue? Material { get; set; }

            public GrBabylonJsBooleanValue? UseVertexAlpha { get; set; }

            public GrBabylonJsBooleanValue? Updateable { get; set; }

            public GrBabylonJsLinesMeshValue? Instance { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Points.GetNameValueCodePair("points");
                yield return Colors.GetNameValueCodePair("colors");
                yield return Instance.GetNameValueCodePair("instance");
                yield return Material.GetNameValueCodePair("material");
                yield return UseVertexAlpha.GetNameValueCodePair("useVertexAlpha");
                yield return Updateable.GetNameValueCodePair("updatable");
            }
        }
    
        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateLines";

        public LinesOptions? Options { get; private set; }
            = new LinesOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => Options;


        public GrBabylonJsLines(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsLines(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsLines SetOptions(LinesOptions options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsLines SetProperties(LinesMeshProperties properties)
        {
            Properties = properties;

            return this;
        }
    }
}