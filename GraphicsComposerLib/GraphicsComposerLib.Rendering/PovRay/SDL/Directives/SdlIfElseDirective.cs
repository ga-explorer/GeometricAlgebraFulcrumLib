using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Directives
{
    public enum SdlIfElseDirectivePartKind
    {
        IfPart = 0, ElseIfPart = 1, ElsePart = 2
    }

    public sealed class SdlIfElseDirectivePart
    {
        public SdlIfElseDirectivePartKind Kind { get; private set; }

        public ISdlBooleanValue Condition { get; private set; }

        public List<ISdlStatement> Statements { get; private set; }


        internal SdlIfElseDirectivePart(SdlIfElseDirectivePartKind kind, ISdlBooleanValue condition)
        {
            Kind = kind;
            Condition = condition;
            Statements = new List<ISdlStatement>();
        }
    }

    public sealed class SdlIfElseDirective : SdlDirective
    {
        public List<SdlIfElseDirectivePart> Parts { get; private set; }


        internal SdlIfElseDirective()
        {
            Parts = new List<SdlIfElseDirectivePart>();
        }

    }
}
