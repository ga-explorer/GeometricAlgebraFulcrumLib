namespace GraphicsComposerLib.PovRay.SDL.Values
{
    public sealed class SdlBooleanLiteral : SdlValue, ISdlBooleanValue
    {
        public static SdlBooleanLiteral True { get; private set; }

        public static SdlBooleanLiteral False { get; private set; }

        static SdlBooleanLiteral()
        {
            True = new SdlBooleanLiteral(true);

            False = new SdlBooleanLiteral(false);
        }


        public bool BooleanValue { get; }

        public override string Value => BooleanValue ? "on" : "off";


        private SdlBooleanLiteral(bool value)
        {
            BooleanValue = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
