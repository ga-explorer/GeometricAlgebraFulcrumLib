namespace GraphicsComposerLib.PovRay.SDL.Values
{
    public abstract class SdlStoredValue : SdlValue
    {
        public override string Value { get; }


        protected SdlStoredValue(string value)
        {
            Value = value;
        }
    }
}
