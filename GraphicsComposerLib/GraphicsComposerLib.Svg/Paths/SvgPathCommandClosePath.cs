namespace GraphicsComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandClosePath : SvgPathCommand
    {
        public static SvgPathCommandClosePath Default { get; } = new SvgPathCommandClosePath();


        public override string ValueText => "z";


        private SvgPathCommandClosePath()
        {
        }
    }
}
