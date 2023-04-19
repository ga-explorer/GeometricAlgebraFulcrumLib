using GraphicsComposerLib.Rendering.BabylonJs.Materials;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsMaterialValue :
        GrBabylonJsValue<GrBabylonJsMaterial>
    {
        public static implicit operator GrBabylonJsMaterialValue(string valueText)
        {
            return new GrBabylonJsMaterialValue(valueText);
        }

        public static implicit operator GrBabylonJsMaterialValue(GrBabylonJsMaterial value)
        {
            return new GrBabylonJsMaterialValue(value);
        }


        private GrBabylonJsMaterialValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsMaterialValue(GrBabylonJsMaterial value)
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