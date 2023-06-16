using GraphicsComposerLib.Rendering.BabylonJs.Textures;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    public class GrBabylonJsGuiFullScreenUi :
        GrBabylonJsAdvancedDynamicTexture,
        IGrBabylonJsGuiControlContainer
    {
        protected override string ConstructorName 
            => "BABYLON.GUI.AdvancedDynamicTexture.CreateFullscreenUI";

        public GrBabylonJsBooleanValue IsForeground { get; set; }

        public GrBabylonJsBooleanValue AdaptiveScaling { get; set; }

        public GrBabylonJsGuiFullScreenUiValue ParentUi 
            => this;

        public GrBabylonJsGuiControlList ControlList { get; } 
            = new GrBabylonJsGuiControlList();


        public GrBabylonJsGuiFullScreenUi(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsGuiFullScreenUi(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();

            if (IsForeground.IsNullOrEmpty()) yield break;
            yield return IsForeground.GetCode();

            if (ParentScene.IsNullOrEmpty()) yield break;
            yield return ParentScene.GetCode();

            if (SamplingMode.IsNullOrEmpty()) yield break;
            yield return SamplingMode.GetCode();
        
            if (AdaptiveScaling.IsNullOrEmpty()) yield break;
            yield return AdaptiveScaling.GetCode();
        }


        public override string GetCode()
        {
            var composer = new LinearTextComposer();
        
            if (!string.IsNullOrEmpty(ConstName))
            {
                var declarationKeyword = UseLetDeclaration ? "let" : "const";

                composer.Append($"{declarationKeyword} {ConstName} = ");
            }

            var constructorCode = GetConstructorCode();
            var propertiesCode = GetPropertiesCode();

            composer
                .AppendLine(constructorCode)
                .AppendAtNewLine(propertiesCode);

            if (ControlList.Count > 0)
            {
                composer.AppendEmptyLines(1);

                foreach (var control in ControlList)
                    composer
                        .AppendAtNewLine(control.GetCode())
                        .AppendLineAtNewLine($"{ConstName}.addControl({control.ConstName});")
                        .AppendLine();
            }

            return composer.ToString();
        }
    }
}