namespace GraphicsComposerLib.POVRay.SDL.Values
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
