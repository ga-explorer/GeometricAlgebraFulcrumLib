using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.ValueAccess;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Basic;

/// <summary>
/// This class represents an operands list where each operand is set using a value access process
/// </summary>
public sealed class OperandsByValueAccess : PolyadicOperands
{
    /// <summary>
    /// The list of operands assignments used for setting operands
    /// </summary>
    public List<OperandsByValueAccessAssignment> AssignmentsList { get; } = new List<OperandsByValueAccessAssignment>();


    private OperandsByValueAccess()
    {
    }


    /// <summary>
    /// Add a new operand to this list
    /// </summary>
    /// <param name="lhsValAccess">The value access process for this operand</param>
    /// <param name="rhsExpr">The RHS expression of the operand</param>
    public void AddOperand(LanguageValueAccess lhsValAccess, ILanguageExpressionAtomic rhsExpr)
    {
        AssignmentsList.Add(new OperandsByValueAccessAssignment(lhsValAccess, rhsExpr));
    }

    public override IEnumerable<ILanguageExpressionAtomic> RhsOperands
    {
        get {
            return AssignmentsList.Select(assignment => assignment.RhsExpression);
        }
    }

    public override PolyadicOperands Duplicate()
    {
        var operands = new OperandsByValueAccess();

        foreach (var assignment in AssignmentsList)
            operands.AssignmentsList.Add(new OperandsByValueAccessAssignment(assignment.LhsValueAccess, assignment.RhsExpression));

        return operands;
    }


    public override string ToString()
    {
        var s = new StringBuilder();

        s.Append("(");

        foreach (var commandAssign in AssignmentsList)
        {
            s.Append(commandAssign.LhsValueAccess);
            s.Append(" = ");
            s.Append(commandAssign.RhsExpression);
            s.Append(", ");
        }

        s.Length = s.Length - 2;

        s.Append(")");

        return s.ToString();
    }


    public static OperandsByValueAccess Create()
    {
        return new OperandsByValueAccess();
    }

}