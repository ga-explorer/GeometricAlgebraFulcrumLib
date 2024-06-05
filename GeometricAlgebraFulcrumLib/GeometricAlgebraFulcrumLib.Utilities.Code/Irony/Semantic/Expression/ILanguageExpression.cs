using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression;

public interface ILanguageExpression : IIronyAstObject
{
    /// <summary>
    /// Type of expression (may be null for typless expressions)
    /// </summary>
    ILanguageType ExpressionType { get; }

    /// <summary>
    /// This returns true if the given expression is simple
    /// A simple expression is an atomic expression or a basic expression (but never a composite expression).
    /// A simple expression does not depend on any l-values (local variables or procedure parameters) for its computations.
    /// </summary>
    bool IsSimpleExpression { get; }
}