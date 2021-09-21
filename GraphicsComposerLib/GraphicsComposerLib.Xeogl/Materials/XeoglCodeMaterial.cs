using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Materials
{
    public class XeoglCodeMaterial : XeoglMaterial
    {
        public static XeoglCodeMaterial Create(string code) 
            => new XeoglCodeMaterial(code);


        public override string JavaScriptClassName => string.Empty;

        public override XeoglMaterialType MaterialType
            => XeoglMaterialType.Code;

        public string Code { get; set; }

        public XeoglCodeMaterial()
        {
        }

        public XeoglCodeMaterial(string code)
        {
            Code = code ?? string.Empty;
        }


        public override string ToString()
        {
            return Code;
        }
    }
}