using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes
{
    /// <summary>
    /// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateBox-2
    /// </summary>
    public sealed class GrBabylonJsBox :
        GrBabylonJsMesh
    {
        public sealed class BoxOptions :
            GrBabylonJsObjectOptions
        {
            public GrBabylonJsFloat32Value? BottomBaseAt { get; init; }

            public GrBabylonJsFloat32Value? TopBaseAt { get; init; }

            public GrBabylonJsFloat32Value? Width { get; init; }

            public GrBabylonJsFloat32Value? Height { get; init; }

            public GrBabylonJsFloat32Value? Depth { get; init; }

            public GrBabylonJsFloat32Value? Size { get; init; }

            public GrBabylonJsBooleanValue? Wrap { get; init; }

            public GrBabylonJsColor4ArrayValue? FaceColors { get; init; }

            public GrBabylonJsVector4ArrayValue? FaceUv { get; init; }

            public GrBabylonJsMeshOrientationValue? SideOrientation { get; init; }

            public GrBabylonJsVector4Value? FrontUVs { get; init; }

            public GrBabylonJsVector4Value? BackUVs { get; init; }

            public GrBabylonJsBooleanValue? Updateable { get; init; }


            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                yield return BottomBaseAt.GetNameValueCodePair("bottomBaseAt");
                yield return TopBaseAt.GetNameValueCodePair("topBaseAt");
                yield return Width.GetNameValueCodePair("width");
                yield return Height.GetNameValueCodePair("height");
                yield return Depth.GetNameValueCodePair("depth");
                yield return Size.GetNameValueCodePair("size");
                yield return Wrap.GetNameValueCodePair("wrap");
                yield return FaceColors.GetNameValueCodePair("faceColors");
                yield return FaceUv.GetNameValueCodePair("faceUV");
                yield return SideOrientation.GetNameValueCodePair("sideOrientation");
                yield return FrontUVs.GetNameValueCodePair("frontUVs");
                yield return BackUVs.GetNameValueCodePair("backUVs");
                yield return Updateable.GetNameValueCodePair("updateable");
            }
        }


        protected override string ConstructorName
            => "BABYLON.MeshBuilder.CreateBox";

        public BoxOptions? Options { get; private set; }
            = new BoxOptions();

        public override GrBabylonJsObjectOptions? ObjectOptions
            => Options;


        public GrBabylonJsBox(string constName)
            : base(constName)
        {
        }

        public GrBabylonJsBox(string constName, GrBabylonJsSceneValue scene)
            : base(constName, scene)
        {
        }


        public GrBabylonJsBox SetOptions([NotNull] BoxOptions? options)
        {
            Options = options;

            return this;
        }

        public GrBabylonJsBox SetProperties([NotNull] MeshProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}