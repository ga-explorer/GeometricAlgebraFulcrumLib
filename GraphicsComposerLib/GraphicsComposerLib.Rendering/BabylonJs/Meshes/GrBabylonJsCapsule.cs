using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateCapsule-2
/// </summary>
public sealed class GrBabylonJsCapsule :
    GrBabylonJsMesh
{
    public sealed class CapsuleOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsInt32Value? BottomCapSubdivisions { get; set; }

        public GrBabylonJsInt32Value? TopCapSubdivisions { get; set; }

        public GrBabylonJsInt32Value? CapSubdivisions { get; set; }

        public GrBabylonJsInt32Value? Subdivisions { get; set; }

        public GrBabylonJsInt32Value? Tessellation { get; set; }

        public GrBabylonJsFloat32Value? Height { get; set; }

        public GrBabylonJsFloat32Value? Radius { get; set; }

        public GrBabylonJsFloat32Value? RadiusBottom { get; set; }

        public GrBabylonJsFloat32Value? RadiusTop { get; set; }

        public GrBabylonJsVector3Value? Orientation { get; set; }

        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return BottomCapSubdivisions.GetNameValueCodePair("bottomCapSubdivisions");
            yield return TopCapSubdivisions.GetNameValueCodePair("topCapSubdivisions");
            yield return CapSubdivisions.GetNameValueCodePair("capSubdivisions");
            yield return Subdivisions.GetNameValueCodePair("subdivisions");
            yield return Tessellation.GetNameValueCodePair("tessellation");
            yield return Height.GetNameValueCodePair("height");
            yield return Radius.GetNameValueCodePair("radius");
            yield return RadiusBottom.GetNameValueCodePair("radiusBottom");
            yield return RadiusTop.GetNameValueCodePair("radiusTop");
            yield return Orientation.GetNameValueCodePair("orientation");
            yield return Updateable.GetNameValueCodePair("updateable");
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateCapsule";

    public CapsuleOptions? Options { get; private set; }
        = new CapsuleOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsCapsule(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsCapsule(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsCapsule SetOptions([NotNull] CapsuleOptions? options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsCapsule SetProperties([NotNull] MeshProperties? properties)
    {
        Properties = properties;

        return this;
    }
}