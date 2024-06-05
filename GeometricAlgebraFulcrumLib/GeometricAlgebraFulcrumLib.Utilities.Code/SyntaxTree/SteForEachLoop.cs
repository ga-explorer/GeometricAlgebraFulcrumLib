namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteForEachLoop : SteSyntaxElement
{
    public string LoopVariableName { get; set; }

    public string LoopVariableType { get; set; }

    public ISyntaxTreeElement LoopCollection { get; set; }

    public ISyntaxTreeElement LoopCode { get; set; }


}