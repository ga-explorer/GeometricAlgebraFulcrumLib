using DataStructuresLib.AttributeSet;
using GraphicsComposerLib.Rendering.BabylonJs.Constants;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsMeshOrientationValue :
        SparseCodeAttributeValue<GrBabylonJsMeshOrientation>
    {
        public static implicit operator GrBabylonJsMeshOrientationValue(string valueText)
        {
            return new GrBabylonJsMeshOrientationValue(valueText);
        }

        public static implicit operator GrBabylonJsMeshOrientationValue(GrBabylonJsMeshOrientation value)
        {
            return new GrBabylonJsMeshOrientationValue(value);
        }


        private GrBabylonJsMeshOrientationValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsMeshOrientationValue(GrBabylonJsMeshOrientation value)
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