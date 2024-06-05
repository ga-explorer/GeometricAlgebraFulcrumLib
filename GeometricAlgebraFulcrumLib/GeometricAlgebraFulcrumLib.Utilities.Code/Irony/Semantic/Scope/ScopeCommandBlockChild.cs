using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Command;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;

public sealed class ScopeCommandBlockChild : LanguageScope
{
    public CommandBlock ParentCommandBlock { get; }

    public CompositeExpression ParentCompositeExpression => ParentCommandBlock as CompositeExpression;


    private ScopeCommandBlockChild(CommandBlock parentBlock)
        : base(parentBlock.ParentScope)
    {
        ParentCommandBlock = parentBlock;
    }

    private ScopeCommandBlockChild(CommandBlock parentBlockExpr, string scopeName)
        : base(parentBlockExpr.ParentScope, scopeName)
    {
        ParentCommandBlock = parentBlockExpr;
    }


    public static ScopeCommandBlockChild Create(CommandBlock parentBlockExpr)
    {
        var scope = new ScopeCommandBlockChild(parentBlockExpr);

        RegisterChildScope(scope.ParentScope, scope);

        return scope;
    }

    public static ScopeCommandBlockChild Create(CommandBlock parentBlock, string scopeName)
    {
        var scope = new ScopeCommandBlockChild(parentBlock, scopeName);

        RegisterChildScope(scope.ParentScope, scope);

        return scope;
    }
}