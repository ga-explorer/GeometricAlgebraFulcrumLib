namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteAssign : SteSyntaxElement
{
    public bool LocalAssignment { get; set; }

    public ISyntaxTreeElement LeftHandSide { get; set; }

    public ISyntaxTreeElement RightHandSide { get; set; }
}