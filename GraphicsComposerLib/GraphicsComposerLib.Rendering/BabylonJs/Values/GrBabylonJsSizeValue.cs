namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsSizeValue :
        GrBabylonJsValue<SizeF>
    {
        internal static GrBabylonJsSizeValue Create(SizeF value)
        {
            return new GrBabylonJsSizeValue(value);
        }


        public static implicit operator GrBabylonJsSizeValue(string valueText)
        {
            return new GrBabylonJsSizeValue(valueText);
        }

        public static implicit operator GrBabylonJsSizeValue(SizeF value)
        {
            return new GrBabylonJsSizeValue(value);
        }

        
        public bool IsSquare 
            => !IsEmpty && Value.Width == Value.Height;


        private GrBabylonJsSizeValue(string valueText)
            : base(valueText)
        {
        }

        private GrBabylonJsSizeValue(SizeF value)
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