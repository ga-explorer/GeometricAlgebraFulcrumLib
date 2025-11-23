using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Command;

/// <summary>
/// This class represents an If-ElseIf-Else command
/// </summary>
public class CommandIfElseIf : LanguageCommand
{
    //TODO: All TrueExpressions and FalseExpression must have the same ExpressionType

    /// <summary>
    /// All conditional expressions for the If and ElseIf parts
    /// </summary>
    private readonly List<ILanguageExpression> _conditionExpressions = new List<ILanguageExpression>();

    /// <summary>
    /// All True expressions for the If and ElseIf parts
    /// </summary>
    private readonly List<ILanguageExpression> _trueExpressions = new List<ILanguageExpression>();

    /// <summary>
    /// The expression for the Else part
    /// </summary>
    private ILanguageExpression _falseExpression;


    public CommandIfElseIf(LanguageScope parentScope)
        : base(parentScope)
    {
    }


    /// <summary>
    /// Add a new If or IfElse part of the command
    /// </summary>
    /// <param name="condExpr">The conditional expression for the added part</param>
    /// <param name="trueExpr">The True expression for the added part</param>
    public void AddIfBlock(ILanguageExpression condExpr, ILanguageExpression trueExpr)
    {
        _conditionExpressions.Add(condExpr);
        _trueExpressions.Add(trueExpr);
    }

    /// <summary>
    /// Set the Else part of the command
    /// </summary>
    /// <param name="falseExpr">The expression of the Else part</param>
    public void SetFalseExpression(ILanguageExpression falseExpr)
    {
        _falseExpression = falseExpr;
    }
}