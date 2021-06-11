namespace CodeComposerLib.HTMLold.Values
{
    public static class HtmlConstants
    {
        public enum Alignment { Min, Mid, Max }

        public enum ColorSpecs { None, CurrentColor, ColorValue }

        public enum FontSize
        {
            AbsoluteSmallXx,
            AbsoluteSmallX,
            AbsoluteSmall,
            AbsoluteMedium,
            AbsoluteLarge,
            AbsoluteLargeX,
            AbsoluteLargeXx,
            RelativeLarger,
            RelativeSmaller,
            Length
        }

        public static class GenericFontFamilies
        {
            public static string Serif { get; } = "serif";

            public static string SansSerif { get; } = "sans-serif";

            public static string Cursive { get; } = "cursive";

            public static string Fantasy { get; } = "fantasy";

            public static string Monospace { get; } = "monospace";
        }
    }
}