namespace GraphicsComposerLib.Rendering.PovRay.SDL.Modifiers
{
    public enum SdlSimpleModifierKind
    {
        NoShadow = 0,
        NoImage = 1,
        NoRadiosity = 2,
        NoReflection = 3,
        Inverse = 4,
        Hierarchy = 5,
        DoubleIlluminate = 6,
        Hollow = 7
    }

    public sealed class SdlSimpleModifier : ISdlObjectModifier
    {
        private static readonly string[] KindNames = new[]
        {
            "no_shadow", "no_image", "no_radiosity", "no_reflection", 
            "inverse", "hierarchy", "double_illuminate", "hollow"
        };


        public SdlSimpleModifierKind Kind { get; set; }

        public bool State { get; set; }

        public string Name => KindNames[(int)Kind];


        internal SdlSimpleModifier(SdlSimpleModifierKind kind, bool state)
        {
            State = state;
            Kind = kind;
        }


        public override string ToString()
        {
            if (Kind == SdlSimpleModifierKind.Inverse || Kind == SdlSimpleModifierKind.NoShadow)
                return Name;

            return Name + " " + (State ? "on" : "off");
        }
    }
}
