using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateSphere-2
    /// </summary>
    public sealed class GrBabylonJsSphere :
        GrBabylonJsMesh
    {
        public sealed class SphereOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsFloat32Value? Arc { get; init; }

            public GrBabylonJsFloat32Value? Slice { get; init; }

            public GrBabylonJsFloat32Value? Diameter { get; init; }

            public GrBabylonJsFloat32Value? DiameterX { get; init; }

            public GrBabylonJsFloat32Value? DiameterY { get; init; }
    
            public GrBabylonJsFloat32Value? DiameterZ { get; init; }
    
            public GrBabylonJsInt32Value? Segments { get; init; }
    
            public GrBabylonJsMeshOrientationValue? SideOrientation { get; init; }

            public GrBabylonJsVector4Value? FrontUVs { get; init; }

            public GrBabylonJsVector4Value? BackUVs { get; init; }

            public GrBabylonJsBooleanValue? Updateable { get; init; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return Arc.GetNameValueCodePair("arc");
                yield return Slice.GetNameValueCodePair("slice");
                yield return Diameter.GetNameValueCodePair("diameter");
                yield return DiameterX.GetNameValueCodePair("diameterX");
                yield return DiameterY.GetNameValueCodePair("diameterY");
                yield return DiameterZ.GetNameValueCodePair("diameterZ");
                yield return Segments.GetNameValueCodePair("segments");
                yield return SideOrientation.GetNameValueCodePair("sideOrientation");
                yield return FrontUVs.GetNameValueCodePair("frontUVs");
                yield return BackUVs.GetNameValueCodePair("backUVs");
                yield return Updateable.GetNameValueCodePair("updatable");
            }
        }
    
    
        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateSphere";

        public SphereOptions? Options { get; private set; }
            = new SphereOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions 
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
}