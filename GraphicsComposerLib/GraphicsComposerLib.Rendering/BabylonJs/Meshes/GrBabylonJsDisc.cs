using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
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
            public GrBabylonJsFloat32Value? Arc { get; set; }

            public GrBabylonJsFloat32Value? Radius { get; set; }
    
            public GrBabylonJsInt32Value? Tessellation { get; set; }
    
            public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

            public GrBabylonJsVector4Value? FrontUVs { get; set; }

            public GrBabylonJsVector4Value? BackUVs { get; set; }

            public GrBabylonJsBooleanValue? Updateable { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Arc.GetNameValueCodePair("arc");
                yield return Radius.GetNameValueCodePair("radius");
                yield return Tessellation.GetNameValueCodePair("segments");
                yield return SideOrientation.GetNameValueCodePair("sideOrientation");
                yield return FrontUVs.GetNameValueCodePair("frontUVs");
                yield return BackUVs.GetNameValueCodePair("backUVs");
                yield return Updateable.GetNameValueCodePair("updateable");
            }
        }


        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateDisc";

        public DiscOptions? Options { get; private set; }
            = new DiscOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => Options;


        public GrBabylonJsDisc(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsDisc(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsDisc SetOptions([NotNull] DiscOptions? options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsDisc SetProperties([NotNull] MeshProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}