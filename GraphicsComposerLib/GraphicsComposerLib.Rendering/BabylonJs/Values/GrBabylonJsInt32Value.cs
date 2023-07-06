using DataStructuresLib.AttributeSet;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsInt32Value :
        SparseCodeAttributeValue<int>
    {
        public static implicit operator GrBabylonJsInt32Value(string valueText)
        {
            return new GrBabylonJsInt32Value(valueText);
        }

        public static implicit operator GrBabylonJsInt32Value(int value)
        {
            return new GrBabylonJsInt32Value(value);
        }
    
        public static implicit operator GrBabylonJsInt32Value(float value)
        {
            return new GrBabylonJsInt32Value((int) value);
        }
    
        public static implicit operator GrBabylonJsInt32Value(double value)
        {
            return new GrBabylonJsInt32Value((int) value);
        }


        private GrBabylonJsInt32Value(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsInt32Value(int value)
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