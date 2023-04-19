namespace GraphicsComposerLib.Rendering.SvgNew
{
    public abstract class GrSvgElement :
        IGrSvgCodeElement
    {
        public string Id { get; }

        public abstract string ElementName { get; }


        protected GrSvgElement(string id)
        {
            Id = id;
        }


        public abstract string GetCode();
    }
}