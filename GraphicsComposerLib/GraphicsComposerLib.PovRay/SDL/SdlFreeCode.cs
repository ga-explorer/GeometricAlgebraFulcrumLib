namespace GraphicsComposerLib.PovRay.SDL
{
    public class SdlFreeCode : ISdlStatement
    {
        public string Code { get; set; }


        public override string ToString()
        {
            return Code;
        }
    }
}
