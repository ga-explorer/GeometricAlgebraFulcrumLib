using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeComposerLib.Irony.Semantic.Expression.Basic;

/// <summary>
/// This class represents an operands list that is a sparse string-based dictionary of operands
/// </summary>
public sealed class OperandsByName : PolyadicOperands
{
    /// <summary>
    /// The dictionary of operands for this list
    /// </summary>
    public Dictionary<string, ILanguageExpressionAtomic> OperandsDictionary { get; } = new Dictionary<string, ILanguageExpressionAtomic>();


    private OperandsByName()
    {
    }


    public void ChangeOperand(string opName, ILanguageExpressionAtomic opExpr)
    {
        if (OperandsDictionary[opName].ExpressionType.IsSameType(opExpr.ExpressionType))
            OperandsDictionary[opName] = opExpr;
        else
            throw new InvalidOperationException();
    }

    /// <summary>
    /// Add a new operand to this list
    /// </summary>
    /// <param name="opName">The name of the operand</param>
    /// <param name="opExpr">The RHS expression of the operand</param>
    public void AddOperand(string opName, ILanguageExpressionAtomic opExpr)
    {
        OperandsDictionary.Add(opName, opExpr);
    }

    public override IEnumerable<ILanguageExpressionAtomic> RhsOperands
    {
        get 
        {
            return OperandsDictionary.Select(pair => pair.Value);
        }
    }

    public override PolyadicOperands Duplicate()
    {
        var operands = new OperandsByName();

        foreach (var pair in OperandsDictionary)
            operands.OperandsDictionary.Add(pair.Key, pair.Value);

        return operands;
    }


    public override string ToString()
    {
        var s = new StringBuilder();

        s.Append("(");

        foreach (var pair in OperandsDictionary)
        {
            s.Append(pair.Key);
            s.Append(" = ");
            s.Append(pair.Value);
            s.Append(", ");
        }

        s.Length = s.Length - 2;

        s.Append(")");

        return s.ToString();
    }


    public static OperandsByName Create()
    {
        return new OperandsByName();
    }
}