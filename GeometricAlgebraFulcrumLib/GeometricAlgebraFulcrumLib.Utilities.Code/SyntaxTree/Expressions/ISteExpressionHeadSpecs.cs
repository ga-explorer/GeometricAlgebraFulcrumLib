namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;

/// <summary>
/// Any expression header must be an immutable class that implements this interface
/// </summary>
public interface ISteExpressionHeadSpecs : ISyntaxTreeElement
{
    string HeadText { get; }
}