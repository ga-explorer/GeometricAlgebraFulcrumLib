using System.Diagnostics.CodeAnalysis;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsDynamicTexture :
        GrBabylonJsBaseTexture
    {
        public class DynamicTextureProperties :
            BaseTextureProperties
        {
        
        }

        protected override string ConstructorName
            => "new BABYLON.DynamicTexture";

        public GrBabylonJsSizeValue Size { get; set; }
    
        public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }

        public GrBabylonJsTextureSamplingModeValue SamplingMode { get; set; }

        public GrBabylonJsTextureFormatValue Format { get; set; }

        public GrBabylonJsBooleanValue InvertY { get; set; }

        public DynamicTextureProperties? Properties { get; private set; }
            = new DynamicTextureProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsDynamicTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsDynamicTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsDynamicTexture SetProperties([NotNull] DynamicTextureProperties? properties)
        {
            Properties = properties;

            return this;
        }

        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();
            yield return Size.GetCode();

            if (ParentScene.IsNullOrEmpty()) yield break;
            yield return ParentScene.Value.ConstName;

            if (GenerateMipMaps.IsNullOrEmpty()) yield break;
            yield return GenerateMipMaps.GetCode();
        
            if (SamplingMode.IsNullOrEmpty()) yield break;
            yield return SamplingMode.GetCode();
        
            if (Format.IsNullOrEmpty()) yield break;
            yield return Format.GetCode();

            if (InvertY.IsNullOrEmpty()) yield break;
            yield return InvertY.GetCode();
        }
    }
}