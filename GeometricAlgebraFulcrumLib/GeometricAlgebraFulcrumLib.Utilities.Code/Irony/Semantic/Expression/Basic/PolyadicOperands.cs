using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.Basic;

/// <summary>
/// This abstract class is the base for all operands list for basic polyadic expressions
/// </summary>
public abstract class PolyadicOperands
{
    /// <summary>
    /// The operands of this operands list are accessed by operand index (a sparse list of operands)
    /// </summary>
    public bool IsByIndex => this is OperandsByIndex;

    /// <summary>
    /// The operands of this operands list are accessed by operand name (a sparse list of operands)
    /// </summary>
    public bool IsByName => this is OperandsByName;

    /// <summary>
    /// The operands of this operands list are accessed by a value access operation per operand (a list of assignment commands)
    /// </summary>
    public bool IsByValueAccess => this is OperandsByValueAccess;

    /// <summary>
    /// This is a linear list of operands
    /// </summary>
    public bool IsList => this is OperandsList;


    public OperandsByIndex AsByIndex => this as OperandsByIndex;

    public OperandsByName AsByName => this as OperandsByName;

    public OperandsByValueAccess AsByValueAccess => this as OperandsByValueAccess;

    public OperandsList AsList => this as OperandsList;


    public abstract PolyadicOperands Duplicate();

    public abstract IEnumerable<ILanguageExpressionAtomic> RhsOperands { get; }

    public bool IsAllSimpleOperands 
    { 
        get 
        { 
            return 
                RhsOperands.FirstOrDefault(
                    operand => !operand.IsSimpleExpression
                ) == null; 
        } 
    }
}