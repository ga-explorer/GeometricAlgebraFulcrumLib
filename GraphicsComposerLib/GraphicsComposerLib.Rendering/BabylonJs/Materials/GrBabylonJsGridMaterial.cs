using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Materials;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GridMaterial
/// </summary>
public sealed class GrBabylonJsGridMaterial :
    GrBabylonJsMaterial
{
    public sealed class GridMaterialProperties :
        MaterialProperties
    {
        public GrBabylonJsVector3Value? GridOffset { get; set; }

        public GrBabylonJsFloat32Value? GridRatio { get; set; }

        public GrBabylonJsColor3Value? LineColor { get; set; }

        public GrBabylonJsColor3Value? MainColor { get; set; }

        public GrBabylonJsFloat32Value? MajorUnitFrequency { get; set; }

        public GrBabylonJsFloat32Value? MinorUnitVisibility { get; set; }

        public GrBabylonJsFloat32Value? Opacity { get; set; }

        public GrBabylonJsTextureValue? OpacityTexture { get; set; }

        public GrBabylonJsBooleanValue? PreMultiplyAlpha { get; set; }

        public GrBabylonJsBooleanValue? UseMaxLine { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return GridOffset.GetNameValueCodePair("gridOffset");
            yield return GridRatio.GetNameValueCodePair("gridRatio");
            yield return LineColor.GetNameValueCodePair("lineColor");
            yield return MainColor.GetNameValueCodePair("mainColor");
            yield return MajorUnitFrequency.GetNameValueCodePair("majorUnitFrequency");
            yield return MinorUnitVisibility.GetNameValueCodePair("minorUnitVisibility");
            yield return Opacity.GetNameValueCodePair("opacity");
            yield return OpacityTexture.GetNameValueCodePair("opacityTexture");
            yield return PreMultiplyAlpha.GetNameValueCodePair("preMultiplyAlpha");
            yield return UseMaxLine.GetNameValueCodePair("useMaxLine");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GridMaterial";

    public GridMaterialProperties? Properties { get; private set; }
        = new GridMaterialProperties();
    
    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsGridMaterial(string constName) 
        : base(constName)
    {
    }
    
    public GrBabylonJsGridMaterial(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }


    public GrBabylonJsGridMaterial SetProperties([NotNull] GridMaterialProperties? properties)
    {
        Properties = properties;

        return this;
    }
}