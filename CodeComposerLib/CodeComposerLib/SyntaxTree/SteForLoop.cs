namespace CodeComposerLib.SyntaxTree;

public class SteForLoop : SteSyntaxElement
{
    public ISyntaxTreeElement LoopInitialization { get; set; }

    public ISyntaxTreeElement LoopCondition { get; set; }

    public ISyntaxTreeElement LoopUpdate { get; set; }

    public ISyntaxTreeElement LoopCode { get; set; }


}