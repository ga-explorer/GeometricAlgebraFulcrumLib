using System;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Expression;
using CodeComposerLib.Irony.Semantic.Expression.Basic;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Expression.ValueAccess;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Symbol;

namespace CodeComposerLib.Irony.DSLInterpreter;

/// <summary>
/// This class is used to apply some processing to LanguageValueAccess objects
/// </summary>
public abstract class LanguageValueAccessPrecessor
{
    /// <summary>
    /// Extract a list of all l-values from the given language operator
    /// </summary>
    /// <param name="langOperator"></param>
    /// <returns></returns>
    public abstract IEnumerable<SymbolLValue> GetLValues(ILanguageOperator langOperator);

    public IEnumerable<SymbolLValue> GetLValues(LanguageValueAccess valueAccess)
    {
        return valueAccess.AccessLValues;
    }

    public IEnumerable<SymbolLValue> GetLValues(BasicUnary expr)
    {
        var lvalues = GetLValues(expr.Operator);

        var access = expr.Operand as LanguageValueAccess;
            
        if (access != null)
            lvalues = lvalues.Concat(access.AccessLValues);

        return lvalues;
    }

    public IEnumerable<SymbolLValue> GetLValues(BasicBinary expr)
    {
        var lvalues = GetLValues(expr.Operator);

        var operand1 = expr.Operand1 as LanguageValueAccess;

        if (operand1 != null)
            lvalues = lvalues.Concat(operand1.AccessLValues);

        var operand2 = expr.Operand2 as LanguageValueAccess;

        if (operand2 != null)
            lvalues = lvalues.Concat(operand2.AccessLValues);

        return lvalues;
    }

    public IEnumerable<SymbolLValue> GetLValues(OperandsByIndex operands)
    {
        var lvalues = Enumerable.Empty<SymbolLValue>();

        return 
            operands
                .OperandsDictionary
                .Where(pair => pair.Value is LanguageValueAccess)
                .Aggregate(
                    lvalues, 
                    (current, pair) => current.Concat(((LanguageValueAccess) pair.Value).AccessLValues)
                );
    }

    public IEnumerable<SymbolLValue> GetLValues(OperandsByName operands)
    {
        var lvalues = Enumerable.Empty<SymbolLValue>();

        return 
            operands
                .OperandsDictionary
                .Where(pair => pair.Value is LanguageValueAccess)
                .Aggregate(
                    lvalues, 
                    (current, pair) => current.Concat(((LanguageValueAccess) pair.Value).AccessLValues)
                );
    }

    public IEnumerable<SymbolLValue> GetLValues(OperandsList operands)
    {
        var lvalues = Enumerable.Empty<SymbolLValue>();

        return 
            operands
                .Operands
                .OfType<LanguageValueAccess>()
                .Aggregate(
                    lvalues, 
                    (current, operand) => current.Concat((operand).AccessLValues)
                );
    }

    public IEnumerable<SymbolLValue> GetLValues(OperandsByValueAccess operands)
    {
        var lvalues = Enumerable.Empty<SymbolLValue>();

        return 
            operands
                .AssignmentsList
                .Where(assignment => assignment.RhsExpression is LanguageValueAccess)
                .Aggregate(
                    lvalues, 
                    (current, assignment) => current.Concat(((LanguageValueAccess) assignment.RhsExpression).AccessLValues)
                );
    }

    public IEnumerable<SymbolLValue> GetLValues(BasicPolyadic expr)
    {
        var lvalues = GetLValues(expr.Operator);

        return 
            expr
                .Operands
                .RhsOperands
                .OfType<LanguageValueAccess>()
                .Aggregate(
                    lvalues, 
                    (current, operand) => current.Concat((operand).AccessLValues)
                );
    }

    public virtual IEnumerable<SymbolLValue> GetLValues(ILanguageExpression expr)
    {
        var t1 = expr as LanguageValueAccess;

        if (t1 != null)
            return GetLValues(t1);

        var t2 = expr as BasicUnary;

        if (t2 != null)
            return GetLValues(t2);

        var t3 = expr as BasicBinary;

        if (t3 != null)
            return GetLValues(t3);

        var t4 = expr as BasicPolyadic;

        return 
            t4 != null 
                ? GetLValues(t4) 
                : Enumerable.Empty<SymbolLValue>();
    }

