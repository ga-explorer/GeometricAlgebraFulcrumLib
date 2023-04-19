namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsSceneValue :
        GrBabylonJsValue<GrBabylonJsScene>
    {
        public static implicit operator GrBabylonJsSceneValue(string valueText)
        {
            return new GrBabylonJsSceneValue(valueText);
        }

        public static implicit operator GrBabylonJsSceneValue(GrBabylonJsScene value)
        {
            return new GrBabylonJsSceneValue(value);
        }


        private GrBabylonJsSceneValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsSceneValue(GrBabylonJsScene value)
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