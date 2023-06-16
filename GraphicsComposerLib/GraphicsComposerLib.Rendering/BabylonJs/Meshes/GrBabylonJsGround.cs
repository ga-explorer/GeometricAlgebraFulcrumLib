using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
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
            public GrBabylonJsFloat32Value? Height { get; set; }

            public GrBabylonJsFloat32Value? Width { get; set; }
    
            public GrBabylonJsInt32Value? Subdivisions { get; set; }
    
            public GrBabylonJsInt32Value? SubdivisionsX { get; set; }

            public GrBabylonJsInt32Value? SubdivisionsY { get; set; }

            public GrBabylonJsBooleanValue? Updateable { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Height.GetNameValueCodePair("height");
                yield return Width.GetNameValueCodePair("width");
                yield return Subdivisions.GetNameValueCodePair("subdivisions");
                yield return SubdivisionsX.GetNameValueCodePair("subdivisionsX");
                yield return SubdivisionsY.GetNameValueCodePair("subdivisionsY");
                yield return Updateable.GetNameValueCodePair("updatable");
            }
        }


        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateGround";

        public GroundOptions? Options { get; private set; }
            = new GroundOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => Options;


        public GrBabylonJsGround(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsGround(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsGround SetOptions([NotNull] GroundOptions? options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsGround SetProperties([NotNull] MeshProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}