using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Basic;

/// <summary>
/// This class represents an operands list that is a sparse index-based dictionary of operands
/// </summary>
public sealed class OperandsByIndex : PolyadicOperands
{
    /// <summary>
    /// The dictionary of operands for this list
    /// </summary>
    public Dictionary<ulong, ILanguageExpressionAtomic> OperandsDictionary { get; } 
        = new Dictionary<ulong, ILanguageExpressionAtomic>();


    private OperandsByIndex()
    {
    }


    public void ChangeOperand(ulong opIndex, ILanguageExpressionAtomic opExpr)
    {
        if (OperandsDictionary[opIndex].ExpressionType.IsSameType(opExpr.ExpressionType))
            OperandsDictionary[opIndex] = opExpr;
        else
            throw new InvalidOperationException();
    }

    /// <summary>
    /// Add a new operand to this list
    /// </summary>
    /// <param name="opIndex">The index of the operand</param>
    /// <param name="opExpr">The RHS expression of the operand</param>
    public void AddOperand(ulong opIndex, ILanguageExpressionAtomic opExpr)
    {
        OperandsDictionary.Add(opIndex, opExpr);
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
        var operands = new OperandsByIndex();

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
            s.Append("Parameter_" + pair.Key);
            s.Append(" = ");
            s.Append(pair.Value);
            s.Append(", ");
        }

        s.Length = s.Length - 2;

        s.Append(")");

        return s.ToString();
    }


    public static OperandsByIndex Create()
    {
        return new OperandsByIndex();
    }
}