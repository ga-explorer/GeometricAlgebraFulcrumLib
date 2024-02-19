﻿namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff;

/// <summary>
/// Visitor for terms that has no result from its computations.
/// </summary>
public interface ITermVisitor
{
    /// <summary>
    /// Performs an action for a constant term.
    /// </summary>
    /// <param name="constant">The input term.</param>
    void Visit(Constant constant);

    /// <summary>
    /// Performs an action for a zero term.
    /// </summary>
    /// <param name="zero">The input term.</param>
    void Visit(Zero zero);

    /// <summary>
    /// Performs an action for a constant power term.
    /// </summary>
    /// <param name="power">The input term.</param>
    void Visit(ConstPower power);

    /// <summary>
    /// Performs an action for a power term.
    /// </summary>
    /// <param name="power">The input term.</param>
    void Visit(TermPower power);

    /// <summary>
    /// Performs an action for a product term.
    /// </summary>
    /// <param name="product">The input term.</param>
    void Visit(Product product);

    /// <summary>
    /// Performs an action for a sum term.
    /// </summary>
    /// <param name="sum">The input term.</param>
    void Visit(Sum sum);

    /// <summary>
    /// Performs an action for a variable term.
    /// </summary>
    /// <param name="variable">The input term.</param>
    void Visit(Variable variable);

    /// <summary>
    /// Performs an action for a logarithm term.
    /// </summary>
    /// <param name="log">The input term.</param>
    void Visit(Log log);

    /// <summary>
    /// Performs an action for an exponential function term.
    /// </summary>
    /// <param name="exp">The input term.</param>
    void Visit(Exp exp);

    /// <summary>
    /// Performs an action for an unary function.
    /// </summary>
    /// <param name="func">The unary function</param>
    void Visit(UnaryFunc func);

    /// <summary>
    /// Performs an action for a binary function.
    /// </summary>
    /// <param name="func">The binary function</param>
    void Visit(BinaryFunc func);

    /// <summary>
    /// Performs an action for a n-ary function.
    /// </summary>
    /// <param name="func">The n-ary function</param>
    void Visit(NaryFunc func);
}