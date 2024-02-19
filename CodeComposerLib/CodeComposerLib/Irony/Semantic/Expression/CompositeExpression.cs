using System;
using CodeComposerLib.Irony.Semantic.Command;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Symbol;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression;

/// <summary>
/// This represents a composite expression having several operators and a single output. 
/// For example an expression 'x * y + z' is converted into the composite expression command block:
/// { declare output t2 : int; t1 = x * y; t2 = t1 + z; } 
/// This is equivalent to an expression tree with root node 't2'
/// </summary>
public class CompositeExpression : CommandBlock, ILanguageExpression
{
    /// <summary>
    /// The local variable holding the output value for this composite expression
    /// </summary>
    private SymbolLocalVariable _outputVariable;

    /// <summary>
    /// The local variable holding the output value for this composite expression. This attribute can be set once.
    /// </summary>
    public SymbolLocalVariable OutputVariable
    {
        get
        {
            return _outputVariable;
        }
        set
        {
            if (ReferenceEquals(_outputVariable, null))
                _outputVariable = value;
            else
                throw new InvalidOperationException("Output variable already declared");
        }
    }

        
    public ILanguageType ExpressionType => OutputVariable.SymbolType;

    /// <summary>
    /// A composite expression is not a simple expression
    /// </summary>
    public bool IsSimpleExpression => false;


    protected CompositeExpression(LanguageScope parentScope)
        : base(parentScope)
    {
    }

    protected CompositeExpression(LanguageScope parentScope, string localVariableRoleName)
        : base(parentScope, localVariableRoleName)
    {
    }


    public new static CompositeExpression Create(LanguageScope parentScope)
    {
        return new CompositeExpression(parentScope);
    }

    public new static CompositeExpression Create(LanguageScope parentScope, string localVariableRoleName)
    {
        return new CompositeExpression(parentScope, localVariableRoleName);
    }

    public new static CompositeExpression Create(IIronyAstObjectWithScope parentObject)
    {
        return new CompositeExpression(parentObject.ChildScope);
    }

    public new static CompositeExpression Create(IIronyAstObjectWithScope parentObject, string localVariableRoleName)
    {
        return new CompositeExpression(parentObject.ChildScope, localVariableRoleName);
    }
}