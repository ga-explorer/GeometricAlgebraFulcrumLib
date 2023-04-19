using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsTextureCoordinatesModeValue :
        GrBabylonJsValue<GrBabylonJsTextureCoordinatesMode>
    {
        public static implicit operator GrBabylonJsTextureCoordinatesModeValue(string valueText)
        {
            return new GrBabylonJsTextureCoordinatesModeValue(valueText);
        }

        public static implicit operator GrBabylonJsTextureCoordinatesModeValue(GrBabylonJsTextureCoordinatesMode value)
        {
            return new GrBabylonJsTextureCoordinatesModeValue(value);
        }


        private GrBabylonJsTextureCoordinatesModeValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsTextureCoordinatesModeValue(GrBabylonJsTextureCoordinatesMode value)
            : base(value)
        {
        }


        public override string GetCode()
        {
            return string.IsNullOrEmpty(ValueText) 
                ? Value.GetBabylonJsCode() 
                : ValueText;
        }
    }
}