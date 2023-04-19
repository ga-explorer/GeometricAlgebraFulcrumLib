using GraphicsComposerLib.Rendering.BabylonJs.GUI;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsContainerValue :
        GrBabylonJsValue<GrBabylonJsGuiContainer>
    {
        public static implicit operator GrBabylonJsContainerValue(string valueText)
        {
            return new GrBabylonJsContainerValue(valueText);
        }

        public static implicit operator GrBabylonJsContainerValue(GrBabylonJsGuiContainer value)
        {
            return new GrBabylonJsContainerValue(value);
        }


        private GrBabylonJsContainerValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsContainerValue(GrBabylonJsGuiContainer value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.ToString() 
                : ValueText;
        }
    }
}