    /// <summary>
    /// Replace the occurances of the given l-value inside the given language operator with the given atomic expression
    /// </summary>
    /// <param name="oldLangOperator"></param>
    /// <param name="oldLvalue"></param>
    /// <param name="newExpr"></param>
    /// <returns></returns>
    public abstract ILanguageOperator ReplaceLValueByExpression(ILanguageOperator oldLangOperator, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr);

    public OperandsByIndex ReplaceLValueByExpression(OperandsByIndex oldOperands, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        var newOperands = new Dictionary<int, ILanguageExpressionAtomic>();

        foreach (var pair in oldOperands.OperandsDictionary)
        {
            var newOpExpr = ReplaceLValueByExpression(pair.Value, oldLvalue, newExpr);

            newOperands.Add((int)pair.Key, newOpExpr);
        }

        oldOperands.OperandsDictionary.Clear();

        foreach (var pair in newOperands)
            oldOperands.AddOperand((ulong)pair.Key, pair.Value);

        return oldOperands;
    }

    public OperandsByName ReplaceLValueByExpression(OperandsByName oldOperands, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        var newOperands = new Dictionary<string, ILanguageExpressionAtomic>();

        foreach (var pair in oldOperands.OperandsDictionary)
        {
            var newOpExpr = ReplaceLValueByExpression(pair.Value, oldLvalue, newExpr);

            newOperands.Add(pair.Key, newOpExpr);
        }

        oldOperands.OperandsDictionary.Clear();

        foreach (var pair in newOperands)
            oldOperands.AddOperand(pair.Key, pair.Value);

        return oldOperands;
    }

    public OperandsByValueAccess ReplaceLValueByExpression(OperandsByValueAccess oldOperands, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        foreach (var assignment in oldOperands.AssignmentsList)
        {
            var newOpExpr = ReplaceLValueByExpression(assignment.RhsExpression, oldLvalue, newExpr);

            assignment.ChangeRhsExpression(newOpExpr);
        }

        return oldOperands;
    }

    public OperandsList ReplaceLValueByExpression(OperandsList oldOperands, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        for (var i = 0; i < oldOperands.Operands.Count; i++)
        {
            var newOpExpr = ReplaceLValueByExpression(oldOperands.Operands[i], oldLvalue, newExpr);

            oldOperands.ChangeOperand(i, newOpExpr);
        }

        return oldOperands;
    }

    public BasicPolyadic ReplaceLValueByExpression(BasicPolyadic oldExpr, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        ReplaceLValueByExpression(oldExpr.Operator, oldLvalue, newExpr);

        var t1 = oldExpr.Operands as OperandsByIndex;

        if (t1 != null)
        {
            ReplaceLValueByExpression(t1, oldLvalue, newExpr);
            return oldExpr;
        }

        var t2 = oldExpr.Operands as OperandsByName;

        if (t2 != null)
        {
            ReplaceLValueByExpression(t2, oldLvalue, newExpr);
            return oldExpr;
        }

        var t3 = oldExpr.Operands as OperandsList;

        if (t3 != null)
        {
            ReplaceLValueByExpression(t3, oldLvalue, newExpr);
            return oldExpr;
        }

        var t4 = oldExpr.Operands as OperandsByValueAccess;

        if (t4 == null) 
            return oldExpr;

        ReplaceLValueByExpression(t4, oldLvalue, newExpr);
        return oldExpr;
    }

    public BasicBinary ReplaceLValueByExpression(BasicBinary oldExpr, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        ReplaceLValueByExpression(oldExpr.Operator, oldLvalue, newExpr);

        oldExpr.ChangeOperand1(ReplaceLValueByExpression(oldExpr.Operand1, oldLvalue, newExpr));

        oldExpr.ChangeOperand2(ReplaceLValueByExpression(oldExpr.Operand2, oldLvalue, newExpr));

        return oldExpr;
    }

    public BasicUnary ReplaceLValueByExpression(BasicUnary oldExpr, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        ReplaceLValueByExpression(oldExpr.Operator, oldLvalue, newExpr);

        oldExpr.ChangeOperand(ReplaceLValueByExpression(oldExpr.Operand, oldLvalue, newExpr));

        return oldExpr;
    }

    public ILanguageExpressionAtomic ReplaceLValueByExpression(ILanguageExpressionAtomic oldExpr, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        var expr = oldExpr as LanguageValueAccess;

        return 
            expr == null ? 
                oldExpr : 
                ReplaceLValueByExpression(expr, oldLvalue, newExpr);
    }

