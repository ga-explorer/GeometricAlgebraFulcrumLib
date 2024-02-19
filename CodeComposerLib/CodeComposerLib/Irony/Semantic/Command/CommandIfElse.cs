using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Scope;

namespace CodeComposerLib.Irony.Semantic.Command;

/// <summary>
/// This class represents an If Else command
/// TODO: Should True and False parts be commands rather than expressions?
/// </summary>
public class CommandIfElse : LanguageCommand
{
    //TODO: Both TrueExpression and FalseExpression must have the same ExpressionType

    /// <summary>
    /// The conditional expression of the command
    /// </summary>
    public ILanguageExpression ConditionExpression { get; private set; }

    /// <summary>
    /// The True part of the command
    /// </summary>
    public ILanguageExpression TrueExpression { get; private set; }
        
    /// <summary>
    /// The False part of the command
    /// </summary>
    public ILanguageExpression FalseExpression { get; private set; }


    public CommandIfElse(LanguageScope parentScope, ILanguageExpression condExpr, ILanguageExpression trueExpr, ILanguageExpression falseExpr)
        : base(parentScope)
    {
        ConditionExpression = condExpr;
        TrueExpression = trueExpr;
        FalseExpression = falseExpr;
    }
}