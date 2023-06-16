namespace WebComposerLib.SvgNew
{
    public abstract class WclSvgElement :
        IWclSvgCodeElement
    {
        public string Id { get; }

        public abstract string ElementName { get; }


        protected WclSvgElement(string id)
        {
            Id = id;
        }


        public abstract string GetCode();
    }
}