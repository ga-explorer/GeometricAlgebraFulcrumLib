using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateTorus-2
/// </summary>
public sealed class GrBabylonJsTorus :
    GrBabylonJsMesh
{
    public sealed class TorusOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsFloat32Value? Diameter { get; set; }

        public GrBabylonJsFloat32Value? Thickness { get; set; }
    
        public GrBabylonJsInt32Value? Tessellation { get; set; }
    
        public GrBabylonJsMeshOrientationValue? SideOrientation { get; set; }

        public GrBabylonJsVector4Value? FrontUVs { get; set; }

        public GrBabylonJsVector4Value? BackUVs { get; set; }

        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Diameter.GetNameValueCodePair("diameter");
            yield return Thickness.GetNameValueCodePair("thickness");
            yield return Tessellation.GetNameValueCodePair("tessellation");
            yield return SideOrientation.GetNameValueCodePair("sideOrientation");
            yield return FrontUVs.GetNameValueCodePair("frontUVs");
            yield return BackUVs.GetNameValueCodePair("backUVs");
            yield return Updateable.GetNameValueCodePair("updateable");
        }
    }
    
    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateTorus";

    public TorusOptions? Options { get; private set; }
        = new TorusOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsTorus(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsTorus(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsTorus SetOptions([NotNull] TorusOptions? options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsTorus SetProperties([NotNull] MeshProperties? properties)
    {
        Properties = properties;

        return this;
    }
}