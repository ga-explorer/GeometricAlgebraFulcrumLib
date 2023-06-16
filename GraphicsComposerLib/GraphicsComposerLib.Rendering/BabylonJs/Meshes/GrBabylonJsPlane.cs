using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
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
            public GrBabylonJsFloat32Value? Size { get; set; }

            public GrBabylonJsFloat32Value? Height { get; set; }

            public GrBabylonJsFloat32Value? Width { get; set; }
    
            public GrBabylonJsCodeValue? SourcePlane { get; set; }
    
            public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

            public GrBabylonJsVector4Value? FrontUVs { get; set; }

            public GrBabylonJsVector4Value? BackUVs { get; set; }

            public GrBabylonJsBooleanValue? Updateable { get; set; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Size.GetNameValueCodePair("size");
                yield return Height.GetNameValueCodePair("height");
                yield return Width.GetNameValueCodePair("width");
                yield return SideOrientation.GetNameValueCodePair("sideOrientation");
                yield return FrontUVs.GetNameValueCodePair("frontUVs");
                yield return BackUVs.GetNameValueCodePair("backUVs");
                yield return Updateable.GetNameValueCodePair("updatable");
                yield return SourcePlane.GetNameValueCodePair("sourcePlane");
            }
        }
    
        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreatePlane";

        public PlaneOptions? Options { get; private set; }
            = new PlaneOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions 
            => Options;


        public GrBabylonJsPlane(string constName) 
            : base(constName)
        {
        }
    
        public GrBabylonJsPlane(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        public GrBabylonJsPlane SetOptions([NotNull] PlaneOptions? options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsPlane SetProperties([NotNull] MeshProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}