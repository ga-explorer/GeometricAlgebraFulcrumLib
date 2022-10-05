using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Meshes;

/// <summary>
/// https://doc.babylonjs.com/typedoc/modules/BABYLON#CreateDashedLines-2
/// </summary>
public sealed class GrBabylonJsDashedLines :
    GrBabylonJsLinesMesh
{
    public sealed class DashedLinesOptions :
        GrBabylonJsObjectOptions
    {
        //instance?: Nullable<LinesMesh>;
    
        public GrBabylonJsVector3ArrayValue? Points { get; set; }
    
        public GrBabylonJsMaterialValue? Material { get; set; }

        public GrBabylonJsInt32Value? DashSize { get; set; }

        public GrBabylonJsInt32Value? GapSize { get; set; }

        public GrBabylonJsInt32Value? DashNumber { get; set; }

        public GrBabylonJsBooleanValue? UseVertexAlpha { get; set; }

        public GrBabylonJsBooleanValue? Updateable { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Points.GetNameValueCodePair("points");
            yield return Material.GetNameValueCodePair("material");
            yield return DashSize.GetNameValueCodePair("dashSize");
            yield return GapSize.GetNameValueCodePair("gapSize");
            yield return DashNumber.GetNameValueCodePair("dashNb");
            yield return UseVertexAlpha.GetNameValueCodePair("useVertexAlpha");
            yield return Updateable.GetNameValueCodePair("updateable");
        }
    }


    protected override string ConstructorName
        => "BABYLON.MeshBuilder.CreateDashedLines";

    public DashedLinesOptions? Options { get; private set; }
        = new DashedLinesOptions();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => Options;


    public GrBabylonJsDashedLines(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsDashedLines(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsDashedLines SetOptions([NotNull] DashedLinesOptions? options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsDashedLines SetProperties([NotNull] LinesMeshProperties? properties)
    {
        Properties = properties;

        return this;
    }

}