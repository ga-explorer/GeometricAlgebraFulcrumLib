namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public sealed class GrBabylonJsSizeValue :
        GrBabylonJsValue
    {
        public static implicit operator GrBabylonJsSizeValue(string valueText)
        {
            return new GrBabylonJsSizeValue(valueText);
        }

        public static implicit operator GrBabylonJsSizeValue(int size)
        {
            return new GrBabylonJsSizeValue(size);
        }
    

        public static GrBabylonJsSizeValue Create(int size)
        {
            return new GrBabylonJsSizeValue(size);
        }

        public static GrBabylonJsSizeValue Create(int width, int height)
        {
            return new GrBabylonJsSizeValue(width, height);
        }

    
        public override bool IsEmpty 
            => false;

        public bool IsSquare 
            => Width == Height;

        public int Width { get; }

        public int Height { get; }


        private GrBabylonJsSizeValue(string valueText)
            : base(valueText)
        {
        }
    
        private GrBabylonJsSizeValue(int size)
            : base(string.Empty)
        {
            Width = size;
            Height = size;
        }

        private GrBabylonJsSizeValue(int width, int height)
            : base(string.Empty)
        {
            Width = width;
            Height = height;
        }


        public override string GetCode()
        {
            if (string.IsNullOrEmpty(ValueText))
                return IsSquare 
                    ? Width.GetBabylonJsCode() 
                    : $"{{ width: {Width.GetBabylonJsCode()}, height: {Height.GetBabylonJsCode()} }}";

            return ValueText;
        }
    }
}