    public ILanguageExpressionAtomic ReplaceLValueByExpression(LanguageValueAccess oldValueAccess, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        //If the old value acceess does not depend on the old_lvalue just return the old value access as is
        if (oldValueAccess.HasAccessStepWithSymbol(oldLvalue) == false)
            return oldValueAccess;

        //If the old value access is a full access just return the new atomic expression
        if (oldValueAccess.IsFullAccess)
            return newExpr;
            
        //If the new expression is a language value access 
        var valAccess = newExpr as LanguageValueAccess;

        if (valAccess != null)
        {
            oldValueAccess.ReplaceRootSymbol(valAccess);

            foreach (var component in oldValueAccess.PartialAccessStepsByLValue(oldLvalue))
                if (valAccess.IsFullAccess)
                    component.ReplaceComponentSymbol(valAccess.RootSymbolAsLValue);
                else
                    throw new InvalidOperationException("Can't replace a symbol with a non-symbol in this access step");

            return oldValueAccess;
        }

        if (!(newExpr is ILanguageValue)) 
            return oldValueAccess;

        if (oldValueAccess.IsVariableAccess)
            throw new InvalidOperationException("Can't replace a symbol with a non-symbol in this access process");

        return ReadPartialValue((ILanguageValue)newExpr, oldValueAccess);
    }

    public virtual ILanguageExpression ReplaceLValueByExpression(ILanguageExpression oldExpr, SymbolLValue oldLvalue, ILanguageExpressionAtomic newExpr)
    {
        var t1 = oldExpr as LanguageValueAccess;

        if (t1 != null)
            return ReplaceLValueByExpression(t1, oldLvalue, newExpr);

        var t2 = oldExpr as BasicUnary;

        if (t2 != null)
            return ReplaceLValueByExpression(t2, oldLvalue, newExpr);

        var t3 = oldExpr as BasicBinary;

        if (t3 != null)
            return ReplaceLValueByExpression(t3, oldLvalue, newExpr);

        var t4 = oldExpr as BasicPolyadic;

        return 
            t4 == null 
                ? oldExpr 
                : ReplaceLValueByExpression(t4, oldLvalue, newExpr);
    }

    /// <summary>
    /// Apply the given value access to the source value to read a partial value
    /// </summary>
    /// <param name="sourceValue"></param>
    /// <param name="valueAccess"></param>
    /// <returns></returns>
    public virtual ILanguageValue ReadPartialValue(ILanguageValue sourceValue, LanguageValueAccess valueAccess)
    {
        return 
            valueAccess
                .PartialAccessSteps
                .Aggregate(sourceValue, ReadPartialValue);
    }

    /// <summary>
    /// Apply the given value access to the source value to read a partial value after skipping a number of
    /// access steps in the value access
    /// </summary>
    /// <param name="sourceValue"></param>
    /// <param name="valueAccess"></param>
    /// <param name="skipSteps"></param>
    /// <returns></returns>
    public virtual ILanguageValue ReadPartialValue(ILanguageValue sourceValue, LanguageValueAccess valueAccess, int skipSteps)
    {
        return 
            valueAccess
                .PartialAccessSteps
                .Skip(skipSteps)
                .Aggregate(sourceValue, ReadPartialValue);
    }

    /// <summary>
    /// Change a partial value inside the source value using the given value access
    /// </summary>
    /// <param name="sourceValue"></param>
    /// <param name="valueAccess"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual ILanguageValue WritePartialValue(ILanguageValue sourceValue, LanguageValueAccess valueAccess, ILanguageValue value)
    {
        if (valueAccess.IsFullAccess)
            throw new InvalidOperationException();

        sourceValue = 
            valueAccess
                .PartialAccessStepsExceptLast
                .Aggregate(sourceValue, ReadPartialValue);

        return WritePartialValue(sourceValue, valueAccess.LastAccessStep, value);
    }

    /// <summary>
    /// Read a partial value from the source value using the given value access step
    /// </summary>
    /// <param name="sourceValue"></param>
    /// <param name="valueAccessStep"></param>
    /// <returns></returns>
    protected abstract ILanguageValue ReadPartialValue(ILanguageValue sourceValue, ValueAccessStep valueAccessStep);

    /// <summary>
    /// Change a partial value inside the source value using the given value access step
    /// </summary>
    /// <param name="sourceValue"></param>
    /// <param name="valueAccessStep"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected abstract ILanguageValue WritePartialValue(ILanguageValue sourceValue, ValueAccessStep valueAccessStep, ILanguageValue value);
}