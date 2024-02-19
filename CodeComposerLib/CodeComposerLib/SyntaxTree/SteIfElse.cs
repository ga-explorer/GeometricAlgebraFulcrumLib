namespace CodeComposerLib.SyntaxTree;

public class SteIfElse : SteSyntaxElement
{
    public ISyntaxTreeElement Condition { get; set; }

    public ISyntaxTreeElement TrueCode { get; set; }

    public ISyntaxTreeElement ElseCode { get; set; }
}