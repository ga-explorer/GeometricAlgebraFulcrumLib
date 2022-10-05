namespace GraphicsComposerLib.Rendering.Xeogl.Geometry
{
    public class XeoglCodeGeometry : XeoglGeometry
    {
        public static XeoglCodeGeometry Create(string code)
            => new XeoglCodeGeometry(code);


        public override string JavaScriptClassName => string.Empty;

        public string Code { get; set; }


        public XeoglCodeGeometry()
        {
            Code = string.Empty;
        }

        public XeoglCodeGeometry(string code)
        {
            Code = code ?? string.Empty;
        }


        public override string ToString()
        {
            return Code;
        }
    }
}