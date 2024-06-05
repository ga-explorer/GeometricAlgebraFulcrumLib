namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;

/// <summary>
/// This class represents a root scope. There can be one root scope per parent Irony DSL
/// </summary>
public sealed class ScopeRoot : LanguageScope
{
    /// <summary>
    /// The parent Irony DSL for this scope
    /// </summary>
    public override IronyAst RootAst { get; }


    private ScopeRoot(IronyAst parentDsl)
        : base(null)
    {
        RootAst = parentDsl;
    }

    private ScopeRoot(IronyAst parentDsl, string scopeName)
        : base(null, scopeName)
    {
        RootAst = parentDsl;
    }


    public static ScopeRoot Create(IronyAst parentDsl)
    {
        return new ScopeRoot(parentDsl);
    }

    public static ScopeRoot Create(IronyAst parentDsl, string scopeName)
    {
        return new ScopeRoot(parentDsl, scopeName);
    }
}