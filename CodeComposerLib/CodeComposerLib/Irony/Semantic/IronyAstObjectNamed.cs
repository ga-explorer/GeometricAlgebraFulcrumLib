using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using DataStructuresLib;

namespace CodeComposerLib.Irony.Semantic;

/// <summary>
/// The base abstract class for objects that can be stored in a parent Irony DSL
/// </summary>
public abstract class IronyAstObjectNamed : IIronyAstObjectNamed
{
    private static readonly IntegerSequenceGenerator IdCounter = new IntegerSequenceGenerator();

    private static int CreateNewId()
    {
        return IdCounter.GetNewCountId();
    }

    public static string CreateNewObjectName()
    {
        return IdCounter.GetNewStringId("sto_");
    }

    public static string CreateNewObjectName(string prefix)
    {
        return IdCounter.GetNewStringId(prefix);
    }


    /// <summary>
    /// Name of symbol
    /// </summary>
    public abstract string ObjectName { get; }

    /// <summary>
    /// Unique ID of object
    /// </summary>
    public int ObjectId { get; }

    /// <summary>
    /// Scope to which symbol belongs
    /// </summary>
    public LanguageScope ParentScope { get; }

    /// <summary>
    /// The parent Irony DSL for this symbol
    /// </summary>
    public virtual IronyAst RootAst => ParentScope.RootAst;

    /// <summary>
    /// True if this object is within a scope inside a parent symbol
    /// </summary>
    public bool HasParentSymbol => ParentScope is ScopeSymbolChild;

    /// <summary>
    /// The number of parent symbols of this symbol if any
    /// </summary>
    public int ParentSymbolsCount
    {
        get
        {
            var parent = ParentLanguageSymbol;

            return ReferenceEquals(parent, null)
                ? 0
                : parent.ParentSymbolsCount + 1;
        }
    }

    /// <summary>
    /// If this object is within a scope inside a parent symbol this returns the parent symbol
    /// </summary>
    public virtual LanguageSymbol ParentLanguageSymbol 
    { 
        get 
        {
            var scope = ParentScope as ScopeSymbolChild;

            return 
                ReferenceEquals(scope, null) ? 
                    null : 
                    scope.ParentLanguageSymbol;
        } 
    }

    /// <summary>
    /// Go up the scope chain until reaching a scope of a namespace and return that namespace if exists
    /// </summary>
    public virtual SymbolNamespace NearsestParentNamespace => ParentScope.NearsestParentNamespace;

    /// <summary>
    /// If this object is a namespace return it else go up the scope chain until reaching a scope of a namespace and return that namespace if exists
    /// </summary>
    public SymbolNamespace NearsestNamespace => (this as SymbolNamespace) ?? ParentScope.NearsestParentNamespace;


    protected IronyAstObjectNamed(LanguageScope parentScope)
    {
        ObjectId = CreateNewId();
        ParentScope = parentScope;
    }


    public override string ToString()
    {
        return RootAst.Describe(this);
    }
    //public virtual void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
    //{
    //    if (visitor is IASTNodeAcyclicVisitor<IronyDSLObjectNamed>)
    //        ((IASTNodeAcyclicVisitor<IronyDSLObjectNamed>)visitor).Visit(this);

    //    //You can write fall back logic here if needed.
    //}
}